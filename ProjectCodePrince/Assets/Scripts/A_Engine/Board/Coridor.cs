using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coridor : MonoBehaviour {

	// Use this for initialization

    public enum CoridorType
    {
        East_West,
        North_South,
        Corss,
        T_North,
        T_South,
        T_East,
        T_West,
        L_North,
        L_South,
        L_East,
        L_West,
        None,
    }

    public TileMap tileMap;

    public int Width;
    public int Height;

    public DungeonRoom.Door[] Doors;

    public int X_Position;
    public int Y_Position;

    private string name;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public Coridor(TileMap _tileMap, int _xPos, int _yPos, int _width, int _height, DungeonRoom.Door[] _doors, CoridorType _type)
    {
        tileMap = _tileMap;

        X_Position = _xPos;
        Y_Position = _yPos;

        Width = _width;
        Height = _height;

        Doors = _doors;
    }

    public Coridor(TileMap _tileMap, int _xPos, int _yPos, int _width, int _height)
    {
        tileMap = _tileMap;

        X_Position = _xPos;
        Y_Position = _yPos;

        Width = _width;
        Height = _height;

    }

    public void Create_East_West(){
        for (int x = X_Position; x < X_Position + Width; x++)
        {
            tileMap.tilesData[x, Y_Position - 1].type.type = TileData.Type.Wall;
            tileMap.tilesData[x, Y_Position + Height].type.type = TileData.Type.Wall;
            for (int y = Y_Position; y < Y_Position + Height; y++)
            {
                tileMap.tilesData[x, y].type.type = TileData.Type.Floor;
                tileMap.tilesData[x, y].type.rotation = Vector3.zero;
            }
        }
    }

    public void Create_North_South()
    {
        for (int x = Y_Position; x < Y_Position + Height; x++)
        {
            tileMap.tilesData[X_Position - 1, x].type.type = TileData.Type.Wall;
            tileMap.tilesData[X_Position + Width, x].type.type = TileData.Type.Wall;
            for (int y = X_Position; y < X_Position + Width; y++)
            {
                tileMap.tilesData[y, x].type.type = TileData.Type.Floor;
                tileMap.tilesData[y, x].type.rotation = Vector3.zero;
            }
        }
    }
}
