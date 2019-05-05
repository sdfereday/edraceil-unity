using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Collectible Item", menuName = "Collectible Item", order = 51)]
public class CollectibleItem : ScriptableObject
{
    private Guid guid = new Guid();
    public string Id { get => guid.ToString(); }

    public bool _IsKeyItem;
    public bool IsKeyItem { get => _IsKeyItem; }

    public ITEM_TYPE _CollectibleItemType;
    public ITEM_TYPE CollectibleItemType { get => _CollectibleItemType; }

    public string _CollectibleItemName;
    public string CollectibleItemName { get => _CollectibleItemName; }

    public int _CollectibleItemHealthValue;
    public int CollectibleItemHealthValue { get => _CollectibleItemHealthValue; }

    public int _CollectibleItemMpValue;
    public int CollectibleItemMpValue { get => _CollectibleItemMpValue; }
}
