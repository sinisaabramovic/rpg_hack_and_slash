﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemsSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler,IEndDragHandler, IDragHandler, IDropHandler{

    [SerializeField] public Image image;

    public event Action<ItemsSlot> OnPointerEnterEvent;
    public event Action<ItemsSlot> OnPointerExitEvent;
    public event Action<ItemsSlot> OnRightClickEvent;
    public event Action<ItemsSlot> OnBeginDragEvent;
    public event Action<ItemsSlot> OnEndDragEvent;
    public event Action<ItemsSlot> OnDragEvent;
    public event Action<ItemsSlot> OnDropEvent;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1, 1, 1, 0);

    private Item _item;
    public Item Item{
        get { return _item; }
        set {
            _item = value;
            if(_item == null){
                image.color = disabledColor;
            }else{                
                image.sprite = _item.Icon;
                image.color = normalColor;
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

    }

    public virtual bool CanReciveItem(Item item)
    {
        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {       
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if(OnRightClickEvent != null)
            {
                
                OnRightClickEvent(this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(OnPointerEnterEvent != null)
        {            
            OnPointerEnterEvent(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitEvent != null)
        {
            OnPointerExitEvent(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragEvent != null)
        {           
            OnBeginDragEvent(this);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragEvent != null)
        {
            OnEndDragEvent(this);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {        
        if (OnDragEvent != null)
        {
            OnDragEvent(this);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
        {
            OnDropEvent(this);
        }
    }
}
