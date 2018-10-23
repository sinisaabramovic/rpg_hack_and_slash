using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sino.CharacterStats;

public class Shield : MonoBehaviour {

    // Use this for initialization

    public Vector3 handPosition;
    public Vector3 handRotation;

    public string ShieldName;
    public Texture2D ShiledImage;

    public float BlockPercentAmount;

    public StatModType modType = StatModType.Flat;

    private StatsModifier blockAmount;
    private Sino.CharacterStats.PlayerCharacter character;

    public void Equip(Sino.CharacterStats.PlayerCharacter c)
    {

        character = c;

        blockAmount = new StatsModifier(BlockPercentAmount, modType, this);
        c.BlockAmount.AddModifier(blockAmount);

    }

    public void UnEquip(Sino.CharacterStats.PlayerCharacter c)
    {
        c.BlockAmount.RemoveAllModifiersFromSource(this);
    }

    public void OnDestroy()
    {
        if(character != null){
            UnEquip(character);
        }
    }

}
