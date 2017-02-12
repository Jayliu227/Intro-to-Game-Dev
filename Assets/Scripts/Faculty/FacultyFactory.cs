using UnityEngine;
using System.Collections.Generic;

public class FacultyFactory : MonoBehaviour {

    public static List<Faculty> facultyLists;
    public GameObject prisoner;

    void Start()
    {
        facultyLists = new List<Faculty>();
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
                break;  
            default:
                Debug.Log("faculty type is not recognized");
                break;
        }

    }
}
