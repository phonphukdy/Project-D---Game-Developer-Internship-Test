using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObject/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("Base data")]
    public string itemName;
    public Sprite icon;
    public EItemCategory category;

    [Header("Stack")]
    public int maxStack = 1;

    [Header("Behaviour")]
    public bool isEquipable;
    public bool isUsable;
    public bool isPlaceable;

    [Header("Only when isPlaceable = true")]
    public GameObject placedPrefab;

    public bool IsStackable
    {
        get
        {
            bool moreThanOne = maxStack > 1;
            return moreThanOne;
        }
    }
}