using UnityEngine;

public class IsItemChest : MonoBehaviour, IInteractible, ICollectible
{
    public string WorldId; // <-- TODO: Again this would be gotten from map data, has to be static.
    public bool IsOpen = false; // <-- Use an interface for things like this (ILockable) or something.

    public CollectibleItem _CollectibleItemObject;
    public EntityHistory ItemHistory;
    public GameObject GraphicalPrefab;

    public Transform Transform => transform;
    public INTERACTIBLE_TYPE InteractibleType => INTERACTIBLE_TYPE.COLLECTIBLE;
    public CollectibleItem CollectibleItemObject => _CollectibleItemObject;

    private IRemotePrefab RemotePrefabInstance;

    private void Start()
    {
        var spawned = Instantiate(GraphicalPrefab, transform.position, Quaternion.identity, transform);
        RemotePrefabInstance = spawned.GetComponent<IRemotePrefab>();

        if (ItemHistory.WasUsed(WorldId))
        {
            RemotePrefabInstance.StartInteraction();
        }

        IsOpen = spawned.GetComponent<ChestGraphic>().IsOpen;
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        if (!IsOpen)
        {
            ItemHistory.LogUsed(WorldId);
            RemotePrefabInstance.StartInteraction();
        }
    }
}
