using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    // TODO:
    // 1. get a reference to the information bar and later prob to the billboard that updates the changes.
    // 2. keep track of all the data from the buildings and faculties.
    // 3. when there is a change from the two categories, call the update function in the information bard and billboard to get the value changed.
    // 4. use appropriate values to represent the ratio of contribution that different faculties and buildings have done to the events.
    // 
    // remember to connect this to the event manager and other things as well.

    // the resources a player has
    public int _playerMoney { get; protected set; }
    public int _playerFacultyNumber { get; protected set; }
    public int _playerStudentKilled { get; protected set; }
    public int _playerMarketInflation { get; protected set; }    // might be between -1 , 1
    public int _playerNotorityLevel { get; protected set; }      // might be shown as x%

    // create lists to contain the type of buildings existing in the scene.
    List<Residence> _residences;
    List<Hospital> _hospitals;

    // for updating the information when selecting the building on the tile
    MouseController _mouseController = null;
    public Text buildingInforText_buildingType;
    public Text buildingInforText_price;

    // Use this for initialization
    void Start () {

        // set the camera to the appropriate position
        Camera.main.transform.position = new Vector3(10f, 10f, -10f);
        Camera.main.orthographicSize = 10f;

        _residences = new List<Residence>();
        _hospitals = new List<Hospital>();

        _mouseController = FindObjectOfType<MouseController>().GetComponent<MouseController>();
	}
	
	// Update is called once per frame
	void Update () {

        ShowSelectedBuildingInfor();

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
