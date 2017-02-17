using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StudentFactory : MonoBehaviour {
    public GameObject student;
    public int maxStudentAmount = 10;

    public static List<Student> _studentList;

    void Start()
    {
        _studentList = new List<Student>();
        // before revision there seemed to be a problem.
        // a static variable is assgined with a non static method seems to be problematic.
        FacultyFactory.RegisterStudentSpawnEvent(SpawnStudent);
    }

    public void SpawnStudent()
    {
        Debug.Log("new student comes out");
        // if student max is suppassed, no students would come unless the max is upgraded.
        if (_studentList.Capacity > maxStudentAmount)
            return;

        Vector3 pos = new Vector3(Random.Range(WorldController.Instance.World.Width / 3, WorldController.Instance.World.Width - (WorldController.Instance.World.Width / 3 )),
                                  Random.Range(WorldController.Instance.World.Height / 3, WorldController.Instance.World.Height - (WorldController.Instance.World.Width / 3 )), 0f);

        Tile tileUnderPos = MouseController.instance.GetTileAtWorldCoord(pos);

        while(tileUnderPos.Type != TileType.PlayGround)
            pos = new Vector3(Random.Range(WorldController.Instance.World.Width / 3 , WorldController.Instance.World.Width - (WorldController.Instance.World.Width / 3 )),
                                  Random.Range(WorldController.Instance.World.Height / 3 , WorldController.Instance.World.Height - (WorldController.Instance.World.Width / 3 )), 0f);

        GameObject _student = Instantiate(student, pos, Quaternion.identity) as GameObject;

        _student.transform.SetParent(GameObject.Find("Student Manager").transform);

        _studentList.Add(_student.GetComponent<Student>());
    }

    public void UpgradeStudentMaxAmount(int amount)
    {
        maxStudentAmount += amount;
        UpdateBarManager.current.UpdateInformationOnBar("current student max is " + maxStudentAmount);
    }
}
