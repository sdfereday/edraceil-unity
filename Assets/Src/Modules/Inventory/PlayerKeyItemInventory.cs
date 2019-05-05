using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerKeyItemInventory : MonoBehaviour
{
    [System.Serializable]
    public class KeyItemMeta
    {
        public string Name;
        public string Id;
        public ITEM_TYPE Type;
    }

    public List<KeyItemMeta> _keyItems;
    public List<KeyItemMeta> Keyitems { get => _keyItems; }

    private void Start()
    {
        // TODO: Don't forget to add existing after load.
        _keyItems = new List<KeyItemMeta>();
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
        // Save gained key items to disk.
        Debug.Log("Player key item inventory got save to disk signal!");
        _keyItems.ForEach(x => Debug.Log(x.Name));
    }

    public void AddItem(CollectibleItem _keyItemObject)
    {
            var existing = _keyItems.Find(x => x.Id == _keyItemObject.Id || x.Name == _keyItemObject.CollectibleItemName);

            if (existing != null)
            {
                throw new UnityException("You shouldn't have more than one of these items.");
            }
            else
            {
                // Meta is just used to populate the temporary instance of the UI, etc.
                _keyItems.Add(new KeyItemMeta()
                {
                    Name = _keyItemObject.CollectibleItemName,
                    Id = _keyItemObject.Id,
                    Type = ITEM_TYPE.KEY_ITEM
                });
            }
        
    }

    public bool HasItem(string _id) =>_keyItems.Any(x => x.Id == _id);
}
