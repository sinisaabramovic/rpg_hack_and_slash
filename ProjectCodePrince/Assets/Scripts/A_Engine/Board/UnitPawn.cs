using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PawnSate{
    Idle,
    Move,
    Attack
}

public class UnitPawn : MonoBehaviour {

    public int tileX;
    public int tileY;
    public TileMap map;
    public Fog fog;

    public List<TileMap.Node> currentPath = null;
    private DiceManager diceManager;
    public Material selectedMaterial;

    public CharCamera mainCamera;

    public PawnSate State = PawnSate.Idle;
    private float speed = 4.2f;
    float moveSpeed = 0.2f;
    Vector3 end = Vector3.zero;
    Vector3 next = Vector3.zero;
    int initCounter = 1;

    public List<GameObject> Enemies;

    bool noMoreMOves = false;

    private void Awake()
    {
        mainCamera = FindObjectOfType<CharCamera>();
        diceManager = new DiceManager();
        diceManager.Init();

    }

    private void DebugDrawLines(){
        if (currentPath != null)
        {
            int currNode = 0;

            while (currNode < currentPath.Count - 1)
            {
                Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y) +
                                   new Vector3(0, 0, 0.2f);
                Vector3 end = map.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].y) +
                                 new Vector3(0, 0, 0.2f);

                Debug.DrawLine(start, end, Color.red);

                currNode++;
            }
        }
    }

    private void Update()
    {
        setFogs();
        Move();
        if(State == PawnSate.Move)
            DebugDrawLines();
    }

    public void Move()
    {        
        if(currentPath != null){            

            float step = speed * Time.deltaTime;
            if(initCounter == currentPath.Count){
                currentPath = null;
                map.setUnit();
                initCounter = 1;
                State = PawnSate.Idle;
                return;
            }
            State = PawnSate.Move;

            if(initCounter == 1){
                ResetTileSelected();
                end = map.TileCoordToWorldCoord(currentPath[initCounter].x, currentPath[initCounter].y) + new Vector3(0, 0, 0.5f);
            }

            if(Vector3.Distance(transform.position, end) <= 0.001f){
                //Debug.Log("MOVE = " + initCounter);
                initCounter++;
                if (initCounter < currentPath.Count && noMoreMOves == false)
                {
                    map.setUnit();
                    end = map.TileCoordToWorldCoord(currentPath[initCounter].x, currentPath[initCounter].y) + new Vector3(0, 0, 0.5f);
                    if (initCounter + 1 < currentPath.Count){                        
                        next = map.TileCoordToWorldCoord(currentPath[initCounter + 1].x, currentPath[initCounter + 1].y) + new Vector3(0, 0, 0.5f);
                    }
                    int _x = (int)GetComponent<Transform>().position.x;
                    int _y = (int)GetComponent<Transform>().position.y;
                    setFogs(_x, _y);
                }
            }
            if (map.tilesData[(int)end.x, (int)end.y].type.type == TileData.Type.Enemy)
            {
                Debug.Log("ON ENEMY GROUND!!!!!");
                // COMBAT PART !!!!!
                noMoreMOves = true;
            }

            transform.position = Vector3.MoveTowards(transform.position, end, step);
        }
    }


    public void MoveNextTile(){
        if (currentPath == null)
            return;
        currentPath.RemoveAt(0);
        GetComponent<Transform>().position = map.TileCoordToWorldCoord(currentPath[0].x, currentPath[0].y) + new Vector3(0, 0, 0.5f);
        int _x = (int)GetComponent<Transform>().position.x;
        int _y = (int)GetComponent<Transform>().position.y;
        setFogs(_x, _y);
        ResetTileSelected();
        if(currentPath.Count == 1){
            currentPath = null;
            map.setUnit();
        }
    }

    public void ResetTileSelected()
    {
        for (int x = 0; x < map.mapSizeX; x++)
        {
            for (int y = 0; y < map.mapSizeY; y++)
            {
                map.tilesData[x, y].type.unSelected();
                if (map.tilesData[x, y].type.tileVisualPrefab.GetComponent<Clickable>() != null){
                    map.tilesData[x, y].type.tileVisualPrefab.GetComponent<Clickable>().Enabled = false;    
                }
            }
        }
    }

    public void setFogs(int x, int y)
    {
        fog.unFog(x, y);

        for (int i = x - 3; i < x + 4; i++)
        {
            for (int j = y - 3; j < y + 4; j++)
            {
                fog.unFog(i, j);
            }
        }
    }

    public void setFogs(){
        fog.unFog(tileX, tileY);

        for (int i = tileX - 3; i < tileX + 4; i++){
            for (int j = tileY - 3; j < tileY + 4; j++)
            {
                fog.unFog(i, j);
            }
        }
    }

    private GameObject GetClosestEnemy(){
        Enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        if(Enemies.Count > 0){
            foreach(GameObject ax in Enemies){
                //Enemies.Sort((unit1, unit2) =>(this.transform.position - unit1.transform.position).sqrMagnitude.CompareTo((this.transform.position - unit2.transform.position).sqrMagnitude));
            }
            Enemies.Sort(delegate (GameObject a, GameObject b) { return (Vector3.Distance(transform.position, a.transform.position)).CompareTo(Vector3.Distance(transform.position, b.transform.position)); });
            return Enemies[0];
        }

        return null;
    }

    public void RollDice(){

        noMoreMOves = false;

        ResetTileSelected();

        GameObject closesetEnemy = GetClosestEnemy();

        if(closesetEnemy != null){
            Debug.Log("ENEMY!!!!! NUM OF ENEMIES = " + Enemies.Count);
        }

        int result = diceManager.RollDices(1);
        mainCamera.setUpDistnce(result/2);
        int x = (int)map.TileCoordToWorldCoord((int)GetComponent<Transform>().position.x, (int)GetComponent<Transform>().position.y).x;
        int y = (int)map.TileCoordToWorldCoord((int)GetComponent<Transform>().position.x, (int)GetComponent<Transform>().position.y).y;
        tileX = x;
        tileY = y;
        Debug.Log("ROLL = " + result);
        for (int xi = x - (result); xi < x + (result) + 1; xi++)
        {
            for (int yi = y - (result); yi < y + (result) + 1; yi++)
            {
                if (xi >= 0 && xi < map.mapSizeX && yi >= 0 && yi < map.mapSizeY)
                {
                    if (map.tilesData[xi, yi].type.type == TileData.Type.Floor || map.tilesData[xi, yi].type.type == TileData.Type.Door || map.tilesData[xi, yi].type.type == TileData.Type.Enemy)
                    {
                        bool map_genearor = map.isTileReachable(xi, yi, result);
                        if (map_genearor)
                        {
                            map.tilesData[xi, yi].type.setSelected(selectedMaterial);
                            map.tilesData[xi, yi].type.tileVisualPrefab.GetComponent<Clickable>().Enabled = true;
                        }
                    }
                }

                fog.unFog(xi+1, yi+1);
            }
        }

    }
}
