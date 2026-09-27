using UnityEngine;

namespace ProjectD.Game
{
    public class ItemPickup : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SpriteRenderer itemImage;
        [SerializeField] private BoxCollider2D groundCollider;   // Is Trigger = false
        [SerializeField] private BoxCollider2D triggerCollider;  // Is Trigger = true

        [Header("Trigger Padding")]
        [Tooltip("Trigger จะใหญ่กว่า sprite กี่เท่า เพื่อให้ผู้เล่นเก็บได้ก่อนโดน solid collider ดันหยุด")]
        [SerializeField] private float triggerPaddingMultiplier = 2f;

        [Header("Item to pickup")]
        [SerializeField] private ItemData item;
        [SerializeField] private int startingAmount = 1;

        private void Start()
        {
            itemImage.sprite = item.icon;
            FitCollidersToSprite();
        }

        private void FitCollidersToSprite()
        {
            Bounds spriteBounds = itemImage.sprite.bounds;

            // Solid collider: พอดีกับรูปเป๊ะ (กันไม่ให้ตกทะลุพื้น)
            groundCollider.size = spriteBounds.size;
            groundCollider.offset = spriteBounds.center;

            // Trigger collider: ใหญ่กว่า solid เสมอ ไม่ว่ารูปจะขนาดไหน
            Vector2 triggerSize = spriteBounds.size * triggerPaddingMultiplier;
            triggerCollider.size = triggerSize;
            triggerCollider.offset = spriteBounds.center;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            InventoryHolder holder = other.GetComponentInParent<InventoryHolder>();

            if (holder == null)
            {
                return;
            }

            int leftover = holder.Inventory.Add(item, startingAmount);

            if (leftover <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                int amountPickedUp = startingAmount - leftover;
                startingAmount = leftover;

                Debug.Log("Item can collect: " + amountPickedUp + ", the left over: " + startingAmount);
            }
        }
    }
}