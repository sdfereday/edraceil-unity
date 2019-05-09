using UnityEngine;
using System;

public class ItemInField : MonoBehaviour, IRemotePrefab
{
    public void StartInteraction(Action OnComplete = null)
    {
        OnComplete?.Invoke();
    }
}
