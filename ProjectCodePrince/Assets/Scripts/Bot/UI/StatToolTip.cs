using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sino.CharacterStats;

public class StatToolTip : MonoBehaviour {


    [SerializeField] Text StatNameText;
    [SerializeField] Text StatModifiersLabelText;
    [SerializeField] Text StatModifiersText;

    private StringBuilder sb = new StringBuilder();

    public void ShowToolTip(CharacterStats stat, string statName)
    {
        StatNameText.text = GetStatTopText(stat, statName);
        StatModifiersLabelText.text = GetStatModifiersText(stat);
        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }

    private string GetStatTopText(CharacterStats stat, string statName)
    {
        sb.Length = 0;
        sb.Append(statName);
        sb.Append(" ");
        sb.Append(stat.Value);

        if(stat.Value != stat.BaseValue){
            sb.Append(" (");
            sb.Append(stat.BaseValue);

            if (stat.Value > stat.BaseValue)
            {
                sb.Append("+");
            }

            sb.Append(System.Math.Round(stat.Value - stat.BaseValue, 4));
            sb.Append(") ");
        }

        return sb.ToString();

    }

    private string GetStatModifiersText(CharacterStats stat){
        sb.Length = 0;

        foreach(StatsModifier mod in stat.StatsModifiers)
        {
            if(sb.Length > 0){
                sb.AppendLine();
            }

            if(mod.Value > 0){
                sb.Append("+");
            }

            if(mod.statModType == StatModType.Flat){
                sb.Append(mod.Value);
            }else{
                sb.Append(mod.Value * 100);
                sb.Append("%");
            }           

            EquippableItem item = mod.Source as EquippableItem;

            if(item != null){
                sb.Append(" ");
                sb.Append(item.ItemName);
            }else{
                Debug.LogError("Modifier is not an Equippableitem!");
            }
        }

        return sb.ToString();
    }

}
