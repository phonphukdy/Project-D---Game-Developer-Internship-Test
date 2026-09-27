using ProjectD.Core;
using ProjectD.Game;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectD.UI
{
    public class InventoryBarView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InventoryHolder inventoryHolder;
        [SerializeField] private GameObject slotViewPrefab;

        private List<SlotView> slotViews = new List<SlotView>();
        private Inventory inventory;

        private void Start()
        {
            inventory = inventoryHolder.Inventory;

            inventory.OnSlotChanged += HandleSlotChanged;
            inventory.OnSelectionChanged += HandleSelectionChanged;

            SpawnSlotViews();
            RefreshAllSlots();
            RefreshSelection(inventory.SelectedIndex);
        }

        private void OnDisable()
        {
            if (inventory != null)
            {
                inventory.OnSlotChanged -= HandleSlotChanged;
                inventory.OnSelectionChanged -= HandleSelectionChanged;
            }
        }

        private void SpawnSlotViews()
        {
            int slotCount = inventory.SlotCount;

            for (int i = 0; i < slotCount; i++)
            {
                GameObject spawned = Instantiate(slotViewPrefab, transform);
                SlotView slotView = spawned.GetComponent<SlotView>();
                slotViews.Add(slotView);
            }
        }

        private void RefreshAllSlots()
        {
            for (int i = 0; i < slotViews.Count; i++)
            {
                Slot slot = inventory.GetSlot(i);
                slotViews[i].Render(slot);
            }
        }

        private void RefreshSelection(int selectedIndex)
        {
            for (int i = 0; i < slotViews.Count; i++)
            {
                bool isSelected = i == selectedIndex;
                slotViews[i].SetSelected(isSelected);
            }
        }

        private void HandleSlotChanged(int index)
        {
            Slot slot = inventory.GetSlot(index);
            slotViews[index].Render(slot);
        }

        private void HandleSelectionChanged(int index)
        {
            RefreshSelection(index);
        }
    }
}