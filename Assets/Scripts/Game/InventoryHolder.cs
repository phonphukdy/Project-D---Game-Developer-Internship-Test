using ProjectD.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectD.Game
{
    public class InventoryHolder : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int slotCount = 6;

        private Inventory inventory;

        public Inventory Inventory
        {
            get
            {
                return inventory;
            }
        }

        private void Awake()
        {
            if (slotCount <= 0)
            {
                // Inventory atleast should have 1 slot
                Debug.LogWarning("InventoryHolder: slotCount must greater than 0, if below or equal 0 set it to 1", this);
                slotCount = 1;
            }

            inventory = new Inventory(slotCount);
        }

        private void Update()
        {
            HandleHotkeySelection();
        }

        private void HandleHotkeySelection()
        {
            int maxHotkeys = 9;
            int slotsAvailable = inventory.SlotCount;
            int hotkeyCount = maxHotkeys;

            if (slotsAvailable < maxHotkeys)
            {
                hotkeyCount = slotsAvailable;
            }

            for (int i = 0; i < hotkeyCount; i++)
            {
                Key key = Key.Digit1 + i;
                bool keyExists = Keyboard.current[key] != null;

                if (keyExists)
                {
                    bool wasPressed = Keyboard.current[key].wasPressedThisFrame;

                    if (wasPressed)
                    {
                        inventory.SelectSlot(i);
                        break;
                    }
                }
            }
        }
    }
}
