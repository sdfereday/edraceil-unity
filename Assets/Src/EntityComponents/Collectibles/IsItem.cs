using UnityEngine;

/*
    Use this for when an item has been placed not in a chest, but statically
    like a chest in the game world. This is not the same type as say something
    that might spawn out of a dead enemy for example (since they don't need to
    be logged).
*/
public class IsItem : FieldEntity, IInteractible, ICollectible
{
    public CollectibleItem _CollectibleItemObject;
    public Transform Transform => transform;
    public INTERACTIBLE_TYPE GetInteractibleType() => INTERACTIBLE_TYPE.COLLECTIBLE;
    public CollectibleItem CollectibleItemObject => _CollectibleItemObject;
    
    public override void OnAssert(bool alreadyAcquired)
    {
        RemotePrefabInstance = GetComponent<IRemotePrefab>();

        if (alreadyAcquired)
            Destroy(gameObject);
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        if (CollectibleItemObject.IsKeyItem)
            throw new UnityException("Tried to add a key item to non-key item store. This is not allowed.");

        UpdateBoolState(true);

        RemotePrefabInstance.StartInteraction(() =>
        {
            Destroy(gameObject);
        });
    }
}
