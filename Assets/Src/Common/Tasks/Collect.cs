using UnityEngine;

public class Collect : MonoBehaviour, IResponseTask
{
    public PlayerInventory Inventory;

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
        var itemData = originTransform.GetComponent<ICollectible>();

        Debug.Log("Collected an item:");
        Debug.Log(itemData);

        Inventory.AddItem(itemData.CollectibleItemObject);
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
