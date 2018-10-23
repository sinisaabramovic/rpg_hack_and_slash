using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonPattern : MonoBehaviour {

	// Use this for initialization
    public void Create (TileMap tileMap) {
        
        tileMap.tilesData[19, 9].type.type = TileData.Type.Floor;
        tileMap.tilesData[21, 9].type.type = TileData.Type.Floor;
        tileMap.tilesData[20, 9].type.type = TileData.Type.Floor;

        tileMap.tilesData[19, 10].type.type = TileData.Type.Floor;
        tileMap.tilesData[19, 11].type.type = TileData.Type.Floor;
        tileMap.tilesData[19, 12].type.type = TileData.Type.Floor;
        tileMap.tilesData[19, 13].type.type = TileData.Type.Floor;
        tileMap.tilesData[19, 14].type.type = TileData.Type.Floor;
        tileMap.tilesData[19, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[19, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[19, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[19, 18].type.type = TileData.Type.Floor;

        tileMap.tilesData[20, 10].type.type = TileData.Type.Floor;
        tileMap.tilesData[20, 11].type.type = TileData.Type.Floor;
        tileMap.tilesData[20, 12].type.type = TileData.Type.Floor;
        tileMap.tilesData[20, 13].type.type = TileData.Type.Floor;
        tileMap.tilesData[20, 14].type.type = TileData.Type.Floor;
        tileMap.tilesData[20, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[20, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[20, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[20, 18].type.type = TileData.Type.Floor;

        tileMap.tilesData[21, 10].type.type = TileData.Type.Floor;
        tileMap.tilesData[21, 11].type.type = TileData.Type.Floor;
        tileMap.tilesData[21, 12].type.type = TileData.Type.Floor;
        tileMap.tilesData[21, 13].type.type = TileData.Type.Floor;
        tileMap.tilesData[21, 14].type.type = TileData.Type.Floor;
        tileMap.tilesData[21, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[21, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[21, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[21, 18].type.type = TileData.Type.Floor;

        tileMap.tilesData[18, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[17, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[16, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[15, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[14, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[13, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[12, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[11, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[10, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[9, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[8, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[7, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[6, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[5, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[4, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[3, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[2, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[1, 18].type.type = TileData.Type.Floor;
        tileMap.tilesData[0, 18].type.type = TileData.Type.Floor;

        tileMap.tilesData[18, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[17, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[16, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[15, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[14, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[13, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[12, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[11, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[10, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[9, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[8, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[7, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[6, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[5, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[4, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[3, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[2, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[1, 17].type.type = TileData.Type.Floor;
        tileMap.tilesData[0, 17].type.type = TileData.Type.Floor;

        tileMap.tilesData[18, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[17, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[16, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[15, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[14, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[13, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[12, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[11, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[10, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[9, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[8, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[7, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[6, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[5, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[4, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[3, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[2, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[1, 16].type.type = TileData.Type.Floor;
        tileMap.tilesData[0, 16].type.type = TileData.Type.Floor;

        tileMap.tilesData[18, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[17, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[16, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[15, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[14, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[13, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[12, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[11, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[10, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[9, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[8, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[7, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[6, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[5, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[4, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[3, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[2, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[1, 15].type.type = TileData.Type.Floor;
        tileMap.tilesData[0, 15].type.type = TileData.Type.Floor;

        tileMap.tilesData[14, 6].type.type = TileData.Type.Floor;
        tileMap.tilesData[15, 6].type.type = TileData.Type.Floor;
        tileMap.tilesData[14, 7].type.type = TileData.Type.Floor;
        tileMap.tilesData[15, 7].type.type = TileData.Type.Floor;
        tileMap.tilesData[15, 8].type.type = TileData.Type.Floor;
        tileMap.tilesData[14, 8].type.type = TileData.Type.Floor;
        tileMap.tilesData[14, 9].type.type = TileData.Type.Floor;
        tileMap.tilesData[14, 10].type.type = TileData.Type.Floor;
        tileMap.tilesData[15, 10].type.type = TileData.Type.Floor;
        tileMap.tilesData[16, 10].type.type = TileData.Type.Floor;
        tileMap.tilesData[17, 10].type.type = TileData.Type.Floor;
        tileMap.tilesData[18, 10].type.type = TileData.Type.Floor;
        tileMap.tilesData[14, 9].type.type = TileData.Type.Floor;
        tileMap.tilesData[15, 9].type.type = TileData.Type.Floor;
        tileMap.tilesData[16, 9].type.type = TileData.Type.Floor;
        tileMap.tilesData[17, 9].type.type = TileData.Type.Floor;
        tileMap.tilesData[18, 9].type.type = TileData.Type.Floor;
	}
	
}
