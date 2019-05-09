using UnityEngine;
using System;

public class ItemInFieldGraphic : MonoBehaviour, IRemotePrefab
{
    public void StartInteraction(Action OnComplete = null)
    {
        OnComplete?.Invoke();
    }
}
