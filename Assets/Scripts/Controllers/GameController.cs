using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public static GameController instance;

    // for code shortcut
    public InputField ifield;

    public RectTransform NumberTracker;
    Text[] trackerTexts;
    // TODO:
    // 1. get a reference to the information bar and later prob to the billboard that updates the changes.
    // 2. keep track of all the data from the buildings and faculties.
    // 3. when there is a change from the two categories, call the update function in the information bard and billboard to get the value changed.
    // 4. use appropriate values to represent the ratio of contribution that different faculties and buildings have done to the events.
    // 
    // remember to connect this to the event manager and other things as well.

    // DATA for checking condition: they are all static variables so that every other scripts would be able to refer to them when needed.
    //------------------------------------

    public static int studentNum { get; protected set; }

    public Dictionary<string, int> facultyNum { get; protected set; }
    public Dictionary<string, int> buildingNum { get; protected set; }

    //------------------------------------
    // the resources a player has
    private float _playerMoney;
    
    public float _PlayerMoney
    {
        get { return _playerMoney; }
        set { _playerMoney = value; }
    }

    public int _playerFacultyNumber { get; protected set; }
    public int _playerStudentKilled { get; protected set; }
    public int _playerMarketInflation { get; protected set; }    // might be between -1 , 1
    public int _playerNotorityLevel { get; protected set; }      // might be shown as x%

    // for updating the information when selecting the building on the tile
    MouseController _mouseController = null;
    public Text buildingInforText_buildingType;
    public Text buildingInforText_price;

    // Update the information bar
    public Transform _informationBar;
    RectTransform _informationBar_Panel; 

    // Use this for initialization
    void Awake () {
        instance = this;

        // for testing
        _playerMoney = 1000;

        // set the camera to the appropriate position
        Camera.main.transform.position = new Vector3(10f, 10f, -10f);
        Camera.main.orthographicSize = 10f;

        // instantiate part:
        facultyNum = new Dictionary<string, int>();
        buildingNum = new Dictionary<string, int>();

        // -------------------------------------------------
        buildingNum.Add("residence",0);
        buildingNum.Add("facultyRoom",0);
        buildingNum.Add("hospital", 0);
        buildingNum.Add("lab", 0);
        buildingNum.Add("diningHall", 0);
        buildingNum.Add("retailer", 0);

        facultyNum.Add("prisoner", 0);
        facultyNum.Add("madScientist", 0);
        facultyNum.Add("profKiller", 0);
        facultyNum.Add("thief", 0);
        // -------------------------------------------------

        trackerTexts = new Text[NumberTracker.transform.childCount];
        for (int i = 0; i < NumberTracker.transform.childCount; i++)
        {
            trackerTexts[i] = NumberTracker.transform.GetChild(i).GetComponent<Text>();
        }

        _mouseController = FindObjectOfType<MouseController>().GetComponent<MouseController>();
        _informationBar_Panel = _informationBar.GetChild(0).gameObject.GetComponent<RectTransform>();
    }

    public void StudentKilled(int num)
    {
        _playerStudentKilled += num;
        UpdateBarManager.current.UpdateInformationOnBar("There were " + _playerStudentKilled + " dead already!");
    }

    public bool SpendMoney(int amount)
    {
        if(amount> _playerMoney)
        {
            UpdateBarManager.current.UpdateInformationOnBar("Not enough money, sad.");
            return false;
        }

        _playerMoney -= amount;
        return true;
    }

	// Update is called once per frame
	void Update () {

        UpdateData();
        UpdateChecker();
        UpdateInformationBar();

        ShowSelectedBuildingInfor();
        
        // control the secret code window

        if (Input.GetKeyDown(KeyCode.P))
        {
            ifield.gameObject.SetActive((ifield.IsActive()) ? false : true);
        }
	}

    void UpdateChecker()
    {
        trackerTexts[0].text = "Student: " + studentNum + " [" + StudentFactory.maxStudentAmount + "]";
        trackerTexts[1].text = "Faculty Max Numer: " + FacultyFactory.MaxFacultyNum;
        trackerTexts[2].text = "Residence: " + buildingNum["residence"] + " [unlimited]";
        trackerTexts[3].text = "Faculty Room: "+ buildingNum["facultyRoom"] + " [unlimited]";
        trackerTexts[4].text = "Hospital: " + buildingNum["hospital"] + " [5]";
        trackerTexts[5].text = "Lab: " + buildingNum["lab"] +" [3]";
        trackerTexts[6].text = "Dining Hall: " + buildingNum["diningHall"] + " [3]";
        trackerTexts[7].text = "Retailer: " + buildingNum["retailer"] + " [5]";
    }

    // TODO: update the data: money, faculty Number, etc.
    void UpdateData()
    {
        _playerFacultyNumber = FacultyFactory.facultyLists.Count;
        studentNum = StudentFactory._studentList.Count;
        // count items in dictionaries:
        buildingNum["residence"] = GameObject.Find("_Residence").transform.childCount;
        buildingNum["facultyRoom"] = GameObject.Find("_FacultyRoom").transform.childCount;
        buildingNum["hospital"] = GameObject.Find("_Hospital").transform.childCount;
        buildingNum["lab"] = GameObject.Find("_Lab").transform.childCount;
        buildingNum["diningHall"] = GameObject.Find("_DiningHall").transform.childCount;
        buildingNum["retailer"] = GameObject.Find("_Retailer").transform.childCount;

        facultyNum["prisoner"] = GameObject.Find("_PRISONER").transform.childCount;
        facultyNum["madScientist"] = GameObject.Find("_MADSCIENTIST").transform.childCount;
        facultyNum["profKiller"] = GameObject.Find("_PROFKILLER").transform.childCount;
        facultyNum["thief"] = GameObject.Find("_THIEF").transform.childCount;
    }

    // for testing.
    public void Code()
    {
        if (ifield.text == "getMoney")
        {
            _playerMoney += 30000;
            UpdateBarManager.current.UpdateInformationOnBar("Have fun with the game");
        }            

        ifield.text = "";
    }

    void UpdateInformationBar()
    {

        _informationBar_Panel.transform.GetChild(0).GetComponent<Text>().text = "Money: " + ((int)_playerMoney).ToString();

        _informationBar_Panel.transform.GetChild(1).GetComponent<Text>().text = "Faculty Number: " + _playerFacultyNumber.ToString();

        _informationBar_Panel.transform.GetChild(2).GetComponent<Text>().text = "Student Killed: " + _playerStudentKilled.ToString();

        _informationBar_Panel.transform.GetChild(3).GetComponent<Text>().text = "Market Inflation: " + _playerMarketInflation.ToString();

        _informationBar_Panel.transform.GetChild(4).GetComponent<Text>().text = "Notority Level: " + _playerNotorityLevel.ToString();

    }

    // a method that updates the information of the building when the mouse is pressed over the building.
    void ShowSelectedBuildingInfor()
    {
        if (EventSystem.current.IsPointerOverGameObject() == true)
            return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            
            Tile tileUnderSelection = _mouseController.GetTileAtWorldCoord(new Vector3 (mousePosition.x, mousePosition.y, 0));

            if(tileUnderSelection != null && tileUnderSelection.building != null && tileUnderSelection.building.isInstalled == true)
            {
                buildingInforText_price.gameObject.transform.parent.gameObject.SetActive(true);

                Building buildingUnderSelection = tileUnderSelection.building;

                buildingInforText_buildingType.text = buildingUnderSelection.buildingType.ToString();

                buildingInforText_price.text = "$" + buildingUnderSelection.price.ToString();
                //buildingInforText.text = "House Info: /nBuildingType: " + buildingUnderSelection.buildingType + "/nPrice: " + buildingUnderSelection.price;
                //buildingInforText.text = string.Format("House Info: /nBuildingType: {0}/nPrice: {1}", buildingUnderSelection.buildingType, buildingUnderSelection.price);
            }else
            {
                return;
            }
            
        }else
        {
            buildingInforText_price.gameObject.transform.parent.gameObject.SetActive(false);
        }

    }
}
