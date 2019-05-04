using UnityEngine;

public class CanBeCollected : MonoBehaviour, IInteractible, ICollectible
{
    public InventoryHistory InventoryHistory;

    public Transform Transform
    {
        get
        {
            return transform;
        }
    }

    public string ItemName { get; private set; }

    public ITEM_TYPE _itemType;
    public ITEM_TYPE ItemType
    {
        get
        {
            return _itemType;
        }
    }

    public INTERACTIBLE_TYPE InteractibleType
    {
        get
        {
            return INTERACTIBLE_TYPE.COLLECTIBLE;
        }
    }

    private void Start()
    {
        // TODO: Any way we can make this nicer with a string helper?
        // Could use the scene management with it then also.
        // NOTE: This item name should never change. It's needed for checking
        // against if picked up or not (perhaps look at ScriptableObjects for this sort of permenance).
        ItemName = "GameObject=" + gameObject.name + ".ItemType=" + ItemType.ToString();

        // TODO: If having trouble destroying things, just never make them in the first
        // place. Think about using item spawners that are invisible, and if item doesn't
        // yet exist in pickup history, spawn it.
        if (InventoryHistory.HasUsedItem(ItemName))
            Destroy(gameObject);
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        // Register it to the inventory history so it doesn't appear on reload.
        InventoryHistory.LogUsedItem(ItemName);

        // Assuming collection was successful.
        Destroy(gameObject);
    }
}
