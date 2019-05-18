using UnityEngine;

public abstract class FieldEntity : MonoBehaviour
{
    protected SaveState UseState;
    protected IRemotePrefab RemotePrefabInstance;

    public virtual void Start()
    {
        UseState = GetComponent<SaveState>();

        if (UseState != null)
        {
            OnAssert(UseState.IsTruthy);
        } else
        {
            OnAssert(false);
        }
    }

    protected void UpdateBoolState(bool state) => UseState.UpdateBoolState(state);

    public abstract void OnAssert(bool truthy);
}
