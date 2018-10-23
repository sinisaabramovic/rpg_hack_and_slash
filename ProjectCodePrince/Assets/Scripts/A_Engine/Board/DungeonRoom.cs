using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour {

    // Use this for initialization

    public enum DoorType{
        East,
        West,
        North,
        South,
        None,
    }

    public struct Door{
        public DoorType DoorPosition;
        public bool Open;
    }

    public TileMap tileMap;

    public int Width;
    public int Height;

    public Door[] Doors;

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

    public DungeonRoom(TileMap _tileMap, Door[] _doors){
        tileMap = _tileMap;
        Doors = _doors;
    }

    public DungeonRoom(TileMap _tileMap, int _xPos, int _yPos, Door[] _doors)
    {
        tileMap = _tileMap;

        X_Position = _xPos;
        Y_Position = _yPos;

        Doors = _doors;
    }

    public DungeonRoom(TileMap _tileMap, int _xPos, int _yPos, int _width, int _height, Door[] _doors)
    {
        tileMap = _tileMap;

        X_Position = _xPos;
        Y_Position = _yPos;

        Width = _width;
        Height = _height;

        Doors = _doors;
    }

    private bool CanCreate () {
		
        if(Width < 4 || Height < 4){
            return false;
        }

        if(X_Position + Width / 2 > tileMap.mapSizeX){
            return false;
        }

        if (X_Position - Width / 2 < 0)
        {
            return false;
        }

        if (Y_Position + Height / 2 > tileMap.mapSizeY)
        {
            return false;
        }

        if (Y_Position - Height / 2 < 0)
        {
            return false;
        }

        return true;

	}

    public void CreateInnerRoom(int _w, int _h, int _x, int _y){

        for (int x = _x - (_w / 2); x < _x + (_w/2); x++)
        {
            for (int y = _y - (_h/2); y < _y + (_h/2); y++)
            {
                tileMap.tilesData[x, y].type.type = TileData.Type.Wall_Filled;
                tileMap.tilesData[x, y].type.rotation = Vector3.zero;
            }
        }

        // Create corners

        // Left Up corner
        tileMap.tilesData[_x - (_w / 2), _y - (_h / 2)].type.type = TileData.Type.Wall_Corner_LeftUp;
        tileMap.tilesData[_x - (_w / 2), _y - (_h / 2)].type.rotation = Vector3.zero;

        // Left Down corner
        tileMap.tilesData[_x - (_w / 2), _y + (_h / 2)-1].type.type = TileData.Type.Wall_Corner_LeftDown;
        tileMap.tilesData[_x - (_w / 2), _y + (_h / 2)-1].type.rotation = Vector3.zero;

        // Right Up
        tileMap.tilesData[_x + (_w / 2)-1, _y - (_h / 2)].type.type = TileData.Type.Wall_Corner_RightUp;
        tileMap.tilesData[_x + (_w / 2)-1, _y - (_h / 2)].type.rotation = Vector3.zero;

        // Right Down
        tileMap.tilesData[_x + (_w / 2)-1, _y + (_h / 2)-1].type.type = TileData.Type.Wall_Corner_RightDown;
        tileMap.tilesData[_x + (_w / 2)-1, _y + (_h / 2)-1].type.rotation = Vector3.zero;

    }

    private void CreateFloors(int _w, int _h, int _x, int _y)
    {

        for (int x = _x - (_w / 2); x < _x + (_w / 2) + 1; x++)
        {
            for (int y = _y - (_h / 2); y < _y + (_h / 2) + 1; y++)
            {
                tileMap.tilesData[x, y].type.type = TileData.Type.Floor;
                tileMap.tilesData[x, y].type.rotation = Vector3.zero;
            }
        }

    }
	
	// Update is called once per frame
    public void Create () {
		
        if(CanCreate()){
            // Rows
            CreateFloors(Width, Height, X_Position, Y_Position);

            for (int x = X_Position - Width / 2; x < X_Position + (Width / 2); x++){
                tileMap.tilesData[x, Y_Position - Height / 2].type.type = TileData.Type.Wall;
                tileMap.tilesData[x, Y_Position + Height / 2].type.type = TileData.Type.Wall;
            }

            // Columns
            for (int y = Y_Position - Height / 2; y <= Y_Position + (Height / 2); y++)
            {
                tileMap.tilesData[X_Position - Width / 2, y].type.type = TileData.Type.Wall;
                tileMap.tilesData[X_Position + Width / 2, y].type.type = TileData.Type.Wall;

                tileMap.tilesData[X_Position + Width / 2, y].type.rotation.z = 90f;
                tileMap.tilesData[X_Position + Width / 2, y].type.rotation.x = 0f;
                tileMap.tilesData[X_Position + Width / 2, y].type.rotation.y = 0f;

                tileMap.tilesData[X_Position - Width / 2, y].type.rotation.z = 90f;
                tileMap.tilesData[X_Position - Width / 2, y].type.rotation.x = 0f;
                tileMap.tilesData[X_Position - Width / 2, y].type.rotation.y = 0f;
            }

            // Create corners

            // Left Up corner
            tileMap.tilesData[X_Position - Width/2, Y_Position - Height / 2].type.type = TileData.Type.Wall_Corner_LeftUp;
            tileMap.tilesData[X_Position - Width/2, Y_Position - Height / 2].type.rotation = Vector3.zero;

            // Left Down corner
            tileMap.tilesData[X_Position - Width/2, Y_Position + Height / 2].type.type = TileData.Type.Wall_Corner_LeftDown;
            tileMap.tilesData[X_Position - Width/2, Y_Position + Height / 2].type.rotation = Vector3.zero;

            // Right Up
            tileMap.tilesData[X_Position + Width/2, Y_Position - Height / 2].type.type = TileData.Type.Wall_Corner_RightUp;
            tileMap.tilesData[X_Position + Width/2, Y_Position - Height / 2].type.rotation = Vector3.zero;

            // Right Down
            tileMap.tilesData[X_Position + Width/2, Y_Position + Height / 2].type.type = TileData.Type.Wall_Corner_RightDown;
            tileMap.tilesData[X_Position + Width/2, Y_Position + Height / 2].type.rotation = Vector3.zero;


            CreateDoors();
        }
	}

    public void CreateDoors(){

        foreach(Door d in Doors){
            if(d.DoorPosition == DoorType.East){
                //tileMap.tilesData[X_Position + Width / 2, Y_Position].type.type = TileData.Type.Door;
                tileMap.tilesData[X_Position + Width / 2, Y_Position].type.door = d;
                if(d.Open){
                    tileMap.tilesData[X_Position + Width / 2, Y_Position].type.type = TileData.Type.Door_Open;
                }else{
                    tileMap.tilesData[X_Position + Width / 2, Y_Position].type.type = TileData.Type.Door_Closed;
                }
                tileMap.tilesData[X_Position + Width / 2, Y_Position].type.rotation = new Vector3(0f, 0f, 90f);
                tileMap.tilesData[X_Position + Width / 2, Y_Position + 1].type.type = TileData.Type.Door_Left_Tile;
                tileMap.tilesData[X_Position + Width / 2, Y_Position - 1].type.type = TileData.Type.Door_Right_Tile;
            }

            if(d.DoorPosition == DoorType.West){
                //tileMap.tilesData[X_Position - Width / 2, Y_Position].type.type = TileData.Type.Door;
                tileMap.tilesData[X_Position - Width / 2, Y_Position].type.door = d;
                if (d.Open)
                {
                    tileMap.tilesData[X_Position - Width / 2, Y_Position].type.type = TileData.Type.Door_Open;
                }
                else
                {
                    tileMap.tilesData[X_Position - Width / 2, Y_Position].type.type = TileData.Type.Door_Closed;
                }
                tileMap.tilesData[X_Position - Width / 2, Y_Position].type.rotation = new Vector3(0f, 0f, 180f);
                tileMap.tilesData[X_Position - Width / 2, Y_Position + 1].type.type = TileData.Type.Door_Left_Tile;
                tileMap.tilesData[X_Position - Width / 2, Y_Position - 1].type.type = TileData.Type.Door_Right_Tile;
            }

            if (d.DoorPosition == DoorType.South)
            {
                //tileMap.tilesData[X_Position, Y_Position + Height / 2].type.type = TileData.Type.Door;
                tileMap.tilesData[X_Position, Y_Position + Height / 2].type.door = d;
                if(d.Open){
                    tileMap.tilesData[X_Position, Y_Position + Height / 2].type.type = TileData.Type.Door_Open;
                }else{
                    tileMap.tilesData[X_Position, Y_Position + Height / 2].type.type = TileData.Type.Door_Closed;
                }
                tileMap.tilesData[X_Position, Y_Position + Height / 2].type.rotation = new Vector3(0f, 0f, 180f);
                tileMap.tilesData[X_Position + 1, Y_Position + Height / 2].type.type = TileData.Type.Door_Left_Tile;
                tileMap.tilesData[X_Position - 1, Y_Position + Height / 2].type.type = TileData.Type.Door_Right_Tile;
            }

            if (d.DoorPosition == DoorType.North)
            {
                //tileMap.tilesData[X_Position, Y_Position - Height / 2].type.type = TileData.Type.Door;
                tileMap.tilesData[X_Position, Y_Position - Height / 2].type.door = d;

                if (d.Open)
                {
                    tileMap.tilesData[X_Position, Y_Position - Height / 2].type.type = TileData.Type.Door_Open;
                }
                else
                {
                    tileMap.tilesData[X_Position, Y_Position - Height / 2].type.type = TileData.Type.Door_Closed;
                }
                tileMap.tilesData[X_Position, Y_Position - Height / 2].type.rotation = new Vector3(0f, 0f, 360f);
                tileMap.tilesData[X_Position + 1, Y_Position - Height / 2].type.type = TileData.Type.Door_Left_Tile;
                tileMap.tilesData[X_Position - 1, Y_Position - Height / 2].type.type = TileData.Type.Door_Right_Tile;
            }
        }
    }
}
