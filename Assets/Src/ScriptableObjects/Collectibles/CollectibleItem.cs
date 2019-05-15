using UnityEngine;

[CreateAssetMenu(fileName = "New Collectible Item", menuName = "Collectible Item", order = 51)]
public class CollectibleItem : ScriptableObject
{
    public string Id;

    public ITEM_TYPE _CollectibleItemType;
    public ITEM_TYPE CollectibleItemType { get => _CollectibleItemType; }

    public bool IsKeyItem { get => CollectibleItemType == ITEM_TYPE.KEY_ITEM; }

    public string _CollectibleItemName;
    public string CollectibleItemName { get => _CollectibleItemName; }

    [TextArea]
    public string _CollectibleItemMeta;
    public string CollectibleItemMeta { get => _CollectibleItemMeta; }

    public int _CollectibleItemHealthValue;
    public int CollectibleItemHealthValue { get => _CollectibleItemHealthValue; }

    public int _CollectibleItemMpValue;
    public int CollectibleItemMpValue { get => _CollectibleItemMpValue; }
}
