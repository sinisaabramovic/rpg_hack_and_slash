using UnityEngine;

public class EquipmentsSlots : ItemsSlot {

    public EquipmentType equipmentType;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = equipmentType.ToString() + " Slot";
    }

    public override bool CanReciveItem(Item item)
    {

        if (item == null)
        {
            return true;
        }
        EquippableItem equippable = item as EquippableItem;

        return equippable != null && equippable.equipmentType == equipmentType;
    }
}
