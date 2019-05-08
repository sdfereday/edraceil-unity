using UnityEngine;

public class Talk : MonoBehaviour, IResponseTask
{
    private string conversationStartPoint;
    private ChatIterator chatIterator;
    public bool IsActive { get; private set; }
    public RESPONSE_TYPE ResponseType
    {
        get
        {
            return RESPONSE_TYPE.CONTINUOUS;
        }
    }

    public PlayerInput playerInput;

    public void OnChatComplete(string endId)
    {
        Debug.Log("Got chat complete message:");
        Debug.Log(endId);

        playerInput.ToggleMovement(true);
        IsActive = false;
    }

    public void Run(INTERACTIBLE_TYPE originType, Transform originTransform)
    {
        // TODO: Maybe make the ID related to conversation, not just generic.
        conversationStartPoint = originTransform.GetComponent<IIdentifier>().Identifier;
        chatIterator = new ChatIterator(ConversationStub.Collection, OnChatComplete);

        // Disable movement on player whilst talking (or give auto control if cutscene)
        playerInput.ToggleMovement(false);
        IsActive = true;
        
        // Start the thing up (buttons will load after initial)
        NextSentence(conversationStartPoint);
    }

    public void NextSentence(string startPoint = null)
    {
        // If you try to call 'goToNext' and there's no 'to' set, things will error out. It could be
        // handled internally of course but it's just easier to see what's going on in here as you might
        // want different implementation.
        // OnChatComplete("notSet");
        var node = chatIterator.GoToNext(startPoint);

        // Probably finished (or errored)
        if (node == null) return;

        Debug.Log(node.Text);

        if (node.HasChoices)
        {
            // Load choices available
            Debug.Log(node.Choices.Count);
        }
    }

    public void Next()
    {
        // ...
    }

    public void Complete()
    {
        // ...
    }
}