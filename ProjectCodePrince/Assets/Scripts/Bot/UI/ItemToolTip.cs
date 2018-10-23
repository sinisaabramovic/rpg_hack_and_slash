using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemToolTip : MonoBehaviour {

    [SerializeField] Text ItemNameText;
    [SerializeField] Text ItemSlotText;
    [SerializeField] Text ItemStatsText;

    private StringBuilder stringBuilder = new StringBuilder();

    public void ShowToolTip(EquippableItem item){

        ItemNameText.text = item.name;
        ItemSlotText.text = item.equipmentType.ToString();

        stringBuilder.Length = 0;
        AddStat(item.StrengthBonus, "Strength");
        AddStat(item.VitalityBonus, "Vitality");
        AddStat(item.IntelligenceBonus, "Intelligence");
        AddStat(item.VitalityBonus, "Agility");

        AddStat(item.StrengthPercentBonus, "Strength", true);
        AddStat(item.VitalityPercentBonus, "Vitality", true);
        AddStat(item.IntelligencePercentBonus, "Intelligence", true);
        AddStat(item.VitalityPercentBonus, "Agility", true);

        ItemStatsText.text = stringBuilder.ToString();
        gameObject.SetActive(true);
    }

    public void HideToolTip(){
        gameObject.SetActive(false);
    }

    private void AddStat(float value, string statName, bool isPercent = false){

        if(value != 0)
        {
            if(stringBuilder.Length > 0){
                stringBuilder.AppendLine();
            }
            if(value > 0){
                stringBuilder.Append("+");
            }

            if(isPercent){
                stringBuilder.Append(value * 100);
                stringBuilder.Append("% "); 
            }else{
                stringBuilder.Append(value);
                stringBuilder.Append(" "); 
            }


            stringBuilder.Append(statName); 
        }
    
    }
}
