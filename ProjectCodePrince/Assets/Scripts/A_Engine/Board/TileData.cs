using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour {

    // Use this for initialization

    public enum Type
    {
        Floor,
        Wall,
        Wall_Corner_LeftUp,
        Wall_Corner_LeftDown,
        Wall_Corner_RightUp,
        Wall_Corner_RightDown,
        Mud,
        Chest,
        Door,
        Empty,
        Wall_Filled,
        Door_Closed,
        Door_Open,
        Door_Left_Tile,
        Door_Right_Tile,
        OuterWall,
        Fog,
        Enemy,
    }

    public string name;

    public TileType type;
    public Clickable clickable;

    public void Awake()
    {
        clickable = GetComponent<Clickable>();
    }

}
