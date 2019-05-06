using UnityEngine;

public class UnlockableDoor : MonoBehaviour, IInteractible
{
    public string WorldId; // <-- TODO: Again this would be gotten from map data, has to be static.

    // You may wish to have an 'open state' also for massive doors / bridges in other components.
    public bool IsUnlocked = false; // <-- Use an interface for things like this (ILockable) or something.
    
    public CollectibleItem ExpectedObjectInInventory;
    public PlayerKeyItemInventory KeyItemInventory;

    // Everything that has a state in the map needs to register its change to the history data to be saved.
    public EntityHistory HistoryData;

    public Transform Transform => transform;

    public INTERACTIBLE_TYPE InteractibleType => INTERACTIBLE_TYPE.DOORWAY;

    private void Start()
    {
        IsUnlocked = HistoryData.WasUsed(WorldId);
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        if (IsUnlocked)
        {
            // Do something else
            return;
        }

        if (KeyItemInventory.HasItem(ExpectedObjectInInventory.Id))
        {
            Debug.Log("Should unlock the door.");
        } else
        {
            Debug.Log("Needs a key to be unlocked.");
        }
    }
}
