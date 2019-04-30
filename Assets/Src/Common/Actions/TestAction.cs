using UnityEngine;

public class TestAction : MonoBehaviour
{
    public void StartTesting(INTERACTIBLE_TYPE originType, Transform originTransform)
    {
        Debug.Log("It works!");
        Debug.Log(originType);
        Debug.Log(originTransform.name);
    }
}
