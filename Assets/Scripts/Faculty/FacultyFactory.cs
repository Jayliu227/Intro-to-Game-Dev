using UnityEngine;
using System.Collections.Generic;

public class FacultyFactory : MonoBehaviour {

    int facultyCount = 0;

    public static List<Faculty> facultyLists;
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
        Debug.Log(sd);
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
                GameObject _prisoner = Instantiate(prisoner) as GameObject;
         
                _prisoner.transform.position = new Vector3(_prisoner.GetComponent<Prisoner>().pos.x, _prisoner.GetComponent<Prisoner>().pos.y, 0f);

                _prisoner.transform.SetParent(GameObject.Find("Faculty Manager").transform);

                facultyLists.Add(prisoner.GetComponent<Prisoner>());

                UpdateBarManager.current.UpdateInformationOnBar("A new prisoner is coming out!");

                facultyCount++;

                break;

            case "MadScientist":
                GameObject _madScientist = Instantiate(madScientist) as GameObject;

                _madScientist.transform.position = new Vector3(_madScientist.GetComponent<MadScientist>().pos.x, _madScientist.GetComponent<MadScientist>().pos.y, 0f);

                _madScientist.transform.SetParent(GameObject.Find("Faculty Manager").transform);

                facultyLists.Add(madScientist.GetComponent<MadScientist>());

                UpdateBarManager.current.UpdateInformationOnBar("A new mad scientist is coming out!");

                facultyCount++;

                break;

            case "ProfKiller":
                GameObject _profKiller = Instantiate(profKiller) as GameObject;

                _profKiller.transform.position = new Vector3(_profKiller.GetComponent<ProfKiller>().pos.x, _profKiller.GetComponent<ProfKiller>().pos.y, 0f);

                _profKiller.transform.SetParent(GameObject.Find("Faculty Manager").transform);

                facultyLists.Add(profKiller.GetComponent<ProfKiller>());

                UpdateBarManager.current.UpdateInformationOnBar("A new professional killer is coming out!");

                facultyCount++;

                break;

            case "Thief":
                GameObject _thief = Instantiate(thief) as GameObject;

                _thief.transform.position = new Vector3(_thief.GetComponent<Thief>().pos.x, _thief.GetComponent<Thief>().pos.y, 0f);

                _thief.transform.SetParent(GameObject.Find("Faculty Manager").transform);

                facultyLists.Add(thief.GetComponent<Thief>());

                UpdateBarManager.current.UpdateInformationOnBar("A new thief is coming out!");

                facultyCount++;

                break;

            default:
                Debug.Log("faculty type is not recognized");
                break;
        }

        if(facultyCount > 2)
        {
            if(OnSpawnStudent != null)
            {              
                OnSpawnStudent();
                facultyCount = 0;
            }
            
        }

    }
}
