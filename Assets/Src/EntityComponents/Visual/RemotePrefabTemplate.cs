using UnityEngine;
using System;

public class RemotePrefabTemplate : MonoBehaviour, IRemotePrefab
{
    public void StartInteraction(Action OnComplete = null)
    {
        OnComplete?.Invoke();
    }
}
