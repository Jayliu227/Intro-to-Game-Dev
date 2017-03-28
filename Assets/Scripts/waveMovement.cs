using UnityEngine;
using System.Collections;

public class waveMovement : MonoBehaviour {

    public static int index = 0;
    Vector3 center;
    Vector3 headingToCenter;
    Vector3 offset = new Vector3(-0.5f, 0 , 0);
    float speed = 0.5f;
    float prepareTime;

	// Use this for initialization
	void Start () {
        transform.position = new Vector3((index % 2 == 0) ? 0f : -1f, index - 6, 1f);

        switch (index % 3)
        {
            case 0: center = transform.position - offset;
                prepareTime = 0f;
                break;
            case 1: center = transform.position + offset;
                prepareTime = 50f;
                break;
            case 2: center = transform.position + 0.5f * offset;
                prepareTime = 100;
                break;
        }

        //center = (index % 3 == 0)? transform.position - offset: transform.position + offset;
        index++;
        //prepareTime = (index % 2 == 0) ? 0f : 50f;
    }
	
	// Update is called once per frame
	void Update () {
        prepareTime--;

        if(prepareTime >= 0)
        {
            return;
        }

        headingToCenter = (transform.position - center).normalized;
        Vector3 dir = Vector3.Cross(headingToCenter, Vector3.forward);
        transform.position += dir * speed * Time.deltaTime;
    }
}
