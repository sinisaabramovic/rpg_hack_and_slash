using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TileType{

    // Use this for initialization


    public string name;
    public GameObject tileVisualPrefab;
    public TileData.Type type;

    public bool isWalkable = true;
    public float movementCost = 1;

    public Material selectedMaterial;
    private Material normalMaterial;

    public DungeonRoom.Door door;
    public Vector3 rotation = Vector3.zero;


    public bool selected;

    public void setNormalMaterial(){
        normalMaterial = tileVisualPrefab.GetComponent<Renderer>().material;
    }

    public void unSelected(){
        tileVisualPrefab.GetComponent<Renderer>().material = normalMaterial;
    }

    public void setSelected(Material selected){
        selectedMaterial = selected;
        tileVisualPrefab.GetComponent<Renderer>().material = selectedMaterial; 
    }
}
