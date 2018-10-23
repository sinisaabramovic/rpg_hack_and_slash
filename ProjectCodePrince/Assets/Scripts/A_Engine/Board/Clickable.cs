using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{

    // Use this for initialization
    public int tileX;
    public int tileY;

    public TileMap map;

    private new bool enabled;

    public bool Enabled
    {
        get
        {
            return enabled;
        }

        set
        {
            enabled = value;
        }
    }

    private void OnMouseUp()
    {
           
        if (this.Enabled && map.unitPawn.State == PawnSate.Idle)
        {
            //Debug.Log("UP !!! " + tileX + "   " + tileY);
            bool map_genearor = map.GeneratePathTo(tileX, tileY);
        }

    }

    private void OnMouseDown()
    {
        //Debug.Log("DOWN !!! " + tileX + "   " + tileY);
    }
}
