using UnityEngine;

/*
    Use this for when a container (such as a chest, etc) will give the player
    a key item. Difference here is that it gets added to a special key item bag.
    Doesn't need a world ID since we can only have on type of this key item, so
    we can just check if we have one or not.
*/
public class IsKeyItem : MonoBehaviour, IInteractible, ICollectible
{
    public SaveState UseState;
    public CollectibleItem _KeyItemObject;
    public GameObject GraphicalPrefab; // TOOD: Possibly get this from collectible object?
    public bool DestroyPrefabOnCollection = false;

    public Transform Transform => transform;
    public INTERACTIBLE_TYPE InteractibleType => INTERACTIBLE_TYPE.COLLECTIBLE;
    public CollectibleItem CollectibleItemObject => _KeyItemObject;
    
    private IRemotePrefab RemotePrefabInstance;

    private void Start()
    {
        if (!UseState.IsTruthy)
        {
            // Spawn graphical prefab and enable interactions
            var spawned = Instantiate(GraphicalPrefab, transform.position, Quaternion.identity, transform);
            RemotePrefabInstance = spawned.GetComponent<IRemotePrefab>();
        }
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        if (!CollectibleItemObject.IsKeyItem)
            throw new UnityException("Tried to add a non-key item to the key item inventory, this isn't allowed.");

        UseState.UpdateBoolState(true);

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
