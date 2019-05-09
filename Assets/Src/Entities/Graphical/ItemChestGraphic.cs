using UnityEngine;
using System;
using System.Collections;

public class ItemChestGraphic : MonoBehaviour, IRemotePrefab
{
    public bool IsOpen = false;

    public void StartInteraction(Action OnComplete = null)
    {
        StartCoroutine(Countdown(OnComplete));
    }

    private IEnumerator Countdown(Action OnComplete = null)
    {
        // Wait for animation simulation.
        yield return new WaitForSeconds(1);

        IsOpen = true;
        OnComplete?.Invoke();
    }
}
