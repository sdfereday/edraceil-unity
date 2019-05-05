using UnityEngine;

public class IsItem : MonoBehaviour, IInteractible
{
    public Transform Transform => transform;
    public bool DestroyOnCollection = false;

    public INTERACTIBLE_TYPE _InteractibleType;
    public INTERACTIBLE_TYPE InteractibleType => _InteractibleType;

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        if (DestroyOnCollection)
            Destroy(gameObject);
    }
}
