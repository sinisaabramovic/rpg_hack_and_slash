using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {

    public Text UIDebugText;

    public GameObject TilePrefab;
    public GameObject[,] tiles;

    public int rowsNum = 10;
    public int columnsNum = 10;

    public bool CanMoveCharacter;

    private DiceManager diceManager;

    public int testI = 0;
    public int testJ = 0;

    public int characterPosition_i;
    public int characterPosition_j;

    public void DebugText(string text){
        UIDebugText.text = text;
    }

    public Room room_1;
	// Use this for initialization
	void Start () {
        CanMoveCharacter = false;
        tiles = new GameObject[rowsNum, columnsNum];
        diceManager = new DiceManager();
        diceManager.Init();

        room_1 = new Room();

        characterPosition_i = testI;
        characterPosition_j = testJ;

        for (int i = 0; i < rowsNum; i++)
        {
            for (int j = 0; j < columnsNum; j++)
            {
                tiles[i, j] = Instantiate(TilePrefab, Vector3.zero, Quaternion.identity);
                tiles[i, j].GetComponent<Tile>().NewTile(i,j);
                tiles[i, j].GetComponent<Tile>().prev_Tile_Type = tiles[i, j].GetComponent<Tile>().tile_Type;
            }
        }

        room_1.NewRoom(5, 5, 2, 4);

        setCharacterPosition(characterPosition_i, characterPosition_j);

	}

    public void SetActive(int _i, int _j)
    {
        if(tiles[_i, _j] != null){
            tiles[_i, _j].GetComponent<Tile>().setActive();
        }

        for (int i = 0; i < rowsNum; i++)
        {
            for (int j = 0; j < columnsNum; j++)
            {
                if(tiles[i, j] != tiles[_i, _j]){
                    tiles[i, j].GetComponent<Tile>().setInActive();   
                }

            }
        }
    }

    public void ResetActive()
    {
        for (int i = 0; i < rowsNum; i++)
        {
            for (int j = 0; j < columnsNum; j++)
            {
                tiles[i, j].GetComponent<Tile>().setInActive();
                tiles[i, j].GetComponent<Tile>().tile_Type = tiles[i, j].GetComponent<Tile>().prev_Tile_Type;
            }
        }

        setCharacterPosition(characterPosition_i, characterPosition_j);
        CanMoveCharacter = false;
    }

    public void setCharacterPosition(int _i, int _j){        
        tiles[_i, _j].GetComponent<Tile>().prev_Tile_Type = tiles[_i, _j].GetComponent<Tile>().tile_Type;
        tiles[_i, _j].GetComponent<Tile>().tile_Type = Tile.TileType.Character;
        tiles[_i, _j].GetComponent<Tile>().setActive();
        characterPosition_i = _i;
        characterPosition_j = _j;
    }

    public void MoveCharacter(string moveDirTest){
        CanMoveCharacter = true;
        Debug.Log(moveDirTest);
    }

    public void CantMoveCharacter(int defaultMoveOnBadResult){
        //Debug.Log("You are stuced in here try move in next Roll");
        DebugText("You are stuced in here try move in next Roll");
        setDiceResult(1);
    }

    private void setDiceResult(int _result){
        ResetActive();
        bool moveIt = false;

        tiles[characterPosition_i, characterPosition_j].GetComponent<Tile>().setActive();
        // Right I
        if(characterPosition_i + _result < rowsNum){
            if(tiles[characterPosition_i + _result, characterPosition_j].GetComponent<Tile>().tile_Type == Tile.TileType.Floor){
                tiles[characterPosition_i + _result, characterPosition_j].GetComponent<Tile>().setActive();
                MoveCharacter("Right");
                moveIt = true;
            }
        }

        // Left
        if (characterPosition_i - _result >= 0)
        {
            if (tiles[characterPosition_i - _result, characterPosition_j].GetComponent<Tile>().tile_Type == Tile.TileType.Floor)
            { 
                tiles[characterPosition_i - _result, characterPosition_j].GetComponent<Tile>().setActive();
                MoveCharacter("Left");
                moveIt = true;
            }
        }

        // Up
        if (characterPosition_j + _result < columnsNum)
        {
            if (tiles[characterPosition_i, characterPosition_j + _result].GetComponent<Tile>().tile_Type == Tile.TileType.Floor)
            {
                tiles[characterPosition_i, characterPosition_j + _result].GetComponent<Tile>().setActive();
                MoveCharacter("Up");
                moveIt = true;
            }
        }

        // Down
        if (characterPosition_j - _result >= 0)
        {
            if (tiles[characterPosition_i, characterPosition_j - _result].GetComponent<Tile>().tile_Type == Tile.TileType.Floor)
            {
                tiles[characterPosition_i, characterPosition_j - _result].GetComponent<Tile>().setActive();
                MoveCharacter("Down");
                moveIt = true;
            }

        }

        if (!moveIt){
            CantMoveCharacter(1);
        }
    }

    public void RollDice(){
        int result = diceManager.RollDices(1);
        //Debug.Log("RESLUT = " + result);
        DebugText("RESLUT = " + result);
        //if (result <= rowsNum + 1 && result <= columnsNum + 1){
        //    setDiceResult(result);
        //}
        setDiceResult(result);

    }

	// Update is called once per frame
	void Update () {
		
	}
}
