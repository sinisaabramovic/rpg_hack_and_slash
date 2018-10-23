using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour {
    
    [SerializeField] private List<Item> StartItems;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private ItemsSlot[] itemsSlots;

    public event Action<ItemsSlot> OnPointerEnterEvent;
    public event Action<ItemsSlot> OnPointerExitEvent;
    public event Action<ItemsSlot> OnRightClickEvent;
    public event Action<ItemsSlot> OnBeginDragEvent;
    public event Action<ItemsSlot> OnEndDragEvent;
    public event Action<ItemsSlot> OnDragEvent;
    public event Action<ItemsSlot> OnDropEvent;

    private void Start()
    {
        for (int i = 0; i < itemsSlots.Length; i++)
        {
            itemsSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            itemsSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            itemsSlots[i].OnRightClickEvent += OnRightClickEvent;
            itemsSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            itemsSlots[i].OnEndDragEvent += OnEndDragEvent;
            itemsSlots[i].OnDragEvent += OnDragEvent;
            itemsSlots[i].OnDropEvent += OnDropEvent;
        }
    }

    private void OnValidate()
    {
        if(itemsParent != null){
            itemsSlots = itemsParent.GetComponentsInChildren<ItemsSlot>();
        }

        //RefreshUI();
        SetStartingItems();
    }

    private void SetStartingItems(){
        int i = 0;
        for (; i < StartItems.Count && i < itemsSlots.Length; i++){
            itemsSlots[i].Item = StartItems[i];
        }

        for (; i < itemsSlots.Length; i++){
            itemsSlots[i].Item = null;
        }
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < itemsSlots.Length; i++){
            if(itemsSlots[i].Item == null)
            {
                itemsSlots[i].Item = item;
                return true;
            }
        }
        Debug.Log("SRANJE");
        return false;
    }

    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < itemsSlots.Length; i++)
        {
            if (itemsSlots[i].Item == item)
            {
                itemsSlots[i].Item = null;
                return true;
            }
        }

        return false;
    }

    public bool IsFull()
    {
        for (int i = 0; i < itemsSlots.Length; i++)
        {
            if (itemsSlots[i].Item == null)
            {                
                return false;
            }
        }

        return true;
    }
}
