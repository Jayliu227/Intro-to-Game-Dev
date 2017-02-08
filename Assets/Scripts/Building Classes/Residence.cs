using UnityEngine;
using System.Collections;
using System;

public class Residence : Building {

    public override void InstantiateBuilding(Vector3 mousePosition)
    {

        SetUpBuilding("Residence", 6f, 200);
        //TODO: set the sprite
        this.position = mousePosition;
        go.transform.name = "Residence";
        go.transform.position = new Vector3(position.x, position.y, 0f);
        go.transform.SetParent(GameObject.Find("House Manager").transform);

        buildingSpriteRenderer = go.AddComponent<SpriteRenderer>();
        Sprite _sprite = Resources.Load<Sprite>("Residence") as Sprite;

        buildingSpriteRenderer.sprite = _sprite;

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

        if(targetTile.Type == TileType.PlayGround)
        {
            Debug.Log("this is a tile for students to \"PLAY\" !!");
            GameObject.DestroyImmediate(go);
            return;
            
        }else if(targetTile.building != null)
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

        isInstalled = true;
    }

    public override void OnBuildingMode()
    {
        
    }

    public override void FinishedBuildingMode()
    {

    }
}
