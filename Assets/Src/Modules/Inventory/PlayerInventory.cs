using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [System.Serializable]
    public class ItemMeta
    {
        public string Name;
        public int Qty;
        public ITEM_TYPE Type;
        public int HealthValue;
        public int MpValue;
    }

    public List<ItemMeta> _items;
    public List<ItemMeta> Items
    {
        get
        {
            return _items;
        }
    }

    private void Start()
    {
        // TODO: Don't forget to add existing after load.
        _items = new List<ItemMeta>();
    }

    private void OnEnable()
    {
        SaveGame.OnSaveSignal += SaveToDisk;
    }

    private void OnDisable()
    {
        SaveGame.OnSaveSignal -= SaveToDisk;
    }

    public void SaveToDisk()
    {
        // Save gained inventory specific items to disk.
        Debug.Log("Player inventory got save to disk signal!");
        _items.ForEach(x => Debug.Log(x.Name));
    }

    public void AddItem(CollectibleItem _collectibleItemObject, int _qty = 1)
    {
        // Check for existing and increase qty if so.
        var existing = Items.Find(x => x.Type == _collectibleItemObject.CollectibleItemType);

        if (existing != null)
        {
            existing.Qty += _qty;
        } else {
            // Meta is just used to populate the temporary instance of the UI, etc.
            _items.Add(new ItemMeta()
            {
                Name = _collectibleItemObject.CollectibleItemName,
                Qty = _qty,
                Type = _collectibleItemObject.CollectibleItemType,
                HealthValue = _collectibleItemObject.CollectibleItemHealthValue,
                MpValue = _collectibleItemObject.CollectibleItemMpValue
            });
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
