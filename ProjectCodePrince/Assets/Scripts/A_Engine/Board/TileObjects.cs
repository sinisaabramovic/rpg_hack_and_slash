using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjects : MonoBehaviour {

    // Use this for initialization
    public TileData.Type type;

    public bool isWalkable = true;
    public float movementCost = 1;

    public Vector3 instantiatePosition;

    public GameObject InfluentedByEnemy;

    public void SetType(TileData.Type _type){
        type = _type;
    }

    public bool ValidateTaype(TileData.Type _type){
        if(_type == type){
            return true;
        }else{
            return false;
        }
    }

    public GameObject GetObject(TileData.Type _type){
        if(_type == type){
            return this.gameObject;
        }else{
            return null;
        }
    }
}
