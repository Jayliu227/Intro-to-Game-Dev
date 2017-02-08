using UnityEngine;
using System.Collections;
using System;

public class Hospital : Building {

    public override void InstantiateBuilding(Vector3 mousePosition)
    {

        SetUpBuilding("Hospital", 5f, 100);
        //TODO: set the sprite
        this.position = mousePosition;
        go.transform.name = "Hospital";
        go.transform.position = new Vector3(position.x, position.y , 0f);
        go.transform.SetParent(GameObject.Find("House Manager").transform);

        buildingSpriteRenderer = go.AddComponent<SpriteRenderer>();


        // this is fucking annoying man! spent a whole afternoon figuring out why Resources.Load() doesn't work!!
        Sprite _sprite = Resources.Load<Sprite>("Hospital") as Sprite;

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

        if (targetTile.Type == TileType.PlayGround)
        {
            Debug.Log("this is a tile already occupied by a stone");
            GameObject.DestroyImmediate(go);
            return;

        }

        this.position = new Vector3 (targetTile.X, targetTile.Y, 0f);
        go.transform.position = this.position;


        // set the targetTile to be stone;
        targetTile.Type = TileType.PlayGround;
        targetTile.building = this;

        // enable the sprite renderer
        buildingSpriteRenderer.enabled = true;

        isInstalled = true;
    }

    public override void OnBuildingMode()
    {
        
    }

    public override void FinishedBuildingMode()
    {
        
    }
    
}
