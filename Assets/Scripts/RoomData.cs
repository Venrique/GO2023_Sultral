using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData
{
    public enum Obstacle
    {
        NONE,
        BLOCKS,
        WALLS
    }

    public float speed;
    public bool complete = false;
    public bool coin = false;
    public bool end = false;
    public Obstacle obstacle = Obstacle.NONE;

    public int x;
    public int y;
    public int depth;

    public bool doorRight = false;
    public bool doorLeft = false;
    public bool doorUp = false;
    public bool doorDown = false;

    public RoomData(int depth, float speed)
    {
        this.depth = depth;
        this.speed = speed;
    }
}
