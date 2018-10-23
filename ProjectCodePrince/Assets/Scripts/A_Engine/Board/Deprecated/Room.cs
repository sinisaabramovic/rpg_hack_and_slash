using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    // Use this for initialization
    public BoardManager manager;

    public int width;
    public int height;

    public int i_position;
    public int j_position;

    public void NewRoom(int _w, int _h, int i_pos, int j_pos){
        i_position = i_pos;
        j_position = j_pos;

        width = _w;
        height = _h;

        manager = (BoardManager)FindObjectOfType(typeof(BoardManager));

        // Down Wall
        for (int i = i_position; i < i_position + height; i++)
        {
            manager.tiles[i, j_position].GetComponent<Tile>().tile_Type = Tile.TileType.Wall;
            manager.tiles[i, j_position].GetComponent<Tile>().prev_Tile_Type = Tile.TileType.Wall;
            manager.tiles[i, j_position].GetComponent<Tile>().setWallTile();
        }

        // Up wall
        for (int i = i_position; i < i_position + height; i++)
        {
            manager.tiles[i, i_position + width].GetComponent<Tile>().tile_Type = Tile.TileType.Wall;
            manager.tiles[i, i_position + width].GetComponent<Tile>().prev_Tile_Type = Tile.TileType.Wall;
            manager.tiles[i, i_position + width].GetComponent<Tile>().setWallTile();
        }

        // Left wall
        for (int i = j_position; i <= j_position + width; i++)
        {
            manager.tiles[j_position, i].GetComponent<Tile>().tile_Type = Tile.TileType.Wall;
            manager.tiles[j_position, i].GetComponent<Tile>().prev_Tile_Type = Tile.TileType.Wall;
            manager.tiles[j_position, i].GetComponent<Tile>().setWallTile();
        }

        for (int i = j_position; i <= j_position + width; i++)
        {
            manager.tiles[j_position + height, i].GetComponent<Tile>().tile_Type = Tile.TileType.Wall;
            manager.tiles[j_position + height, i].GetComponent<Tile>().prev_Tile_Type = Tile.TileType.Wall;
            manager.tiles[j_position + height, i].GetComponent<Tile>().setWallTile();
        }

    }
}
