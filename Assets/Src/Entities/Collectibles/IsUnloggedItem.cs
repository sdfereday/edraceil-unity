using UnityEngine;

/*
    This is the type of item that just gets instantiated and picked up without any
    logging being done. Sort of simulates what an enemy might pop out when you
    slay it, or some sort of exploding treasure chest of wonders.
*/
public class IsUnloggedItem : MonoBehaviour, IInteractible, ICollectible
{
    public CollectibleItem _CollectibleItemObject;
    public GameObject GraphicalPrefab; // TOOD: Possibly get this from collectible object?
    public bool DestroyPrefabOnCollection = false;

    public Transform Transform => transform;
    public INTERACTIBLE_TYPE InteractibleType => INTERACTIBLE_TYPE.COLLECTIBLE;
    public CollectibleItem CollectibleItemObject => _CollectibleItemObject;

    private IRemotePrefab RemotePrefabInstance;

    private void Start()
    {
            var spawned = Instantiate(GraphicalPrefab, transform.position, Quaternion.identity, transform);
            RemotePrefabInstance = spawned.GetComponent<IRemotePrefab>();
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        if (CollectibleItemObject.IsKeyItem)
            throw new UnityException("Tried to add a key item to non-key item store. This is not allowed.");

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
