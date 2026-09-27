using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ProjectD.Core;

namespace ProjectD.UI
{
    public class SlotView : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Image background;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI countText;

        [Header("Background color")]
        [SerializeField] private Color normalColor = Color.gray;
        [SerializeField] private Color selectedColor = Color.yellow;

        public void Render(Slot slot)
        {
            bool isEmpty = slot == null || slot.IsEmpty;

            if (isEmpty)
            {
                icon.enabled = false;
                countText.text = "";
            }
            else
            {
                icon.enabled = true;
                icon.sprite = slot.Item.icon;
                countText.text = slot.Amount.ToString();
            }
        }

        public void SetSelected(bool isSelected)
        {
            if (isSelected)
            {
                background.color = selectedColor;
            }
            else
            {
                background.color = normalColor;
            }
        }
    }
}
