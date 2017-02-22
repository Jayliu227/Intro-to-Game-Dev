using UnityEngine;
using System.Collections.Generic;

public class FacultyFactory : MonoBehaviour {

    int facultyCount = 0;

    public static List<Faculty> facultyLists;
    private static int maxFacultyNum = 5;
    public static int MaxFacultyNum
    {
        get { return maxFacultyNum; }
        set { maxFacultyNum += value; UpdateBarManager.current.UpdateInformationOnBar("Now maximum faculty number is " + maxFacultyNum); }
    }

    public static int durabilityBonus = 0;

    public GameObject prisoner;
    public GameObject madScientist;
    public GameObject profKiller;
    public GameObject thief;

    public delegate void studentDelegate();
    public static event studentDelegate OnSpawnStudent;

    void Start()
    {
        facultyLists = new List<Faculty>();
    }

    public static void RegisterStudentSpawnEvent(studentDelegate sd)
    {
        OnSpawnStudent += sd;
    }


    public static void UnRegisterStudentSpawnEvent(studentDelegate sd)
    {
        OnSpawnStudent -= sd;
    }

    public void InstantiateFaculty(string FacultyType)
    {
        switch (FacultyType)
        {
            case "Prisoner":
                if (GameController.instance._PlayerMoney < prisoner.GetComponent<Prisoner>().price || facultyLists.Count >= maxFacultyNum)
                {
                    UpdateBarManager.current.UpdateInformationOnBar("No room or money for prisoner");
                    return;
                }                    

                GameObject _prisoner = Instantiate(prisoner) as GameObject;
         
                _prisoner.transform.position = new Vector3(_prisoner.GetComponent<Prisoner>().pos.x, _prisoner.GetComponent<Prisoner>().pos.y, 0f);

                _prisoner.transform.SetParent(GameObject.Find("_PRISONER").transform);

                _prisoner.GetComponent<Prisoner>().lifeTime += durabilityBonus;
                // BUG: this was bugged because I added the wrong unit into the faculty list.
                facultyLists.Add(_prisoner.GetComponent<Prisoner>());

                UpdateBarManager.current.UpdateInformationOnBar("A new prisoner is coming out!");

                GameController.instance.SpendMoney(_prisoner.GetComponent<Prisoner>().price);

                facultyCount++;

                break;

            case "MadScientist":
                if (GameController.instance._PlayerMoney < madScientist.GetComponent<MadScientist>().price || facultyLists.Count >= maxFacultyNum)
                {
                    UpdateBarManager.current.UpdateInformationOnBar("No room or money for mad scientist");
                    return;
                }

                if (GameController.instance.buildingNum["lab"] <= 0 || GameController.instance.buildingNum["diningHall"] <= 0)
                {
                    UpdateBarManager.current.UpdateInformationOnBar("Lacks hospital or dining hall for mad scientist");
                    return;
                }

                GameObject _madScientist = Instantiate(madScientist) as GameObject;

                _madScientist.transform.position = new Vector3(_madScientist.GetComponent<MadScientist>().pos.x, _madScientist.GetComponent<MadScientist>().pos.y, 0f);

                _madScientist.transform.SetParent(GameObject.Find("_MADSCIENTIST").transform);

                _madScientist.GetComponent<MadScientist>().lifeTime += durabilityBonus;

                facultyLists.Add(_madScientist.GetComponent<MadScientist>());

                UpdateBarManager.current.UpdateInformationOnBar("A new mad scientist is coming out!");

                GameController.instance.SpendMoney(_madScientist.GetComponent<MadScientist>().price);

                facultyCount++;

                break;

            case "ProfKiller":
                if (GameController.instance._PlayerMoney < profKiller.GetComponent<ProfKiller>().price || facultyLists.Count >= maxFacultyNum)
                {
                    UpdateBarManager.current.UpdateInformationOnBar("No room or money for professional killer" );
                    return;
                }

                if(GameController.instance.buildingNum["hospital"] <= 0)
                {
                    UpdateBarManager.current.UpdateInformationOnBar("Lacks Hospital for professional killer");
                    return;
                }

                GameObject _profKiller = Instantiate(profKiller) as GameObject;

                _profKiller.transform.position = new Vector3(_profKiller.GetComponent<ProfKiller>().pos.x, _profKiller.GetComponent<ProfKiller>().pos.y, 0f);

                _profKiller.transform.SetParent(GameObject.Find("_PROFKILLER").transform);

                _profKiller.GetComponent<ProfKiller>().lifeTime += durabilityBonus;

                facultyLists.Add(_profKiller.GetComponent<ProfKiller>());

                GameController.instance.SpendMoney(_profKiller.GetComponent<ProfKiller>().price);

                UpdateBarManager.current.UpdateInformationOnBar("A new professional killer is coming out!");

                facultyCount++;

                break;

            case "Thief":
                if (GameController.instance._PlayerMoney < thief.GetComponent<Thief>().price || facultyLists.Count >= maxFacultyNum)
                {
                    UpdateBarManager.current.UpdateInformationOnBar("No room or money for thief");
                    return;
                }

                if (GameController.instance.buildingNum["retailer"] <= 0)
                {
                    UpdateBarManager.current.UpdateInformationOnBar("Lacks Hospital for thief");
                    return;
                }

                GameObject _thief = Instantiate(thief) as GameObject;

                _thief.transform.position = new Vector3(_thief.GetComponent<Thief>().pos.x, _thief.GetComponent<Thief>().pos.y, 0f);

                _thief.transform.SetParent(GameObject.Find("_THIEF").transform);

                _thief.GetComponent<Thief>().lifeTime += durabilityBonus;

                facultyLists.Add(_thief.GetComponent<Thief>());

                GameController.instance.SpendMoney(_thief.GetComponent<Thief>().price);

                UpdateBarManager.current.UpdateInformationOnBar("A new thief is coming out!");

                facultyCount++;

                break;

            default:
                Debug.Log("faculty type is not recognized");
                break;
        }

        if(facultyCount >= 1)
        {
            if(OnSpawnStudent != null)
            {              
                OnSpawnStudent();
                facultyCount = 0;
            }
            
        }

    }
}
