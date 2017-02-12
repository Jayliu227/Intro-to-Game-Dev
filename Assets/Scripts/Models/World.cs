using UnityEngine;
using System.Collections;

public class World {

    public Tile[,] tiles { get; protected set; }

    public int Width { get; set; }
    public int Height { get; set; }

    public World (int width = 21, int height = 21)
    {
        this.Width = width;
        this.Height = height;
        tiles = new Tile[Width, Height];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                tiles[x, y] = new Tile(this, x, y);
                
            }
        }

        Debug.Log("A World is Created");
    }


    // this is only for testing, later would be changed to another
    // algorithm that would generate a map with appropriate ratio between stone and empty tiles.
    public void Randomize()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (Random.Range(0, 10) == 0)
                    tiles[x,y].Type = TileType.PlayGround;
                else
                {
                    tiles[x, y].Type = TileType.Empty;
                }
            }
        }
    }

    // credit to chit laam tam
    public void ArrangeWorldWithSpecialConfig()
    {

        int tileIndexForPlayGround = Height / 3;

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                tiles[x, y].Type = TileType.Empty; 
            }
        }

        for (int i = tileIndexForPlayGround; i < tileIndexForPlayGround * 2; i++)
        {
            for (int j =  tileIndexForPlayGround; j < tileIndexForPlayGround * 2; j++)
            {
                tiles[i, j].Type = TileType.PlayGround;

            }

        }

    }

    public Tile GetTileAt(int x, int y)
    {
        // check the range
        // IMPORTANT: remember this is an array so be careful about the boundaries!
        if (x >= Width || x < 0 || y < 0 || y >= Height)
        {
            Debug.Log("It is out of range");
            return null;
        }

        return tiles[x, y];

    }  

}
