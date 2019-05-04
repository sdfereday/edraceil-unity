using UnityEngine;

public class IsAKey : MonoBehaviour, IInteractible, ICollectible
{
    public InventoryHistory InventoryHistory;

    public Transform Transform
    {
        get
        {
            return transform;
        }
    }

    public string ItemName { get; private set; }

    public ITEM_TYPE ItemType
    {
        get
        {
            return ITEM_TYPE.KEY;
        }
    }

    public INTERACTIBLE_TYPE InteractibleType
    {
        get
        {
            return INTERACTIBLE_TYPE.COLLECTIBLE;
        }
    }

    private void Start()
    {
        // ...
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        // ...
        // Assuming collection was successful.
        Destroy(gameObject);
    }
}
