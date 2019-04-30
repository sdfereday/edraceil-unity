using UnityEngine;

public interface IInteractible
{
    Transform Use(Collider2D collider, INPUT_TYPE inputType);
    INTERACTIBLE_TYPE InteractibleType { get; }
}