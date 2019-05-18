using UnityEngine;

public class IsItemChest : FieldEntity, IInteractible, ICollectible
{
    public bool IsOpen = false; // <-- Use an interface for things like this (ILockable) or something.
    public SceneProp ScenePropObject;
    public CollectibleItem _CollectibleItemObject;

    public Transform Transform => transform;
    public INTERACTIBLE_TYPE InteractibleType => INTERACTIBLE_TYPE.COLLECTIBLE;
    public CollectibleItem CollectibleItemObject => _CollectibleItemObject;

    public override void OnAssert(bool truthy)
    {
        var spawned = Instantiate(ScenePropObject.GraphicalPrefab, transform.position, Quaternion.identity, transform);
        RemotePrefabInstance = spawned.GetComponent<IRemotePrefab>();

        if (truthy)
        {
            RemotePrefabInstance.StartInteraction();
        }
        
        IsOpen = spawned.GetComponent<ChestGraphic>().IsOpen;
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        if (!IsOpen)
        {
            UpdateBoolState(true);
            RemotePrefabInstance.StartInteraction();
        }
    }
}
