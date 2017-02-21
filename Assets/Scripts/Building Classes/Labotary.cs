using UnityEngine;
using System.Collections;

public class Labotary : Building {

    public Labotary()
    {
        SetUpBuilding("Lab", 300f, 10000, 3);
    }

    public override void InstantiateBuilding(Vector3 mousePosition)
    {
        //TODO: set the sprite
        this.position = mousePosition;
        go.transform.name = "Lab";
        go.transform.position = new Vector3(position.x, position.y, 0f);
        go.transform.SetParent(GameObject.Find("_Lab").transform);

        buildingSpriteRenderer = go.AddComponent<SpriteRenderer>();
        _sprite = Resources.Load<Sprite>("Lab") as Sprite;

        // first set the spriteRenderer to deactive because we want the player to know the house is moving with the cursor.
        buildingSpriteRenderer.enabled = false;

        // TODO: later should deactivate the button on the UI.
    }

    public override void PlaceBuilding(Tile targetTile)
    {

        if (isInstalled == true)
        {
            return;
        }

        if (targetTile.Type == TileType.PlayGround)
        {
            Debug.Log("this is a tile already occupied by a building");
            GameObject.DestroyImmediate(go);
            return;

        }

        this.position = new Vector3(targetTile.X, targetTile.Y, 0f);
        go.transform.position = this.position;


        // set the targetTile to be stone;
        targetTile.Type = TileType.PlayGround;
        targetTile.building = this;

        // enable the sprite renderer
        buildingSpriteRenderer.enabled = true;

        GameController.instance._PlayerMoney -= price;

        isInstalled = true;
    }

    public override void FinishedBuildingMode()
    {
        base.FinishedBuildingMode();
        // unlock special events
    }
}
