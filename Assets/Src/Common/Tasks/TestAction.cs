using UnityEngine;

public class TestAction : MonoBehaviour, IResponseTask
{
    public bool IsActive { get; private set; }
    public RESPONSE_TYPE ResponseType
    {
        get
        {
            return RESPONSE_TYPE.DEFAULT;
        }
    }

    public void Run(INTERACTIBLE_TYPE originType, Transform originTransform)
    {
        Debug.Log("It works!");
        Debug.Log(originType);
        Debug.Log(originTransform.name);

        IsActive = false;
    }

    public void Complete()
    {
        // ...
    }

    public void Next()
    {
        // ...
    }
}
