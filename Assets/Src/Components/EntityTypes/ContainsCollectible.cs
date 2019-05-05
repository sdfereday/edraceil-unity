using UnityEngine;

public class ContainsCollectible : MonoBehaviour, ICollectible
{
    public CollectibleItem _CollectibleItemObject;
    public PlayerInventory CollectibleItemInventory;
    public EntityHistory ItemHistory;

    // Set by hand, or in some map data. It MUST be static so we can keep track of
    // whether it's been added to the inventory or not. We don't do this with key items
    // since you're only allowed one of each type.
    public string SpawnWorldId; // TODO: Set from map data.
    
    public Transform Transform { get => transform; }
    public CollectibleItem CollectibleItemObject { get => _CollectibleItemObject; }
    public INTERACTIBLE_TYPE InteractibleType { get => INTERACTIBLE_TYPE.COLLECTIBLE; }

    private void Start()
    {        
        if (!ItemHistory.WasUsed(SpawnWorldId))
        {
            // Spawn the prefab, etc.
        }
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        // TODO: This needs deciding on. What do spawners actually do?
        CollectibleItemInventory.AddItem(CollectibleItemObject);
        ItemHistory.LogUsed(SpawnWorldId);
    }
}
