﻿using UnityEngine;

public interface IInteractible
{
    void Use(Collider2D collider, INPUT_TYPE inputType);
    Transform Transform { get; }

    INTERACTIBLE_TYPE GetInteractibleType();
}