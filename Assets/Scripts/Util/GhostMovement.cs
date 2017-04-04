using UnityEngine;
using System.Collections;

public class GhostMovement : MonoBehaviour {

    private Vector3 startPoint;
    private Vector3 targetPos;
    private int indexToChase = 0;

    private float wholeJourney;
    private float startTime = 0;
    private float lerpSpeed = 2f;

    void Start () {
        startPoint = transform.position;
        startTime = Time.time;
        indexToChase = Random.Range(0, GameController.instance._playerFacultyNumber - 1);
        if (GameController.instance._playerFacultyNumber != 0)
        {
            targetPos = FacultyFactory.facultyLists[indexToChase].transform.position;
        }
        wholeJourney = Vector3.Distance(startPoint, targetPos);
    }
	
	// Update is called once per frame
	void Update () {
        // lerp is stil hard to use;

        if (GameController.instance._playerFacultyNumber != 0)
        {
            targetPos = FacultyFactory.facultyLists[indexToChase].transform.position;
        }

        float distCovered = (Time.time - startTime) * lerpSpeed;

        float fract = distCovered / wholeJourney;

        transform.position = Vector3.Lerp(startPoint, targetPos, fract);

        vanish();
	}

    void vanish()
    {
        if (Vector3.Distance(transform.position, targetPos) < 1f)
        {
            Invoke("die", 10f);
        }
    }
    void die() { Destroy(gameObject); }
}
