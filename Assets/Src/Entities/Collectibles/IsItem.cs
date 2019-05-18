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
    public bool DestroyPrefabOnCollection = false;

    public Transform Transform => transform;
    public INTERACTIBLE_TYPE InteractibleType => INTERACTIBLE_TYPE.COLLECTIBLE;
    public CollectibleItem CollectibleItemObject => _CollectibleItemObject;
    
    public override void OnAssert(bool truthy)
    {
        if (!truthy)
        {
            // Spawn grapical prefab and enable interactions.
            var spawned = Instantiate(_CollectibleItemObject.GraphicalPrefab, transform.position, Quaternion.identity, transform);
            RemotePrefabInstance = spawned.GetComponent<IRemotePrefab>();
        }
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        if (CollectibleItemObject.IsKeyItem)
            throw new UnityException("Tried to add a key item to non-key item store. This is not allowed.");

        UpdateBoolState(true);

        RemotePrefabInstance.StartInteraction(() =>
        {
            if (DestroyPrefabOnCollection)
                Destroy(gameObject);
        });
    }
}
