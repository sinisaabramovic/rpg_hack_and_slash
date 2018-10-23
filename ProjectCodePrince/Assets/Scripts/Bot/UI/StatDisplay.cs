using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sino.CharacterStats;

public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField] Text nameText;
    [SerializeField] Text valueText;
    [SerializeField] StatToolTip toolTip;

    private CharacterStats _stats;
    public CharacterStats Stats
    {
        get { return _stats; }
        set
        {
            _stats = value;
            UpdateStatValue();
        }
    }     

    private string _name;
    public string Name{
        get { return _name; }
        set
        {
            _name = value;
            nameText.text = _name;
        }
    }

    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        valueText = texts[1];

        if(toolTip == null){
            toolTip = FindObjectOfType<StatToolTip>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTip.ShowToolTip(Stats, Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.HideToolTip();
    }

    public void UpdateStatValue()
    {
        valueText.text = _stats.Value.ToString();
    }

}
