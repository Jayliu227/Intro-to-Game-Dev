﻿using UnityEngine;
using System.Collections;

public class Prisoner : Faculty {

	// Use this for initialization
	void Awake(){
        Tile tile = WorldController.Instance.World.GetTileAt(Random.Range(1, 20), Random.Range(1, 20));
        while(tile.Type != TileType.Empty)
            tile = WorldController.Instance.World.GetTileAt(Random.Range(1, 20), Random.Range(1, 20));
        
        SetUpFaculty("Prisoner", new Vector3(tile.X, tile.Y, 0f), Vector3.up, 300f, 100);
    }
	
	// Update is called once per frame
	void Update () {
        UpdateMovement();
        DieAfterLifeTime();
    }
}
