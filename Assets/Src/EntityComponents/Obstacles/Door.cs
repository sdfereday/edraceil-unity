using UnityEngine;

public class Door : FieldEntity, IInteractible
{
    // You may wish to have an 'open state' also for massive doors / bridges in other components.
    public bool IsUnlocked = false; // <-- Use an interface for things like this (ILockable) or something.
    public SceneProp ScenePropObject;
    public CollectibleItem ExpectedObjectInInventory;
    public PlayerKeyItemInventory KeyItemInventory;
    public Transform Transform => transform;
    public INTERACTIBLE_TYPE GetInteractibleType() => INTERACTIBLE_TYPE.DOORWAY;

    public override void OnAssert(bool alreadyUnlocked)
    {
        IsUnlocked = alreadyUnlocked && KeyItemInventory.HasItem(ExpectedObjectInInventory.Id);
        RemotePrefabInstance = GetComponent<IRemotePrefab>();
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        IsUnlocked = KeyItemInventory.HasItem(ExpectedObjectInInventory.Id);
        UpdateBoolState(IsUnlocked);

        if (IsUnlocked)
            RemotePrefabInstance.StartInteraction();
    }
}
