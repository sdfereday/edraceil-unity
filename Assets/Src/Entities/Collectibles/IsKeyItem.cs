using UnityEngine;

/*
    Use this for when a container (such as a chest, etc) will give the player
    a key item. Difference here is that it gets added to a special key item bag.
    Doesn't need a world ID since we can only have on type of this key item, so
    we can just check if we have one or not.
*/
public class IsKeyItem : FieldEntity, IInteractible, ICollectible
{
    public CollectibleItem _KeyItemObject;
    public bool DestroyPrefabOnCollection = false;

    public Transform Transform => transform;
    public INTERACTIBLE_TYPE InteractibleType => INTERACTIBLE_TYPE.COLLECTIBLE;
    public CollectibleItem CollectibleItemObject => _KeyItemObject;

    public override void OnAssert(bool truthy)
    {
        if (!truthy)
        {
            // Spawn graphical prefab and enable interactions
            var spawned = Instantiate(_KeyItemObject.GraphicalPrefab, transform.position, Quaternion.identity, transform);
            RemotePrefabInstance = spawned.GetComponent<IRemotePrefab>();
        }
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        if (!CollectibleItemObject.IsKeyItem)
            throw new UnityException("Tried to add a non-key item to the key item inventory, this isn't allowed.");

        UpdateBoolState(true);

        RemotePrefabInstance.StartInteraction(() =>
        {
            if (DestroyPrefabOnCollection)
                Destroy(gameObject);
        });
    }
}
