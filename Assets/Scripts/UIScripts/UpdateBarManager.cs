using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateBarManager : MonoBehaviour {

    public static UpdateBarManager current;

    public Text[] lines = new Text[5];

    private int currentTime;
    private int time_m;
    private int time_s;

	// Use this for initialization
	void Start () {
        current = this;

        foreach (Text t in lines)
            t.text = "";
	}

    public void UpdateInformationOnBar(string infor)
    {
        lines[4].text = lines[3].text;
        lines[3].text = lines[2].text;
        lines[2].text = lines[1].text;
        lines[1].text = lines[0].text;

        // do conversion here!
        currentTime = (int)Time.realtimeSinceStartup;

        time_m = currentTime / 60;
        time_s = currentTime % 60;

        lines[0].text = "[" + time_m + "m" + time_s + "s] " + infor;
    }
}
