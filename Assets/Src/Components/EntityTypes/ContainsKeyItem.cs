using UnityEngine;

public class ContainsKeyItem : MonoBehaviour, ICollectible
{
    public CollectibleItem _KeyItemObject;
    public PlayerKeyItemInventory KeyItemInventory;
    public EntityHistory KeyItemHistory;
    public bool DestroyOnCollection = false;

    public Transform Transform { get => transform; }
    public CollectibleItem CollectibleItemObject { get => _KeyItemObject; }
    public INTERACTIBLE_TYPE InteractibleType { get => INTERACTIBLE_TYPE.COLLECTIBLE; }

    private void Start()
    {
        if (KeyItemHistory.WasUsed(CollectibleItemObject.Id))
        {
            // ...
            // Or, set to open.
            if (DestroyOnCollection)
                Destroy(gameObject);
        }
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        if (!CollectibleItemObject.IsKeyItem)
            throw new UnityException("Tried to add a non-key item to the key item inventory, this isn't allowed.");

        KeyItemInventory.AddItem(CollectibleItemObject);
        KeyItemHistory.LogUsed(CollectibleItemObject.Id);

        // Assuming collection was successful.
        // Or, set to open.
        if (DestroyOnCollection)
            Destroy(gameObject);
    }
}
