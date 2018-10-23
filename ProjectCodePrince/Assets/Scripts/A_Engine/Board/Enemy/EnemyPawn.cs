using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPawn : MonoBehaviour {

    // Use this for initialization
    public string name;
    public List<TileMap.Node> currentPath = null;
    private DiceManager diceManager;
    public TileMap map;
    public Material selectedMaterial;

    private int enemyAffectRadius = 1;
    private bool setTiles = false;

    public void RemoveEnemyTiles(){
        
    }
   
    public void SetEnemyTile()
    {
        
        int result = enemyAffectRadius;
        int x = (int)map.TileCoordToWorldCoord((int)GetComponent<Transform>().position.x, (int)GetComponent<Transform>().position.y).x;
        int y = (int)map.TileCoordToWorldCoord((int)GetComponent<Transform>().position.x, (int)GetComponent<Transform>().position.y).y;

 
        Debug.Log("ENEMY AT POSITION = x = " + x + " y = " + y);
        for (int xi = x - (result); xi < x + (result) + 1; xi++)
        {
            for (int yi = y - (result); yi < y + (result) + 1; yi++)
            {
                if (xi >= 0 && xi < map.mapSizeX && yi >= 0 && yi < map.mapSizeY)
                {
                    if (map.tilesData[xi, yi].type.type == TileData.Type.Floor || map.tilesData[xi, yi].type.type == TileData.Type.Door || map.tilesData[xi, yi].type.type == TileData.Type.Enemy)
                    {
                        //Debug.Log("TRY POSITION = x = " + xi + " y = " + yi);
                        //bool map_genearor = map.isTileReachable(xi, yi, result);
                        if (true)
                        {
                            //Debug.Log("SET AT POSITION = x = " + xi + " y = " + yi);
                            map.tilesData[xi, yi].type.type = TileData.Type.Enemy;
                            map.tilesData[xi, yi].type.setSelected(selectedMaterial);
                            map.tilesData[xi, yi].type.tileVisualPrefab.GetComponent<Clickable>().Enabled = true;
                            map.tilesData[xi, yi].type.tileVisualPrefab.GetComponent<TileObjects>().InfluentedByEnemy = this.gameObject;
                            map.tilesData[xi, yi].type.tileVisualPrefab.GetComponent<TileObjects>().SetType(TileData.Type.Enemy);
                        }
                    }
                }
            }
        }

        map.tilesData[x, y].type.tileVisualPrefab.GetComponent<Clickable>().Enabled = false;
        map.tilesData[x, y].type.isWalkable = false;
    }

	void Start () {
        diceManager = new DiceManager();
        diceManager.Init();


	}
	
	// Update is called once per frame
	void Update () {
        if(setTiles == false){
            SetEnemyTile();
            setTiles = true;
        }

	}
}
