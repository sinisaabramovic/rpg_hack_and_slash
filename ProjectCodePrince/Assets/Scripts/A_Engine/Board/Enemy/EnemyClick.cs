using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClick : MonoBehaviour {

	// Use this for initialization
    private void OnMouseUp()
    {
    

    }

    private void OnMouseDown()
    {
        GetComponent<EnemyPawn>().SetEnemyTile();
    }

}
