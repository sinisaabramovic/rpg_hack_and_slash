using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour {

    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] EquipmentsSlots[] equipmentsSlots;

    public event Action<ItemsSlot> OnPointerEnterEvent;
    public event Action<ItemsSlot> OnPointerExitEvent;
    public event Action<ItemsSlot> OnRightClickEvent;
    public event Action<ItemsSlot> OnBeginDragEvent;
    public event Action<ItemsSlot> OnEndDragEvent;
    public event Action<ItemsSlot> OnDragEvent;
    public event Action<ItemsSlot> OnDropEvent;

    private void Start()
    {
        for (int i = 0; i < equipmentsSlots.Length; i++)
        {
            equipmentsSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            equipmentsSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            equipmentsSlots[i].OnRightClickEvent += OnRightClickEvent;
            equipmentsSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            equipmentsSlots[i].OnEndDragEvent += OnEndDragEvent;
            equipmentsSlots[i].OnDragEvent += OnDragEvent;
            equipmentsSlots[i].OnDropEvent += OnDropEvent;
        }
    }


    private void OnValidate()
    {
      
        equipmentsSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentsSlots>();
    }

    public bool AddItem(EquippableItem item, out EquippableItem previousItem){

        for (int i = 0; i < equipmentsSlots.Length; i++){
            if(equipmentsSlots[i].equipmentType == item.equipmentType){
                previousItem = (EquippableItem)equipmentsSlots[i].Item;
                equipmentsSlots[i].Item = item;
                return true;
            }
        }
        previousItem = null;
        return false;
    }

    public bool RemoveItem(EquippableItem item)
    {

        for (int i = 0; i < equipmentsSlots.Length; i++)
        {
            if (equipmentsSlots[i].Item == item)
            {
                equipmentsSlots[i].Item = null;
                return true;
            }
        }

        return false;

    }
}
