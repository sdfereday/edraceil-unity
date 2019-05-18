using UnityEngine;

/*
    This is the type of item that just gets instantiated and picked up without any
    logging being done. Sort of simulates what an enemy might pop out when you
    slay it, or some sort of exploding treasure chest of wonders.
*/
public class IsUnloggedItem : FieldEntity, IInteractible, ICollectible
{
    public CollectibleItem _CollectibleItemObject;
    public bool DestroyPrefabOnCollection = false;

    public Transform Transform => transform;
    public INTERACTIBLE_TYPE InteractibleType => INTERACTIBLE_TYPE.COLLECTIBLE;
    public CollectibleItem CollectibleItemObject => _CollectibleItemObject;

    public override void OnAssert(bool truthy)
    {
        var spawned = Instantiate(_CollectibleItemObject.GraphicalPrefab, transform.position, Quaternion.identity, transform);
        RemotePrefabInstance = spawned.GetComponent<IRemotePrefab>();
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        if (CollectibleItemObject.IsKeyItem)
            throw new UnityException("Tried to add a key item to non-key item store. This is not allowed.");

        RemotePrefabInstance.StartInteraction(() =>
        {
            if (DestroyPrefabOnCollection)
                Destroy(gameObject);
        });
    }
}
