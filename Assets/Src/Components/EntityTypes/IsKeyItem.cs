using UnityEngine;

public class IsKeyItem : MonoBehaviour, IInteractible, ICollectible
{
    public KeyItemHistory KeyItemHistory;

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
            return ITEM_TYPE.KEY_ITEM;
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
        // ... check if already collected, etc
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        // ...
        // Assuming collection was successful.
        Destroy(gameObject);
    }
}
