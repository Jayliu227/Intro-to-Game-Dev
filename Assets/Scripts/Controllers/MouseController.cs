using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class MouseController : MonoBehaviour {

	World world;

	string buildingType;
	BuildingFactory _buildingFactory;
	Building targetBuilding;

    public Dictionary< Tile, Building> tileToBuildingDic;
    // used to collect data and calculate
    public List<Building> buildingsAlreadyInstalled;

    // TODO: maybe later we need different lists to contain different building types for convenient calculation

    // variables to control the mouse position
	Vector3 lastFramePos;
	Vector3 currentFramePos;

    // this is the cursor sprite used when there is no selection
    public GameObject cursorObject;

	// Use this for initialization
	void Start () {
		_buildingFactory = new BuildingFactory ();

        tileToBuildingDic = new Dictionary<Tile, Building>();

        buildingsAlreadyInstalled = new List<Building>();
    }
	
	// Update is called once per frame
	void Update () {
		lastFramePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		lastFramePos.z = 0;


		// use mouse to move the screen
		ScreenMovement();
		// change the color of the selected tile
		UpdateSelection();


		currentFramePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		currentFramePos.z = 0;

	}

	public void SetBuildingTypeAndInstantiate(string buildingType){

		this.buildingType = buildingType;

		targetBuilding = _buildingFactory.GetBuilding (buildingType);

		targetBuilding.InstantiateBuilding (lastFramePos);

    }

    // TODO: this one is later used to destory a building
    // based on the tile it stands on and remember to remove it from the dictionary.
    public void SellBuilding() { }


    // ----------------------- Almost Finished Parts Down Below ---------------------------

    void UpdateSelection()
    {
        // check to see if the cursor is on UI or in the world
        if (EventSystem.current.IsPointerOverGameObject())
        {
            cursorObject.SetActive(false);
            return;
        }

        // if not, then select the tile with the cursor
        Tile tileUnderCursor = GetTileAtWorldCoord(lastFramePos); // this is not the real object but a reference to the object but has the right x and y coord

        GameObject tileUnderCursor_go = WorldController.Instance.GetGameObjectWithTile(tileUnderCursor);

        if (tileUnderCursor != null)
        {
            cursorObject.SetActive(true);
            Vector3 cursorPosition = new Vector3(tileUnderCursor.X, tileUnderCursor.Y, -1f);
            cursorObject.transform.position = cursorPosition;
        }
        else
        {
            cursorObject.SetActive(false);

        }

        //until now the tile is located by the cursor position.

        // change the property of the tile selected
        if (Input.GetMouseButtonDown(0))
        {
            //SpriteRenderer tileUnderCursor_sr = tileUnderCursor_go.GetComponent<SpriteRenderer>();
            //tileUnderCursor_sr.color *= new Color(0, 0, 0);

            if (targetBuilding != null)
            {

                targetBuilding.PlaceBuilding(tileUnderCursor);

                // when the building is placed, add it to the dictionary
                if (!tileToBuildingDic.ContainsKey(tileUnderCursor))
                    tileToBuildingDic.Add(tileUnderCursor, targetBuilding);

                if (!buildingsAlreadyInstalled.Contains(targetBuilding))
                    buildingsAlreadyInstalled.Add(targetBuilding);

                // later when uninstalling the building, remember to remove both the building and the pair of tile and building.

                targetBuilding = null;
                buildingType = null;

            }
        }

    }

    public void ScreenMovement()
	{
		if (Input.GetMouseButton(1))
		{
			Vector3 diff = currentFramePos - lastFramePos;
			Camera.main.transform.Translate(diff);
		}
		
		Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 1.0f, 12.0f);
	}

    public Tile GetTileAtWorldCoord(Vector3 coord)
    {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        return WorldController.Instance.World.GetTileAt(x, y);
    }
}
