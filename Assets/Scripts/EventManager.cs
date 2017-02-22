using UnityEngine;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

    public static EventManager instance;

    // for missile attack
    public Transform targetCross;
    public GameObject missile;

    LayerMask layerMask = (1 << 8 | 1 << 9); // containing students and faculties

    List<Faculty> facultyKilled;

    void Awake() {

        // singleton:
        if (instance == null)
            instance = this;
        else
            Destroy(instance);

        facultyKilled = new List<Faculty>();
    }


    void Update()
    {

        if (Input.GetKey(KeyCode.E))
            if (Input.GetMouseButtonDown(0))
                Explosion_Event();

        if (Input.GetKeyDown(KeyCode.M))
            Murder_Event();

        if (Input.GetKeyDown(KeyCode.S))
            PseudoEarthQuake_Event();

        if (Input.GetKey(KeyCode.F))
            if (Input.GetMouseButtonDown(0))
                MissleAttack_Event();

        if (Input.GetKeyDown(KeyCode.Z))
            Fire_Event();
    }
    // TODO: we first need to figure out how to achieve the effects
    // and then try to figure out the relationship between faculty, building and the possibility of each event.

    public void Explosion_Event() {
        // figure out a random point within the map range but exclude playground area.
        int range = 2;
      
        Vector3 explosionPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (MouseController.instance.GetTileAtWorldCoord(explosionPoint) == null)
        {
            return;
        }
        // and expand it to a circle and get all the things in the circle and destory bascially.

        Collider2D[] collInRange = Physics2D.OverlapCircleAll(new Vector2(explosionPoint.x, explosionPoint.y), range, layerMask, -10, 10);
        GameObject[] goInRange = new GameObject[collInRange.Length];

        if (collInRange.Length == 0)
        {
            //Debug.Log("no people in range");
            return;
        }

        for (int i = 0; i < collInRange.Length; i++)
        {
            goInRange[i] = collInRange[i].gameObject;
        }

        // check if it is a faculty or a student.
        // faculty: decrease its life time; student: directly kill them.
        foreach (GameObject go in goInRange)
        {
            if (go.GetComponent<Faculty>() != null)
            {
                go.GetComponent<Faculty>().lifeTime -= 10;               
            }else if(go.GetComponent<Student>() != null)
            {
                go.GetComponent<Student>().Die();
            }
            
        }

        // print to the information window.
        UpdateBarManager.current.UpdateInformationOnBar("Explosion affects " + goInRange.Length + " people!");

        // prob animation and sound effect.
    }

    public void Murder_Event() {
        // simply get rid of one student from the student list.

        if (StudentFactory._studentList.Count <= 0)
            return;

        int index = Mathf.RoundToInt(Random.Range(0, StudentFactory._studentList.Count - 1));

        UpdateBarManager.current.UpdateInformationOnBar("One student was murdered!");

        GameObject target = StudentFactory._studentList[index].gameObject;
        StudentFactory._studentList.Remove(target.GetComponent<Student>());
        target.GetComponent<Student>().Die();
    }
   
    private System.Collections.IEnumerator WaitForMissle (){

        Debug.Log(targetCross.transform.position);
        yield return new WaitForSeconds(3);
        GameObject _missile = Instantiate(missile, targetCross.position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(1);
        GameObject.Destroy(_missile);
        targetCross.gameObject.SetActive(false);
        CameraShake.instance.BeginShaking(3f, 15);

        Collider2D[] studentsAffected = Physics2D.OverlapCircleAll(new Vector2 (targetCross.position.x, targetCross.position.y), 2, layerMask, -10, 10);

        for (int i = 0; i < studentsAffected.Length; i++)
        {
            GameObject std = studentsAffected[i].gameObject;
            Vector3 offset = std.transform.position - new Vector3(_missile.transform.position.x, _missile.transform.position.y, std.transform.position.z);
            std.GetComponent<Student>().MoveTowards(offset);

            std.GetComponent<Student>().Health = 60;
        }

        yield return new WaitForSeconds(1);
    } 

    public void MissleAttack_Event() {
         
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Tile targetTile = MouseController.instance.GetTileAtWorldCoord(pos);

        if (targetTile == null || targetTile.Type != TileType.PlayGround)
            return;

        targetCross.gameObject.SetActive(true);
        targetCross.position = new Vector3(targetTile.X, targetTile.Y, targetCross.transform.position.z);

        StartCoroutine(WaitForMissle());
    }

    public void PseudoEarthQuake_Event() {
        // make the camera vibrate and kill randomly
        CameraShake.instance.BeginShaking(1f, 80);

        int studentKilledNum = Mathf.RoundToInt(Random.Range(0, StudentFactory._studentList.Count));
        for (int i = 0; i < studentKilledNum; i++)
        {
            if (StudentFactory._studentList.Count < 0)
                return;

            GameObject targetStudent = StudentFactory._studentList[0].gameObject;
            StudentFactory._studentList.Remove(targetStudent.GetComponent<Student>());
            targetStudent.GetComponent<Student>().Die();
        }


        float radius = 1;

        if(MouseController.buildingsAlreadyInstalled.Count != 0)
        {
            foreach(Building b in MouseController.buildingsAlreadyInstalled)
            {
                Collider2D[] facultyColliders = Physics2D.OverlapCircleAll(b.go.transform.position, radius, layerMask, -10, 10);

                for (int i = 0; i < facultyColliders.Length; i++)
                {
                    if (!facultyKilled.Contains(facultyColliders[i].gameObject.GetComponent<Faculty>()))
                    {
                        facultyKilled.Add(facultyColliders[i].gameObject.GetComponent<Faculty>());
                    }
                }
            }
        }

        if (facultyKilled.Count == 0)
            return;

        for (int i = 0; i < facultyKilled.Count; i++)
        {
            if(facultyKilled[i] != null)
                facultyKilled[i].Die();
        }

        facultyKilled.Clear();
        // increase the possibility of educational check.
    }

    // --------------------------------------------------------------------------->

    public void RampantPhantom()
    {
        // make the scene darker
        // add in ghosts of students
        // get rid of the bodies in the middle.

    }

    public void Fire_Event()
    {
        // choose one faculty or student to runaway, not die. 
        // decrease the notorious level.
    }

    public void Suicide_Event() {
        // student lies without increasing the notorious level.
    }

    public void GasPoison_Event() {
        // certain type of faculty and destory, kill students along the whole column or row.
    }

    public void ChemicalAccident_Event()
    {
        // get the student list and choose a random index to get rid of
        // might effec other students, not sure now.

        // print to the information window.
        // animation and sound effect.
    }

    public void Brawl_Event() {
        // students die and drastically increase the notorious level.

    }
    public void FoodPoison_Event() {
        // choose one dining hall to kill students within two tiles.
    }
    public void EducationCheck_Event() {
        // instantiate a national education department car
        // animation starts.
        // check standard to see if it fullfills.
        // do certain punishment if not.

        // should have some sub methods to define what to do.
    }
    public void NervousBreakdown_Event() {
        // choose one student to kill
        // and students refuse to pay tuition for a certain amount of time.
    }
    public void MonsterAppear_Event() {
        // instantiate a monster to kill anybody near it.
    }
    public void UFO_Event() {
        // take some people away, some faculty and some students.
        // invoke fear among students.
        // so students would move faster.
    }
}
