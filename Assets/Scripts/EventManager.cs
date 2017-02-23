﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

    public static EventManager instance;

    public float bonusPossibility = 0;
    // for missile attack
    public Transform targetCross;
    public GameObject missile;

    public RectTransform notification;

    LayerMask layerMask = (1 << 8 | 1 << 9); // containing students and faculties

    List<Faculty> facultyKilled;

    // check is the event is triggerable.
    bool isEarthQuakable = false;
    bool isExplodable = false;
    bool isMurderable = false;
    bool isMissileAttackable = false;

    void Awake() {

        // singleton:
        if (instance == null)
            instance = this;
        else
            Destroy(instance);

        facultyKilled = new List<Faculty>();
    }

    void Start()
    {
        StartCoroutine(EducationalCheck());
        StartCoroutine(CheckCondition());
    }

    void Update()
    {
        TriggerEvents();
    }

    System.Collections.IEnumerator CheckCondition()
    {
        while (true)
        {
            checkCondition();
            yield return new WaitForSeconds(2);
        }
    }

    void checkCondition()
    {
        float randomNum = Random.Range(0, 100);

        float PEarthQuake = 0.1f + bonusPossibility;
        float PExplosion = 0.1f + bonusPossibility;
        float PMurder = 0.1f + bonusPossibility;
        float PMissileAttack = 0.1f + bonusPossibility;

        // condition...
        PEarthQuake += (FacultyFactory.facultyLists.Count + StudentFactory._studentList.Count) * 0.5f + MouseController.buildingsAlreadyInstalled.Count * 0.5f;
        PExplosion += GameController.instance.facultyNum["madScientist"] * 1.0f + GameController.instance.facultyNum["prisoner"] * 0.5f;
        PMurder += FacultyFactory.facultyLists.Count + GameController.instance.facultyNum["profKiller"] * 0.5f;
        PMissileAttack += GameController.instance.facultyNum["madScientist"] * 1.0f + GameController.instance.facultyNum["profKiller"] * 0.5f + GameController.instance.facultyNum["prisoner"] * 0.5f;

        Debug.Log(PEarthQuake +"  "+ PExplosion + "  " +  PMurder + "  " + PMissileAttack);

        if(!isEarthQuakable && randomNum <= PEarthQuake)
        {
            isEarthQuakable = true;
        }

        if(!isEarthQuakable && randomNum <= PEarthQuake)
        {
            isEarthQuakable = true;
        }

        if(!isExplodable && randomNum <= PExplosion)
        {
            isExplodable = true;
            notification.GetChild(0).GetComponent<Text>().color = Color.red;
        }

        if(!isMurderable && randomNum <= PMurder)
        {
            isMurderable = true;
            notification.GetChild(1).GetComponent<Text>().color = Color.red;
        }

        if(!isMissileAttackable && randomNum <= PMissileAttack)
        {
            isMissileAttackable = true;
            notification.GetChild(2).GetComponent<Text>().color = Color.red;
        }
    }

    void TriggerEvents()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            CleanGround();
        }

        if (Input.GetKey(KeyCode.E) && isExplodable)
            if (Input.GetMouseButtonDown(0))
            {
                Explosion_Event();
                isExplodable = false;
                notification.GetChild(0).GetComponent<Text>().color = Color.black;
            }
        if (Input.GetKeyDown(KeyCode.K) && isMurderable)
        {
            Murder_Event();
            isMurderable = false;
            notification.GetChild(1).GetComponent<Text>().color = Color.black;
        }

        if (isEarthQuakable)
        {
            PseudoEarthQuake_Event();
            isEarthQuakable = false;
        }
        if (Input.GetKey(KeyCode.M) && isMissileAttackable)
            if (Input.GetMouseButtonDown(0))
            {
                MissleAttack_Event();
                isMissileAttackable = false;
                notification.GetChild(3).GetComponent<Text>().color = Color.black;
            }
    }
    // TODO: we first need to figure out how to achieve the effects
    // and then try to figure out the relationship between faculty, building and the possibility of each event.

    // --------------------------------------------------------------------------->
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

        yield return new WaitForSeconds(3);
        GameObject _missile = Instantiate(missile, targetCross.position, Quaternion.identity) as GameObject;
        CameraShake.instance.BeginShaking(0.05f, 40);

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

        Student[] graves = new Student[StudentFactory._studentGraveStones.Count];
        StudentFactory._studentGraveStones.CopyTo(graves);

        foreach (Student s in graves)
            s.RemoveGraveStone();

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

    public void Suicide_Event() {
        Murder_Event();
    }

    private System.Collections.IEnumerator EducationalCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(120);           
            UpdateBarManager.current.UpdateInformationOnBar("Educational Event would happen in 10 sec! Work Harder");
            yield return new WaitForSeconds(10);
            EducationCheck_Event();
            if(!EducationCheck_Event())
                UpdateBarManager.current.UpdateInformationOnBar("You've passed. The next check would happen in 2 min.");
            else
                UpdateBarManager.current.UpdateInformationOnBar("Sorry...");
        }
    }

    public bool EducationCheck_Event()
    {
        // instantiate a national education department car
        // animation starts.
        // check standard to see if it fullfills.
        bool isFullFilled = true;
        GameController gc = GameController.instance;
        
        if(gc._PlayerMoney < 1500)
        {
            UpdateBarManager.current.UpdateInformationOnBar("The great philanthropist Jay Liu anonymously supported this school with " + 3500 + ".");
            gc._PlayerMoney += 1500;
        }

        if(StudentFactory._studentList.Count <= 10)
        {
            UpdateBarManager.current.UpdateInformationOnBar("Educational Department's Director, Ms Stella, feels ashamed of this school's environment for students.");
            gc.SpendMoney(-200 * (10 - StudentFactory._studentList.Count));
            isFullFilled = false;
        }

        if (FacultyFactory.facultyLists.Count <= 10)
        {
            UpdateBarManager.current.UpdateInformationOnBar("Educational Department's Director, Ms Stella, critizes this school for back teaching quality.");
            gc.SpendMoney(-200 * (10 - FacultyFactory.facultyLists.Count));
            isFullFilled = false;
        }

        if(StudentFactory._studentGraveStones.Count <= 3)
        {
            UpdateBarManager.current.UpdateInformationOnBar("Educational Department's Director, Ms Stella, is really satisfied by the clean campus!");
            gc._PlayerMoney += 1000;
        }

        if (!isFullFilled)
            EducationCheckPunishment();

        return isFullFilled;
    }

    private void EducationCheckPunishment()
    {
        GameController.instance._PlayerMoney = -1500;
        GameController.instance._playerNotorityLevel -= 10;
    }

    public void CleanGround()
    {
        Student[] graves = new Student[StudentFactory._studentGraveStones.Count];
        StudentFactory._studentGraveStones.CopyTo(graves);

        if (graves.Length == 0 || GameController.instance._PlayerMoney <= 100 * graves.Length)
            return;

        GameController.instance.SpendMoney(-100 * graves.Length);

        foreach (Student std in graves)
        {
            std.RemoveGraveStone();
            GameController.instance._playerNotorityLevel += 1;
        }            
    }

    // --------------------------------------------------------------------------->

}