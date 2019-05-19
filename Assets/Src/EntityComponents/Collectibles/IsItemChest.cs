﻿using UnityEngine;

public class IsItemChest : FieldEntity, IInteractible, ICollectible
{
    public bool IsOpen = false; // <-- Use an interface for things like this (ILockable) or something.
    public SceneProp ScenePropObject; // TODO: Is this being used?
    public CollectibleItem _CollectibleItemObject;
    public CollectibleItem CollectibleItemObject => _CollectibleItemObject;
    public Transform Transform => transform;
    public INTERACTIBLE_TYPE GetInteractibleType() => INTERACTIBLE_TYPE.COLLECTIBLE;

    public override void OnAssert(bool alreadyOpened)
    {      
        GetComponent<RemoteTwoStateTemplate>().SetActive(alreadyOpened);
        IsOpen = alreadyOpened;
    }

    public void Use(Collider2D collider, INPUT_TYPE inputType)
    {
        if (!IsOpen)
        {
            UpdateBoolState(true);
            RemotePrefabInstance.StartInteraction();
        }
    }
}
