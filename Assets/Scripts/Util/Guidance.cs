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
        panels[0].transform.GetChild(0).GetComponent<Text>().text = "When these events are red,\npress corresponding key to kill students!\n(ESC)";
    }

    void BuildBuilding()
    {
        if (!panels[1].activeSelf)
            panels[1].SetActive(true);
        panels[1].transform.GetChild(0).GetComponent<Text>().text = "Click here to create buildings.\nThey increase your maximum students and faculty, along with other bonuses!\n(ESC)";
    }

    void ShowGoal()
    {
        if (!panels[2].activeSelf)
            panels[2].SetActive(true);
        panels[2].transform.GetChild(0).GetComponent<Text>().text = "Kill as many students as possible.\nBut remember to pass your educational checks too\n(ESC)";
    }
}
