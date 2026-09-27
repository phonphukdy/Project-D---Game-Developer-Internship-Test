using System;
using UnityEngine;

namespace ProjectD.Core
{
    public class Inventory
    {
        private Slot[] slots;
        private int selectedIndex;

        public event Action<int> OnSlotChanged;
        public event Action<int> OnSelectionChanged;

        public int SlotCount
        {
            get
            {
                return slots.Length;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
        }

        public Inventory(int size)
        {
            slots = new Slot[size];

            for (int i = 0; i < slots.Length; i++)
            {
                slots[i] = new Slot();
            }

            selectedIndex = 0;
        }

        public Slot GetSlot(int index)
        {
            bool indexIsValid = IsIndexValid(index);

            if (indexIsValid == false)
            {
                UnityEngine.Debug.LogWarning("Inventory: index not correct " + index);
                return null;
            }

            return slots[index];
        }

        public int Add(ItemData item, int amount)
        {
            if (item == null || amount <= 0)
            {
                return amount;
            }

            int remaining = amount;

            for (int i = 0; i < slots.Length; i++)
            {
                if (remaining <= 0)
                {
                    break;
                }

                Slot slot = slots[i];
                bool sameItem = slot.Item == item;

                if (sameItem)
                {
                    int spaceLeft = item.maxStack - slot.Amount;

                    if (spaceLeft > 0)
                    {
                        int amountToAdd = remaining;
                        if (amountToAdd > spaceLeft)
                        {
                            amountToAdd = spaceLeft;
                        }

                        slot.Amount = slot.Amount + amountToAdd;
                        remaining = remaining - amountToAdd;

                        NotifySlotChanged(i);
                    }
                }
            }

            for (int i = 0; i < slots.Length; i++)
            {
                if (remaining <= 0)
                {
                    break;
                }

                Slot slot = slots[i];

                if (slot.IsEmpty)
                {
                    int amountToAdd = remaining;
                    if (amountToAdd > item.maxStack)
                    {
                        amountToAdd = item.maxStack;
                    }

                    slot.Item = item;
                    slot.Amount = amountToAdd;
                    remaining = remaining - amountToAdd;

                    NotifySlotChanged(i);
                }
            }

            return remaining;
        }

        public bool RemoveAt(int index, int amount)
        {
            bool indexIsValid = IsIndexValid(index);

            if (indexIsValid == false)
            {
                return false;
            }

            Slot slot = slots[index];

            if (slot.IsEmpty || slot.Amount < amount)
            {
                return false;
            }

            slot.Amount = slot.Amount - amount;

            if (slot.Amount <= 0)
            {
                slot.Clear();
            }

            NotifySlotChanged(index);
            return true;
        }

        public bool RemoveItem(ItemData item, int amount)
        {
            int totalAvailable = 0;

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].Item == item)
                {
                    totalAvailable = totalAvailable + slots[i].Amount;
                }
            }

            bool hasEnough = totalAvailable >= amount;

            if (hasEnough == false)
            {
                return false;
            }

            int remainingToRemove = amount;

            for (int i = 0; i < slots.Length; i++)
            {
                if (remainingToRemove <= 0)
                {
                    break;
                }

                Slot slot = slots[i];

                if (slot.Item == item)
                {
                    int amountToRemove = remainingToRemove;
                    if (amountToRemove > slot.Amount)
                    {
                        amountToRemove = slot.Amount;
                    }

                    slot.Amount = slot.Amount - amountToRemove;
                    remainingToRemove = remainingToRemove - amountToRemove;

                    if (slot.Amount <= 0)
                    {
                        slot.Clear();
                    }

                    NotifySlotChanged(i);
                }
            }

            return true;
        }

        public void Swap(int indexA, int indexB)
        {
            bool bothValid = IsIndexValid(indexA) && IsIndexValid(indexB);

            if (bothValid == false)
            {
                return;
            }

            Slot temp = slots[indexA];
            slots[indexA] = slots[indexB];
            slots[indexB] = temp;

            NotifySlotChanged(indexA);
            NotifySlotChanged(indexB);
        }

        public void SelectSlot(int index)
        {
            bool indexIsValid = IsIndexValid(index);

            if (indexIsValid == false)
            {
                return;
            }

            selectedIndex = index;

            if (OnSelectionChanged != null)
            {
                OnSelectionChanged(index);
            }
        }

        private bool IsIndexValid(int index)
        {
            bool tooSmall = index < 0;
            bool tooBig = index >= slots.Length;

            if (tooSmall || tooBig)
            {
                return false;
            }

            return true;
        }

        private void NotifySlotChanged(int index)
        {
            if (OnSlotChanged != null)
            {
                OnSlotChanged(index);
            }
        }
    }
}
