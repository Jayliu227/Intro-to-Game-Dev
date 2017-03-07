using UnityEngine;
using System.Collections;

public class Thief : Faculty {

    // Use this for initialization
    void Awake()
    {
        Tile tile = WorldController.Instance.World.GetTileAt(Random.Range(1, 20), Random.Range(1, 20));
        while (tile.Type != TileType.Empty)
            tile = WorldController.Instance.World.GetTileAt(Random.Range(1, 20), Random.Range(1, 20));

        SetUpFaculty("Thief", new Vector3(tile.X, tile.Y, 0f), Vector3.up, 450f, 380);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        DieAfterLifeTime();
    }
}
