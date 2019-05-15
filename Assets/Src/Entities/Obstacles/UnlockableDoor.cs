using UnityEngine;

public class UnlockableDoor : MonoBehaviour, IInteractible
{
    // You may wish to have an 'open state' also for massive doors / bridges in other components.
    public bool IsUnlocked = false; // <-- Use an interface for things like this (ILockable) or something.
    
    public CollectibleItem ExpectedObjectInInventory;
    public PlayerKeyItemInventory KeyItemInventory;
    public GameObject GraphicalPrefab;

    public Transform Transform => transform;
    public INTERACTIBLE_TYPE InteractibleType => INTERACTIBLE_TYPE.DOORWAY;

    public SaveState UseState;
    private IRemotePrefab RemotePrefabInstance;

    private void Start()
    {
        UseState = GetComponent<SaveState>();

        // TODO: Currently broken since inventory isn't saved (this needs an SO also).
        IsUnlocked = UseState.IsTruthy && KeyItemInventory.HasItem(ExpectedObjectInInventory.Id);

        var spawned = Instantiate(GraphicalPrefab, transform.position, Quaternion.identity, transform);
        RemotePrefabInstance = spawned.GetComponent<IRemotePrefab>();
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        IsUnlocked = KeyItemInventory.HasItem(ExpectedObjectInInventory.Id);
        UseState.UpdateBoolState(IsUnlocked);

        if (IsUnlocked)
            RemotePrefabInstance.StartInteraction();
    }
}
