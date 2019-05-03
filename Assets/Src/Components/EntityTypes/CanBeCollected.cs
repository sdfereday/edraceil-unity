using UnityEngine;

public class CanBeCollected : MonoBehaviour, IInteractible, ICollectible
{
    public Transform Transform
    {
        get
        {
            return transform;
        }
    }

    public ITEM_TYPE _itemType;
    public ITEM_TYPE ItemType
    {
        get
        {
            return _itemType;
        }
    }

    public INTERACTIBLE_TYPE InteractibleType
    {
        get
        {
            return INTERACTIBLE_TYPE.COLLECTIBLE;
        }
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        Destroy(gameObject);
    }
}
