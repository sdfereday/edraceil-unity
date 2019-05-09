using UnityEngine;

public class StartsDialogue : MonoBehaviour, IInteractible, IIdentifier
{
    public string Id;
    public string Identifier => Id;

    public Transform Transform => transform;
    public INTERACTIBLE_TYPE InteractibleType => INTERACTIBLE_TYPE.DIALOGUE;

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        // ...
    }
}
