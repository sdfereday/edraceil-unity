using System;

public interface IRemotePrefab
{
    void StartInteraction(Action OnComplete = null);
}