using UnityEngine;

/*
    Use this for when an item has been placed not in a chest, but statically
    like a chest in the game world. This is not the same type as say something
    that might spawn out of a dead enemy for example (since they don't need to
    be logged).
*/
public class IsItem : MonoBehaviour, IInteractible, ICollectible
{
    public string WorldId;
    public CollectibleItem _CollectibleItemObject;
    public PlayerInventory CollectibleItemInventory;
    public EntityHistory ItemHistory;
    public GameObject GraphicalPrefab; // TOOD: Possibly get this from collectible object?
    public bool DestroyPrefabOnCollection = false;

    public Transform Transform => transform;
    public INTERACTIBLE_TYPE InteractibleType => INTERACTIBLE_TYPE.COLLECTIBLE;
    public CollectibleItem CollectibleItemObject => _CollectibleItemObject;

    private IRemotePrefab RemotePrefabInstance;

    private void Start()
    {
        if (!ItemHistory.WasUsed(WorldId))
        {
            // Spawn grapical prefab and enable interactions.
            var spawned = Instantiate(GraphicalPrefab, transform.position, Quaternion.identity, transform);
            RemotePrefabInstance = spawned.GetComponent<IRemotePrefab>();
        }
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        if (CollectibleItemObject.IsKeyItem)
            throw new UnityException("Tried to add a key item to non-key item store. This is not allowed.");

        CollectibleItemInventory.AddItem(CollectibleItemObject);
        ItemHistory.LogUsed(WorldId);

        /* So here what you might choose to do is create
         * some sort of transition of the item being
         received such as a simple chest or cutscene.
         When said action is done, we perform a callback
         with whatever we need. Such as reporting to
         messages, destroying things in the game world,
         etc. Whatever really. You can add the SO any time
         through other means too, it all gets added
         in the same way. */
        RemotePrefabInstance.StartInteraction(() =>
        {
            if (DestroyPrefabOnCollection)
                Destroy(gameObject);
        });
    }
}
