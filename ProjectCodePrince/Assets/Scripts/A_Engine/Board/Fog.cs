using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour {

    public GameObject fogPrefab;
    TileMap tileMap;
    [SerializeField]
    public GameObject[,] fogObjects;

	// Use this for initialization
	void Start () {
        tileMap = GetComponent<TileMap>();
        fogObjects = new GameObject[tileMap.mapSizeX, tileMap.mapSizeY];
        for (int x = 0; x < tileMap.mapSizeX; x++)
        {
            for (int y = 0; y < tileMap.mapSizeY; y++)
            {
                GameObject fogs  = Instantiate(fogPrefab, new Vector3(x, y, 3f), Quaternion.identity);
                fogObjects[x, y] = fogs;
            }
        }


	}

    public void unFog(int x, int y){
        if(x >= 0 && y >= 0 && x <= tileMap.mapSizeX && y <= tileMap.mapSizeY){
            fogObjects[x, y].SetActive(false);
        }
    }

	// Update is called once per frame

	void Update () {

	}
}
