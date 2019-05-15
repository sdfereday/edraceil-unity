using UnityEngine;

public class IsItemChest : MonoBehaviour, IInteractible, ICollectible
{
    public CollectibleItem _CollectibleItemObject;
    public GameObject GraphicalPrefab;
    public bool IsOpen = false; // <-- Use an interface for things like this (ILockable) or something.

    public Transform Transform => transform;
    public INTERACTIBLE_TYPE InteractibleType => INTERACTIBLE_TYPE.COLLECTIBLE;
    public CollectibleItem CollectibleItemObject => _CollectibleItemObject;

    private SaveState UseState;
    private IRemotePrefab RemotePrefabInstance;

    private void Start()
    {
        UseState = GetComponent<SaveState>();

        var spawned = Instantiate(GraphicalPrefab, transform.position, Quaternion.identity, transform);
        RemotePrefabInstance = spawned.GetComponent<IRemotePrefab>();

        if (UseState.IsTruthy)
        {
            RemotePrefabInstance.StartInteraction();
        }
        
        IsOpen = spawned.GetComponent<ChestGraphic>().IsOpen;
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        if (!IsOpen)
        {
            UseState.UpdateBoolState(true);
            RemotePrefabInstance.StartInteraction();
        }
    }
}
