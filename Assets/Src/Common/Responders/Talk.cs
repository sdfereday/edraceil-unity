using UnityEngine;

public class Talk : MonoBehaviour, IResponseTask
{
    public bool IsActive { get; private set; }
    public RESPONSE_TYPE ResponseType
    {
        get
        {
            return RESPONSE_TYPE.CONTINUOUS;
        }
    }

    public void OnChatComplete(string endId)
    {
        Debug.Log("Got chat complete message:");
        Debug.Log(endId);
        IsActive = false;
    }

    public void Run(INTERACTIBLE_TYPE originType, Transform originTransform)
    {
        var conversationStartPoint = originTransform.GetComponent<IIdentifier>().Identifier;
        var chatIterator = new ChatIterator(ConversationStub.Collection, OnChatComplete);
        IsActive = true;

        // Get first node
        var node = chatIterator.Start(conversationStartPoint);
        Debug.Log(node.Text);

        // If you try to call 'goToNext' and there's no 'to' set, things will error out. It could be
        // handled internally of course but it's just easier to see what's going on in here as you might
        // want different implementation.
        if (chatIterator.HasChoices(node)) {
            Debug.Log(node.Choices.Count);
        } else {
            var nextNode = chatIterator.GoToNext();
            Debug.Log(nextNode.Text);
        }
    }
    
    public void Complete()
    {
        // ...
    }

    public void Next()
    {
        // Testing
        OnChatComplete("notSet");
    }
}