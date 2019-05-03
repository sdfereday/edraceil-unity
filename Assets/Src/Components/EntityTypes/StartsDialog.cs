using UnityEngine;

public class StartsDialog : MonoBehaviour, IInteractible, IIdentifier
{
    public string Id;
    public string Identifier
    {
        get { return Id; }
    }

    public Transform Transform
    {
        get
        {
            return transform;
        }
    }

    public INTERACTIBLE_TYPE InteractibleType
    {
        get
        {
            return INTERACTIBLE_TYPE.DIALOG;
        }
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        // ...
    }
}
