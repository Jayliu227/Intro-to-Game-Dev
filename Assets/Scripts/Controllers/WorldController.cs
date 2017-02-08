using System;
using UnityEngine;
using System.Collections.Generic;

public class WorldController : MonoBehaviour {
    
    public static WorldController Instance { get; protected set; }
    public World World { get; protected set; }
    public Dictionary<Tile, GameObject> tileGameObjectDic;

    public Sprite floorSprite;
    public Sprite stoneSprite;

	// Use this for initialization
	void Start () {
        if (Instance != null)
            Debug.Log("There are more than two World Controllers in the world");
        Instance = this;
        World = new World();
        tileGameObjectDic = new Dictionary<Tile, GameObject>();

        //World.Randomize();
        World.ArrangeWorldWithSpecialConfig();


        for (int x = 0; x < World.Width; x++)
        {
            for (int y = 0; y < World.Height; y++)
            {
                GameObject tile_go = new GameObject();
                Tile tile_data = World.GetTileAt(x, y);

                // Add the tile/gameobject pair into the dictionary so that later we can directly access
                // the gameobject with the Tile object as the key
                tileGameObjectDic.Add(tile_data, tile_go);

                tile_go.name = "Tile" + x + "," + y;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0f);

                tile_go.transform.SetParent(this.transform);
                SpriteRenderer tile_sr = tile_go.AddComponent<SpriteRenderer>();

                if(tile_data.Type == TileType.Empty)
                {
                    tile_sr.sprite = floorSprite;
                }else if(tile_data.Type == TileType.PlayGround)
                {
                    tile_sr.sprite = stoneSprite;
                }
            }
        }
	}

    public GameObject GetGameObjectWithTile(Tile tile)
    {
        GameObject _gameObject = tileGameObjectDic[tile];

        if (_gameObject != null)
            return _gameObject;
        else
            return null;
    }

}
