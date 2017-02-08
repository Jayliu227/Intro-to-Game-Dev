using UnityEngine;
using System.Collections;


public enum TileType { Empty, PlayGround };

public class Tile {

    public TileType Type { get; set; }

    // coordinates of the tile
   
    public int X { get; protected set; }
    public int Y { get; protected set; }

    World world;

    // have a reference to the building, because later on when the mouse presses the tile, it needs to know the building information.
    public Building building;

    public Tile(World world, int x, int y)
    {
        Type = TileType.Empty;
        this.world = world;
        this.X = x;
        this.Y = y;
    }
}
