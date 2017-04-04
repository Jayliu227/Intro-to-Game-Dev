using UnityEngine;
using UnityEngine.UI;

public class Guidance : MonoBehaviour {

    public bool isGuided = true;
    private bool guide1 = false;
    private bool guide2 = false;
    private bool guide3 = false;
    [SerializeField]
    public GameObject[] panels;

    void Start(){
        if (isGuided)
        {
            guide1 = true;
            guide2 = false;
            guide3 = false;

            panels[0].SetActive(true);
            panels[1].SetActive(false);
            panels[2].SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Debug.Log(guide1 + " " + guide2 + " "+ guide3);
        if (guide1)
        {
            UseEvents();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                panels[0].SetActive(false);
                guide1 = false;
                guide2 = true;
            }
        }else if (guide2)
        {
            BuildBuilding();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                panels[1].SetActive(false);
                guide2 = false;
                guide3 = true;
            }
        }else if (guide3)
        {
            ShowGoal();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                panels[2].SetActive(false);
                guide3 = false;
            }
        }    
    }

    void UseEvents()
    {
        if(!panels[0].activeSelf)
            panels[0].SetActive(true);
        panels[0].transform.GetChild(0).GetComponent<Text>().text = "When these events are light up\nPress corresponding key and kill students!\n(ESC)";
    }

    void BuildBuilding()
    {
        if (!panels[1].activeSelf)
            panels[1].SetActive(true);
        panels[1].transform.GetChild(0).GetComponent<Text>().text = "Click here to build buildings\nThey would give you some bonus!\n(ESC)";
    }

    void ShowGoal()
    {
        if (!panels[2].activeSelf)
            panels[2].SetActive(true);
        panels[2].transform.GetChild(0).GetComponent<Text>().text = "Kill as many students as possible\nRemember to pay attention\nto educational check!\n(ESC)";
    }
}
