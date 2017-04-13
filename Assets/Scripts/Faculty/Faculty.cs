using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Faculty : MonoBehaviour {
    public string facultyType { get; protected set; }

    MouseController _mouseController = null;

    public Vector3 pos { get; protected set; }
    public Vector3 spawningPoint { get; protected set; }
    public Vector3 speed { get; protected set; }
    public float lifeTime { get; set; }
    public int price { get; protected set; }

    protected Vector3 targetDir;
    protected Transform selfTransform;

    // variables for patrolling
    float probeRange = 2f;
    float timePerChange = 3f;
    float angleForTarget;
    float changeSpeed = 10f;
    float timeForMovement = 3f;
    float timeForRest = 3f;
    Quaternion dir = Quaternion.identity;

    public void SetUpFaculty(string _facultyType, Vector3 _spawningPoint, Vector3 _speed, float _lifeTime, int _price)
    {

        facultyType = _facultyType;
        _spawningPoint.z = -10f;
        spawningPoint = _spawningPoint;
        pos = spawningPoint;
        speed = _speed;
        lifeTime = _lifeTime;
        price = _price;
        selfTransform = transform;

        _mouseController = FindObjectOfType<MouseController>();

        gameObject.name = facultyType.ToString();
    }

    protected void DieAfterLifeTime()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("A Faculty Member has died.");
        FacultyFactory.facultyLists.Remove(this);
        if(gameObject != null)
            Destroy(gameObject);
    }

    protected void UpdateMovement()
    {

        // the time per change is equal to the gap time + run time;
        if(timePerChange <= timeForMovement)    // this is the run time. so gap time should be timePerChange - this.
        {
            if (timePerChange <= 0f)
            {
                probeRange = UnityEngine.Random.Range(1f, 2f);
                FindDirection();
            }

            // check if the new targetDir is good to go!
            // -------------------------------------------
            Tile tileUnderTargetDir = _mouseController.GetTileAtWorldCoord(targetDir + selfTransform.position);

            if(tileUnderTargetDir != null)
            {
                if (tileUnderTargetDir.Type != TileType.Empty)
                    ReFindDirection();
            }else
            {
                ReFindDirection();
            }

            //--------------------------------------------

            // if all the conditions are satisfied, we are ready to go!
            selfTransform.position += Vector3.Slerp(Vector3.zero, targetDir, Time.deltaTime / 7f);

        }

        timePerChange -= Time.deltaTime;
    }

    void FindDirection()
    {
        timePerChange = timeForMovement + timeForRest;

        timeForRest = UnityEngine.Random.Range(3f, 20f);
        timeForMovement = UnityEngine.Random.Range(3f, 20f);
        timePerChange = UnityEngine.Random.Range(3f, 20f);

        angleForTarget = UnityEngine.Random.Range(0, 2 * Mathf.PI);

        targetDir = new Vector3(Mathf.Cos(angleForTarget) * probeRange, Mathf.Sin(angleForTarget) * probeRange, 0f);
    }

    void ReFindDirection()
    {
        timePerChange = timeForMovement + timeForRest;

        timeForRest = 0.1f;
        timeForMovement = 0.1f;
        timePerChange = 0.1f;

        angleForTarget = UnityEngine.Random.Range(0, 2 * Mathf.PI);
        
        targetDir = new Vector3(Mathf.Cos(angleForTarget) * probeRange, Mathf.Sin(angleForTarget) * probeRange, 0f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(selfTransform.position, probeRange);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(selfTransform.position, targetDir + selfTransform.position);
    }
}
