﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sino.CharacterStats;

public class StatPanel : MonoBehaviour {

    [SerializeField] StatDisplay[] statDisplays;
    [SerializeField] string[] statNames;

    private CharacterStats[] stats;

    private void OnValidate()
    {
        statDisplays = GetComponentsInChildren<StatDisplay>();
        UpdateNames();
    }

    public void SetStats(params CharacterStats[] charStats){
        stats = charStats;

        if(stats.Length > statDisplays.Length){
            Debug.LogError("Not Enough Stat Displays!");
            return;
        }

        for (int i = 0; i < statDisplays.Length; i++){
            statDisplays[i].gameObject.SetActive(i < stats.Length);

            if(i < stats.Length){
                statDisplays[i].Stats = stats[i];    
            }
        }
    }

    public void UpdateStatValues(){
        for (int i = 0; i < stats.Length; i++)
        {
            statDisplays[i].UpdateStatValue();
        } 
    }

    public void UpdateNames()
    {
        for (int i = 0; i < statNames.Length; i++)
        {
            statDisplays[i].Name = statNames[i].ToString();
        }
    }
}
