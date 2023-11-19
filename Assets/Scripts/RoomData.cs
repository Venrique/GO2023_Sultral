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
    public bool start = false;
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

    public void setStartRoom()
    {
        this.start = true;
        this.complete = true;
    }

    public void setEndRoom()
    {
        this.end = true;
        this.complete = true;
    }

    public void setCoinRoom()
    {
        this.coin = true;
        this.complete = true;
    }
}
