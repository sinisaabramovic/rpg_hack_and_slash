using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class Tile : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    public enum TileType{
        Floor,
        Wall,
        Door,
        Character,
        Enemy,
        Empty,
    }

    public Material m_MaterialNormal;
    public Material m_MaterialSelected;
    public Material m_MaterialCharacter;
    public Material m_MaterialEnemy;

    public Material m_MaterialWall;
    public Material m_MaterialDoor;

    private BoardManager manager;

    public event Action<Tile> OnPointerEnterEvent;
    public event Action<Tile> OnPointerExitEvent;
    public event Action<Tile> OnRightClickEvent;
    public event Action<Tile> OnLeftClickEvent;

    [SerializeField] public GameObject tileObject;
    // Use this for initialization
    [SerializeField] public TileType tile_Type;
    [SerializeField] public TileType prev_Tile_Type;
    private int i_Position;
    private int j_Position;

    private Vector2 xyPosition;

    public bool selected;

    public bool Selected
    {
        get { return selected; }
        set { selected = value; }
    }

    public int I_Position{
        get { return i_Position; }
        set { i_Position = value; }
    }

    public int J_Position
    {
        get { return j_Position; }
        set { j_Position = value; }
    }

    public Vector2 XYPosition{
        get { return xyPosition; }
        set { xyPosition = value; } 
    }

    public TileType Tile_Type
    {
        get { return tile_Type; }
        set { tile_Type = value; }
    }

    public Tile(int _i, int _j){
        i_Position = _i;
        j_Position = _j;

        xyPosition = new Vector2();

        xyPosition.x = _i + 0.5f;
        xyPosition.y = _j + 0.5f;

        tileObject = GetComponent<GameObject>();
        tileObject.transform.position = new Vector3(xyPosition.x, 0, xyPosition.y);
    }

    public void setActive(){
        GetComponent<Renderer>().material = m_MaterialSelected;
        Selected = true;

        if(tile_Type == TileType.Character){
            setCharacterTile();
        }

        if (tile_Type == TileType.Wall)
        {
            setWallTile();
        }
    }

    private void setCharacterTile(){
        GetComponent<Renderer>().material = m_MaterialCharacter;
    }

    public void setWallTile()
    {
        GetComponent<Renderer>().material = m_MaterialWall;
    }

    public void setInActive(){
        if(tile_Type == TileType.Floor || tile_Type == TileType.Character){
            GetComponent<Renderer>().material = m_MaterialNormal;    
        }

        Selected = false;
    }

    public void NewTile(int _i, int _j)
    {
        i_Position = _i;
        j_Position = _j;

        xyPosition = new Vector2();

        xyPosition.x = _i + 0.5f;
        xyPosition.y = _j + 0.5f;

        manager = (BoardManager)FindObjectOfType(typeof(BoardManager));
        transform.position = new Vector3(xyPosition.x, 0, xyPosition.y);

        setInActive();

        switch(tile_Type){
            case TileType.Empty:
                gameObject.active = false;
                break;
            case TileType.Wall:
                GetComponent<Renderer>().material = m_MaterialWall;
                break;
            case TileType.Door:
                GetComponent<Renderer>().material = m_MaterialDoor;
                break;
        }

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        manager.DebugText("KLIK ON");
        if (Selected && tile_Type == TileType.Floor){
            manager.DebugText("KLIK S");
            if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
            {

            }

            if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
            {
                manager.SetActive(I_Position, J_Position);
                manager.setCharacterPosition(I_Position, J_Position);
            }
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

}
