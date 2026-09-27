using NUnit.Framework;
using ProjectD.Core;
using ProjectD.Game;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class InventoryTests
{
    [Test]
    public void Add_FillsCompletely_WhenAmountFitsInOneSlot()
    {
        Inventory inventory = new Inventory(4);
        ItemData lumber = ScriptableObject.CreateInstance<ItemData>();
        lumber.maxStack = 10;

        int leftover = inventory.Add(lumber, 5);

        Assert.AreEqual(0, leftover);
        Assert.AreEqual(5, inventory.GetSlot(0).Amount);
    }

    [Test]
    public void Add_ReturnsLeftover_WhenInventoryIsFull()
    {
        Inventory inventory = new Inventory(4);
        ItemData lumber = ScriptableObject.CreateInstance<ItemData>();
        lumber.maxStack = 10;

        for (int i = 0; i < 4; i++)
        {
            inventory.Add(lumber, 10);
        }

        int leftover = inventory.Add(lumber, 5);

        Assert.AreEqual(5, leftover);
    }

    [Test]
    public void RemoveAt_ReducesAmount_WhenEnoughAvailable()
    {
        Inventory inventory = new Inventory(4);
        ItemData lumber = ScriptableObject.CreateInstance<ItemData>();
        lumber.maxStack = 10;
        inventory.Add(lumber, 5);

        bool success = inventory.RemoveAt(0, 3);

        Assert.IsTrue(success);
        Assert.AreEqual(2, inventory.GetSlot(0).Amount);
    }

    [Test]
    public void RemoveAt_Fails_WhenAmountExceedsAvailable()
    {
        Inventory inventory = new Inventory(4);
        ItemData lumber = ScriptableObject.CreateInstance<ItemData>();
        lumber.maxStack = 10;
        inventory.Add(lumber, 3);

        bool success = inventory.RemoveAt(0, 10);

        Assert.IsFalse(success);
        Assert.AreEqual(3, inventory.GetSlot(0).Amount);
    }

    [Test]
    public void RemoveItem_RemovesAcrossMultipleSlots_WhenSplitOverSlots()
    {
        Inventory inventory = new Inventory(4);
        ItemData lumber = ScriptableObject.CreateInstance<ItemData>();
        lumber.maxStack = 10;

        inventory.Add(lumber, 10);
        inventory.Add(lumber, 10);
        inventory.Add(lumber, 5);

        bool success = inventory.RemoveItem(lumber, 15);

        Assert.IsTrue(success);

        int totalRemaining = 0;
        for (int i = 0; i < inventory.SlotCount; i++)
        {
            totalRemaining = totalRemaining + inventory.GetSlot(i).Amount;
        }

        Assert.AreEqual(10, totalRemaining);
    }

    [Test]
    public void Swap_ExchangesItemsBetweenTwoSlots()
    {
        Inventory inventory = new Inventory(4);

        ItemData lumber = ScriptableObject.CreateInstance<ItemData>();
        lumber.itemName = "Lumber";
        lumber.maxStack = 10;
        inventory.Add(lumber, 5);

        ItemData stone = ScriptableObject.CreateInstance<ItemData>();
        stone.itemName = "Stone";
        stone.maxStack = 10;
        inventory.Add(stone, 3);

        inventory.Swap(0, 1);

        Assert.AreEqual(stone, inventory.GetSlot(0).Item);
        Assert.AreEqual(3, inventory.GetSlot(0).Amount);
        Assert.AreEqual(lumber, inventory.GetSlot(1).Item);
        Assert.AreEqual(5, inventory.GetSlot(1).Amount);
    }

    [Test]
    public void SelectSlot_DoesNotChangeSelection_WhenIndexOutOfBounds()
    {
        Inventory inventory = new Inventory(4);
        inventory.SelectSlot(2);

        inventory.SelectSlot(99);

        Assert.AreEqual(2, inventory.SelectedIndex);
    }

    [Test]
    public void Awake_CreatesInventoryWithCorrectDefaultSize()
    {
        GameObject go = new GameObject();
        InventoryHolder holder = go.AddComponent<InventoryHolder>();

        Assert.AreEqual(6, holder.Inventory.SlotCount); // 6 คือค่า default ใน field slotCount

        Object.DestroyImmediate(go);
    }
}
