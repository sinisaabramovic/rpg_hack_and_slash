using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PathFindTest : MonoBehaviour {

    // Use this for initialization

    public GameObject mapGroup;

	void Start () {
        int[,] map = new int[5, 5]{
            {0,1,0,0,0},
            {0,1,0,0,0},
            {0,1,0,0,0},
            {0,1,0,0,0},
            {0,0,0,0,0},
        };

        var graph = new Graph(map);

        var search = new Search(graph);
        search.Start(graph.nodes[0], graph.nodes[2]);

        while(!search.finished){
            search.Step();
        }

        print("Search done! " + search.path.Count + " iterations " + search.iterations);

        ResetmapGroup(graph);

        foreach(var node in search.path){
            GetImage(node.label).color = Color.red;
        }
	}

    Image GetImage(string label){
        var id = Int32.Parse(label);
        var go = mapGroup.transform.GetChild(id).gameObject;

        return go.GetComponent<Image>();
    }

    private void ResetmapGroup(Graph graph)
	{
        foreach(var node in graph.nodes){
            GetImage(node.label).color = node.adjecent.Count == 0 ? Color.white : Color.grey;
        }
	}

	// Update is called once per frame
	void Update () {
		
	}
}
