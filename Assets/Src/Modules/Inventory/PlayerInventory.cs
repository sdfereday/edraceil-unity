using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public string Name;
        public int Qty;
        public ITEM_TYPE Type;
        public int HealthValue;
        public int MpValue;
    }

    public List<Item> _items;
    public List<Item> Items
    {
        get
        {
            return _items;
        }
    }

    private void Start()
    {
        _items = new List<Item>();
    }

    public void AddItem(ITEM_TYPE _type, string _itemName, int _qty = 1)
    {
        // Find data info for item picked up.
        var registeredItem = ItemDataStub.ItemRegistry.FirstOrDefault(x => x.Type == _type);

        if (registeredItem != null)
        {
            // Check for existing and increase qty if so.
            var existing = Items.Find(x => x.Type == _type);

            if (existing != null)
            {
                existing.Qty += _qty;
            } else {
                // Otherwise, add it in.
                _items.Add(new Item()
                {
                    Name = _itemName,
                    Qty = _qty,
                    Type = registeredItem.Type,
                    HealthValue = registeredItem.HealthValue,
                    MpValue = registeredItem.MpValue
                });
            }
        }
    }

    public void RemoveItem(ITEM_TYPE _type, int _qty = 1)
    {
        var existing = Items.Find(x => x.Type == _type);

        if (existing != null)
        {
            if (existing.Qty - _qty > 0)
            {
                existing.Qty -= _qty;
            } else
            {
                _items.RemoveAll(x => x.Type == _type);
            }
        }
    }
}
