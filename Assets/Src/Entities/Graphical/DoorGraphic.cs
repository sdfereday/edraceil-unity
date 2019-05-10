﻿using UnityEngine;
using System;

public class DoorGraphic : MonoBehaviour, IRemotePrefab
{
    public bool IsOpen = false;
    public ToggledSprite ToggleSprite;

    private void Start()
    {
        if (IsOpen)
        {
            ToggleSprite.On();
        }
        else
        {
            ToggleSprite.Off();
        }
    }

    public void StartInteraction(Action OnComplete = null)
    {
        ToggleSprite.Off();
        IsOpen = !IsOpen;

        if (IsOpen)
        {
            ToggleSprite.On();
        }
        else
        {
            ToggleSprite.Off();
        }

        OnComplete?.Invoke();
    }
}
