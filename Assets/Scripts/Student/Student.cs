using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Student : MonoBehaviour {

    // later prob sign a name to it.
    public string name { get; protected set; }

    public static float tuitionAmount = 1;

    private float health = 100;
    public float Health
    {
        get { return health; }
        set
        {
            health -= value; // some other animation here.
        }
    }

    float probeRange = 1f;
    float timePerChange = 3f;
    float angleForTarget;
    float changeSpeed = 10f;
    float timeForMovement = 3f;
    float timeForRest = 3f;

    Vector3 targetDir;
    Transform selfTransform = null;

    // graveStone
    public Sprite[] graveStones;
    public bool isAlive = true;

	// Use this for initialization
	void Start () {
        selfTransform = transform;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!isAlive)
            return;

        UpdateMovement();
        PayForTuition();

        if (health <= 0)
            Die();
	}

    public void Die()
    {  
        GameController.instance.StudentKilled(1);
        StudentFactory._studentList.Remove(this);
        if(!StudentFactory._studentGraveStones.Contains(this))
            StudentFactory._studentGraveStones.Add(this);

        GameController.instance._playerNotorityLevel += 5;

        isAlive = false;
        // we turn them into graveStone
        int randomNum = Mathf.RoundToInt(Random.Range(0, graveStones.Length - 1));
        gameObject.GetComponent<SpriteRenderer>().sprite = graveStones[randomNum];
    }

    public void RemoveGraveStone()
    {
        if (!isAlive)
        {
            Destroy(gameObject);
            if(StudentFactory._studentGraveStones.Contains(this))
                StudentFactory._studentGraveStones.Remove(this);
        }
            
    }

    void PayForTuition()
    {
        GameController.instance._PlayerMoney += Time.deltaTime;
    }

    void UpdateMovement()
    {

        // the time per change is equal to the gap time + run time;
        if (timePerChange <= timeForMovement)    // this is the run time. so gap time should be timePerChange - this.
        {
            if (timePerChange <= 0f)
            {
                probeRange = Random.Range(0f, 1.5f);
                FindDirection();
            }

            // check if the new targetDir is good to go!
            // -------------------------------------------
            Tile tileUnderTargetDir = MouseController.instance.GetTileAtWorldCoord(targetDir + selfTransform.position);

            if (tileUnderTargetDir != null)
            {
                if (tileUnderTargetDir.Type != TileType.PlayGround)
                    ReFindDirection();
            }
            else
            {
                ReFindDirection();
            }

            //--------------------------------------------

            // if all the conditions are satisfied, we are ready to go!
            selfTransform.position += Vector3.Slerp(Vector3.zero, targetDir, Time.deltaTime / 7f);

        }

        timePerChange -= Time.deltaTime;
    }

    public void MoveTowards(Vector3 destination)
    {
        transform.Translate(destination);

        Tile tileUnder = MouseController.instance.GetTileAtWorldCoord(transform.position);

        if (tileUnder.Type != TileType.PlayGround)
            Die();
    }

    void FindDirection()
    {
        timePerChange = timeForMovement + timeForRest;

        timeForRest = Random.Range(3f, 20f);
        timeForMovement = Random.Range(3f, 20f);
        timePerChange = Random.Range(3f, 20f);

        angleForTarget = Random.Range(0, 2 * Mathf.PI);

        targetDir = new Vector3(Mathf.Cos(angleForTarget) * probeRange, Mathf.Sin(angleForTarget) * probeRange, 0f);
    }

    void ReFindDirection()
    {
        timePerChange = timeForMovement + timeForRest;

        timeForRest = 0.1f;
        timeForMovement = 0.1f;
        timePerChange = 0.1f;

        angleForTarget = Random.Range(0, 2 * Mathf.PI);

        targetDir = new Vector3(Mathf.Cos(angleForTarget) * probeRange, Mathf.Sin(angleForTarget) * probeRange, 0f);
    }
}
