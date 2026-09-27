using ProjectD.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectD.Game
{
    public class ItemActions : MonoBehaviour
    {
        private InventoryHolder inventoryHolder;
        private ItemData equippedItem;

        public ItemData EquippedItem
        {
            get
            {
                return equippedItem;
            }
        }

        private void Awake()
        {
            inventoryHolder = GetComponent<InventoryHolder>();
        }

        private void Update()
        {
            bool interactPressed = Keyboard.current[Key.E].wasPressedThisFrame;

            if (interactPressed)
            {
                TryInteractWithSelectedItem();
            }
        }

        private void TryInteractWithSelectedItem()
        {
            Inventory inventory = inventoryHolder.Inventory;
            Slot selectedSlot = inventory.GetSlot(inventory.SelectedIndex);

            if (selectedSlot == null || selectedSlot.IsEmpty)
            {
                Debug.Log("ช่องที่เลือกว่างเปล่า ไม่มีอะไรให้ใช้");
                return;
            }

            ItemData item = selectedSlot.Item;

            if (item.isUsable)
            {
                UseItem(inventory, inventory.SelectedIndex, item);
            }
            else if (item.isEquipable)
            {
                ToggleEquip(item);
            }
            else
            {
                Debug.Log(item.itemName + " ใช้จากแถบไม่ได้ (ไม่ใช่ usable หรือ equipable)");
            }
        }

        private void UseItem(Inventory inventory, int index, ItemData item)
        {
            bool removed = inventory.RemoveAt(index, 1);

            if (removed)
            {
                Debug.Log("ใช้ " + item.itemName + " แล้ว");
            }
        }

        private void ToggleEquip(ItemData item)
        {
            bool alreadyEquipped = equippedItem == item;

            if (alreadyEquipped)
            {
                equippedItem = null;
                Debug.Log("ถอด " + item.itemName + " แล้ว");
            }
            else
            {
                equippedItem = item;
                Debug.Log("สวม/ถือ " + item.itemName + " แล้ว");
            }
        }
    }
}
