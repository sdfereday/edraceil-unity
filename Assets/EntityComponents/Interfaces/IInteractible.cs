using UnityEngine;

public interface IInteractible
{
    void Use(Collider2D collider, INPUT_TYPE inputType);
    INTERACTIBLE_TYPE InteractibleType { get; }
}