using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sino.CharacterStats;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    // Use this for initialization

    public CharacterStats Strength;
    public CharacterStats Agility;
    public CharacterStats Intelligence;
    public CharacterStats Vitality;

    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] StatPanel statPanel;
    [SerializeField] ItemToolTip itemToolTip;
    [SerializeField] Image draggableItem;

    private ItemsSlot draggSlot;

    private void OnValidate()
    {
        if(itemToolTip == null){
            itemToolTip = FindObjectOfType<ItemToolTip>();
        }
    }

    public void Awake()
    {
        statPanel.SetStats(Strength, Agility, Intelligence, Vitality);
        statPanel.UpdateStatValues();

        // Setup Events

        inventory.OnRightClickEvent += Equip;
        equipmentPanel.OnRightClickEvent += Unequip;

        inventory.OnPointerEnterEvent += ShowToolTip;
        equipmentPanel.OnPointerEnterEvent += ShowToolTip;

        inventory.OnPointerExitEvent += HideToolTip;
        equipmentPanel.OnPointerExitEvent += HideToolTip;

        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;

        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;

        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;

        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;

    }

    private void Equip(ItemsSlot itemsSlot)
    {
        EquippableItem equippableItem = itemsSlot.Item as EquippableItem;

        if(equippableItem != null){
            Equip(equippableItem);
        }
    }

    private void Unequip(ItemsSlot itemsSlot)
    {
        EquippableItem equippableItem = itemsSlot.Item as EquippableItem;

        if (equippableItem != null)
        {
            Unequip(equippableItem);
        }
    }

    private void ShowToolTip(ItemsSlot itemsSlots){
       
        EquippableItem equippableItem = itemsSlots.Item as EquippableItem;

        if (equippableItem != null)
        {
            itemToolTip.ShowToolTip(equippableItem);
        }
    }

    private void HideToolTip(ItemsSlot itemsSlots){
        
        itemToolTip.HideToolTip();
    }

    private void BeginDrag(ItemsSlot itemSlot)
    {        
        if(itemSlot.Item != null){
            draggSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    private void EndDrag(ItemsSlot itemsSlots)
    {        
        draggSlot = null;
        draggableItem.enabled = false;
    }

    private void Drag(ItemsSlot itemsSlots)
    {        
        if(draggableItem.enabled){
            draggableItem.transform.position = Input.mousePosition;    
        }
    }

    private void Drop(ItemsSlot dropItemSlot)
    {
        if(dropItemSlot.CanReciveItem(draggSlot.Item) && draggSlot.CanReciveItem(dropItemSlot.Item))
        {

            EquippableItem dragItem = draggSlot.Item as EquippableItem;
            EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

            if(draggSlot is EquipmentsSlots){
                if (dragItem != null) dragItem.Unequip(this);
                if (dropItem != null) dropItem.Equip(this);
            }

            if(dropItemSlot is EquipmentsSlots){
                if (dragItem != null) dragItem.Equip(this);
                if (dropItem != null) dropItem.Unequip(this);
            }

            statPanel.UpdateStatValues();

            //Item draggedItem = dropItemSlot.Item;
            Item draggedItem = draggSlot.Item;
            Item dropSlotItem = dropItemSlot.Item;
            dropItemSlot.Item = draggedItem;
            draggSlot.Item = dropSlotItem;
            //dropItemSlot.Item = draggedItem;
        }
            
    }

    public void Equip(EquippableItem item){
        if(inventory.RemoveItem(item)){
            EquippableItem previousItem;
            if(equipmentPanel.AddItem(item, out previousItem))
            {
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
        }else{
            inventory.AddItem(item);
        }
    }

    public void Unequip(EquippableItem item){
        if(!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
    }
}
