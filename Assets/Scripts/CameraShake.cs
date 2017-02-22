using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public static CameraShake instance;

    float intensity;
    bool isShaking;
    int shakeTimes;

    float Xbase;
    float Ybase;

    float changedX;
    float changedY;

	// Use this for initialization
	void Start () {
        instance = this;
        isShaking = false;
        shakeTimes = 5;

        Xbase = transform.position.x;
        Ybase = transform.position.y;
    }
	
    public void BeginShaking(float intensity, int shakeTimes = 5)
    {
        Xbase = transform.position.x;
        Ybase = transform.position.y;

        this.intensity = intensity;
        this.shakeTimes = shakeTimes;

        isShaking = true;
    }

	// Update is called once per frame
	void Update () {

        if (isShaking)
        {
            changedX = Random.Range(-intensity, intensity);
            changedY = Random.Range(-intensity, intensity);

            transform.position = new Vector3(changedX + Xbase, changedY + Ybase, transform.position.z);

            shakeTimes--;
        }

        if (shakeTimes <= 0 && isShaking)
        {
            isShaking = false;
            transform.position = new Vector3(Xbase, Ybase, transform.position.z);
        }
       
	}
}
