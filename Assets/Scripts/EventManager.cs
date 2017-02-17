using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

    public static EventManager instance;    

    void Awake() {

        // singleton:
        if (instance == null)
            instance = this;
        else
            Destroy(instance);

    }


    void Update()
    {
        
    }
    // TODO: we first need to figure out how to achieve the effects
    // and then try to figure out the relationship between faculty, building and the possibility of each event.

    public void Explosion_Event() {
        // figure out a random point within the map range but exclude playground area.
        int range = 2;
        LayerMask layerMask = (1 << 8 | 1 << 9);
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
        UpdateBarManager.current.UpdateInformationOnBar("Explosion affects" + goInRange.Length + " people!");

        // prob animation and sound effect.
    }

    public void RampantPhantom()
    {
        // make the scene darker
        // add in ghosts of students
        // get rid of the bodies in the middle.

    }

    public void ChemicalAccident_Event() {
        // get the student list and choose a random index to get rid of
        // might effec other students, not sure now.

        // print to the information window.
        // animation and sound effect.
    }
    public void Murder_Event() {
        // simply get rid of one student from the student list.

    }
    public void Runaway_Event() {
        // choose one faculty or student to runaway, not die. 
        // decrease the notorious level.

    }
    public void MissleAttack_Event() {
        // ask for the help to destory students.
        // would be a plane coming across the map in the sky and drop missles to kill
        // both faculty and students.
        // houses are not immune!

        // prob need to instantiate a plane.

    }
    public void PseudoEarthQuake_Event() {
        // make the camera vibrate and kill randomly
        // increase the possibility of educational check.
    }
    public void Suicide_Event() {
        // student lies without increasing the notorious level.
    }
    public void GasPoison_Event() {
        // certain type of faculty and destory, kill students along the whole column or row.
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
