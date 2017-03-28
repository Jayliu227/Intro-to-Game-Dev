using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StudentFactory : MonoBehaviour {
    public GameObject student;
    public static int maxStudentAmount = 5;

    public static List<Student> _studentList;
    public static List<Student> _studentGraveStones;

	public Sprite[] bodies = new Sprite[7];
	public Sprite[] heads = new Sprite[18];

	AudioSource Audio;

    void Start()
    {
		Audio = GetComponent<AudioSource> ();
        _studentList = new List<Student>();
        _studentGraveStones = new List<Student>();
        // before revision there seemed to be a problem.
        // a static variable is assgined with a non static method seems to be problematic.
        FacultyFactory.RegisterStudentSpawnEvent(SpawnStudent);
    }

	void Update(){
		if (_studentList.Count > 0) {
			if (!GetComponent<AudioSource> ().isPlaying) {
				Audio.Play ();
			}
		}else
			Audio.Stop ();
	}

    public void SpawnStudent()
    {
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
		int head = Mathf.RoundToInt(Random.Range (0, 18));
		int body = Mathf.RoundToInt(Random.Range (0, 7));

		_student.GetComponent<SpriteRenderer> ().sprite = bodies[body];
		_student.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = heads[head];

        _student.transform.SetParent(GameObject.Find("Student Manager").transform);
        _studentList.Add(_student.GetComponent<Student>());
    }

    public static void UpgradeStudentMaxAmount(int amount)
    {
        maxStudentAmount += amount;
        UpdateBarManager.current.UpdateInformationOnBar("current student max is " + maxStudentAmount);
    }
}
