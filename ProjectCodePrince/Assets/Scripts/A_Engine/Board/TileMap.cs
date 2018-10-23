using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {

    public enum MoveType{
        Four_Direction,
        Eight_Direction,
    }

    public class Node{
        public List<Node> neighbours;
        public int x;
        public int y;

        public Node(){
            neighbours = new List<Node>();
        }

        public float DistanceTo(Node n){
            return Vector2.Distance(new Vector2(x, y), new Vector2(n.x, n.y));
        }
    }

    public MoveType MoveTypeCharacter;

    public TileObjects[] tileObjects;
    public TileData[,] tilesData;
    public UnitPawn unitPawn;

    Node[,] graph;

    public int mapSizeX = 100;
    public int mapSizeY = 100;


    private void Awake()
    {

        setUnit();
        InitMap();
    }

    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    }


    public void setUnit()
    {
        unitPawn.tileX = (int)unitPawn.GetComponent<Transform>().position.x;
        unitPawn.tileY = (int)unitPawn.GetComponent<Transform>().position.y;
        unitPawn.map = this;
    }

    public void InitMap(){
        GenerateMapData();
        GeneratePathFind();
        GenerateMapVisual();
    }

    // This is for generating map

    public void GenerateMapData()
    {
        tilesData = new TileData[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tilesData[x, y] = new TileData();
                tilesData[x, y].type = new TileType();
                tilesData[x, y].type.type = TileData.Type.OuterWall;
            }
        }

        DungeonRoom.Door[] doors;
        doors = new DungeonRoom.Door[2];
        doors[0].Open = true;

        doors[0].DoorPosition = DungeonRoom.DoorType.South;
        doors[1].DoorPosition = DungeonRoom.DoorType.East;
        doors[1].Open = true;

        DungeonRoom room_1 = new DungeonRoom(this, 7, 7, 12, 14, doors);
        room_1.Create();
        room_1.CreateInnerRoom(4, 4, 6, 6);

        DungeonRoom.Door[] door2 = new DungeonRoom.Door[1];
        door2[0].DoorPosition = DungeonRoom.DoorType.South;
        door2[0].Open = false;
        DungeonRoom room_2 = new DungeonRoom(this, 20, 4, 8, 8, door2);
        room_2.Create();

        Coridor corridor1 = new Coridor(this,22,13,5,2);
        corridor1.Create_East_West();

        DungeonPattern dungeonPattern = new DungeonPattern();
        dungeonPattern.Create(this);
    }
           
    public void GenerateMapVisual(){

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {                
                GameObject gameObj = null;
                foreach(TileObjects to in tileObjects){
                    if(to.ValidateTaype(tilesData[x, y].type.type)){
                        gameObj = to.GetObject(tilesData[x, y].type.type);
                        break;
                    }
                }

                float z_offset = 0f;

                GameObject go = Instantiate(gameObj, new Vector3(x, y, z_offset), Quaternion.identity);
                go.transform.Rotate(tilesData[x, y].type.rotation);
                Clickable ct = go.GetComponent<Clickable>();
                tilesData[x, y].type.isWalkable = go.GetComponent<TileObjects>().isWalkable;

                if(tilesData[x, y].type.type == TileData.Type.Door || tilesData[x, y].type.type == TileData.Type.Door_Open || tilesData[x, y].type.type == TileData.Type.Door_Closed){
                    tilesData[x, y].type.isWalkable = tilesData[x, y].type.door.Open;
                }

                tilesData[x, y].type.movementCost = go.GetComponent<TileObjects>().movementCost;
                tilesData[x, y].type.tileVisualPrefab = go;
                tilesData[x, y].type.setNormalMaterial();
                if(ct != null){
                    tilesData[x, y].type.tileVisualPrefab.GetComponent<Clickable>().tileX = x;
                    tilesData[x, y].type.tileVisualPrefab.GetComponent<Clickable>().tileY = y;
                    tilesData[x, y].type.tileVisualPrefab.GetComponent<Clickable>().map = this;
                    tilesData[x, y].type.tileVisualPrefab.GetComponent<Clickable>().Enabled = false;
                }
            }
        }
    }

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // All path related things
    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    // Helper functions
    float CostToEnterTile(int sourceX, int sourceY, int targerX, int targetY)
    {

        TileType tx = tilesData[targerX, targetY].type;

        float cost = tx.movementCost;

        if (sourceX != targerX && sourceY != targetY)
        {
            cost += 0.01f;
        }

        return cost;
    }

    bool IsWalkable(int x, int y)
    {
        TileType tx = tilesData[x, y].type;
        return tx.isWalkable;
    }

    // Path generator

    void GeneratePathFind()
    {
        graph = new Node[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                
                if(MoveTypeCharacter == MoveType.Four_Direction){
                    // 4 Way connection
                    if (x > 0)
                        graph[x, y].neighbours.Add(graph[x - 1, y]);
                    if (x < mapSizeX - 1)
                        graph[x, y].neighbours.Add(graph[x + 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x, y - 1]);
                    if (y < mapSizeY - 1)
                        graph[x, y].neighbours.Add(graph[x, y + 1]);
                }else if(MoveTypeCharacter == MoveType.Eight_Direction){
                    // 8 Way connection
                    if (x > 0)
                    {
                        graph[x, y].neighbours.Add(graph[x - 1, y]);
                        if (y > 0)
                            graph[x, y].neighbours.Add(graph[x - 1, y - 1]);
                        if (y < mapSizeY - 1)
                            graph[x, y].neighbours.Add(graph[x - 1, y + 1]);
                    }

                    if (x < mapSizeX - 1)
                    {
                        graph[x, y].neighbours.Add(graph[x + 1, y]);
                        if (y > 0)
                            graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
                        if (y < mapSizeY - 1)
                            graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
                    }

                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x, y - 1]);
                    if (y < mapSizeY - 1)
                        graph[x, y].neighbours.Add(graph[x, y + 1]);
                }

            }
        }
    }


    public bool GeneratePathTo(int x, int y){

        unitPawn.currentPath = null;
        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        Node source = graph[unitPawn.tileX, unitPawn.tileY];
        Node target = graph[x, y];


        List<Node> unvisited = new List<Node>();

        dist[source] = 0;
        prev[source] = null;

        foreach(Node v in graph){
            if(v != source){
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }
            unvisited.Add(v);
        }

        while(unvisited.Count > 0){
            Node u = null;

            foreach(Node possible in unvisited){
                if(u == null || dist[possible] < dist[u]){
                    u = possible;
                }
            }

            if(u == target){
                break;
            }

            unvisited.Remove(u);

            foreach(Node v in u.neighbours){
                if (IsWalkable(v.x, v.y)){
                    float alt = dist[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
                    if (alt < dist[v])
                    {
                        dist[v] = alt;
                        prev[v] = u;
                    }
                }
            }
        }

        if(prev[target] == null){
            // No route between target and sourcce
            return false;
        }

        List<Node> currentPath = new List<Node>();

        Node curr = target;

        while(curr != null){
            currentPath.Add(curr);
            curr = prev[curr];
        }

        currentPath.Reverse();
        unitPawn.currentPath = currentPath;

        return true;
    }

    public bool isTileReachable(int x, int y, int dice_result)
    {
        
        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        Node source = graph[unitPawn.tileX, unitPawn.tileY];
        Node target = graph[x, y];


        List<Node> unvisited = new List<Node>();

        dist[source] = 0;
        prev[source] = null;

        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }
            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            Node u = null;

            foreach (Node possible in unvisited)
            {
                if (u == null || dist[possible] < dist[u])
                {
                    u = possible;
                }
            }

            if (u == target)
            {
                break;
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                if (IsWalkable(v.x, v.y))
                {
                    float alt = dist[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
                    if (alt < dist[v])
                    {
                        dist[v] = alt;
                        prev[v] = u;
                    }
                }
            }
        }

        if (prev[target] == null)
        {
            // No route between target and sourcce
            return false;
        }

        List<Node> currentPath = new List<Node>();

        Node curr = target;

        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        //currentPath.Reverse();
        int move_for = dice_result;
        //if(move_for <= 1){
        //    move_for++;
        //}
        if (move_for + 1 >= currentPath.Count){
            return true;
        }else{
            return false;
        }

    }

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
