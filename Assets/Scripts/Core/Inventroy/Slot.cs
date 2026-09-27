using UnityEngine;

namespace ProjectD.Core
{
    public class Slot
    {
        public ItemData Item;
        public int Amount;

        public bool IsEmpty
        {
            get
            {
                bool noItem = Item == null;
                return noItem;
            }
        }

        public void Clear()
        {
            Item = null;
            Amount = 0;
        }
    }
}
