using UnityEngine;
using System.Collections;

public class Retailer : Building {

    public Retailer()
    {
        SetUpBuilding("Retailer", 20f, 500, 5);
    }

    public override void InstantiateBuilding(Vector3 mousePosition)
    {
        //TODO: set the sprite
        this.position = mousePosition;
        go.transform.name = "Retailer";
        go.transform.position = new Vector3(position.x, position.y, 0f);
        go.transform.SetParent(GameObject.Find("_Retailer").transform);

        buildingSpriteRenderer = go.AddComponent<SpriteRenderer>();
        _sprite = Resources.Load<Sprite>("Retailer") as Sprite;

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

        // check to see if this tile can be built on

        if (targetTile.Type == TileType.PlayGround)
        {
            Debug.Log("this is a tile for students to \"PLAY\" !!");
            GameObject.DestroyImmediate(go);
            return;

        }
        else if (targetTile.building != null)
        {
            Debug.Log("this is a tile already occupied by a building!!");
            GameObject.DestroyImmediate(go);
            return;
        }

        this.position = new Vector3(targetTile.X, targetTile.Y, 0f);
        go.transform.position = this.position;

        // enable the sprite renderer
        buildingSpriteRenderer.enabled = true;

        // set the targetTile to be stone;
        targetTile.Type = TileType.PlayGround;
        targetTile.building = this;

        GameController.instance._PlayerMoney -= price;

        isInstalled = true;
    }

    public override void FinishedBuildingMode()
    {
        base.FinishedBuildingMode();
        Student.tuitionAmount *= 1.2f;
    }
}
