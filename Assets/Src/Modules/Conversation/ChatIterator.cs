using System;
using System.Linq;
using System.Collections.Generic;

public class ChatIterator
{
    private const string EndConversationAction = "endConversation";
    private const string SaveConversationAction = "save";
    private const string CancelConversationAction = "cancel";

    public string ChainPosition { get; private set; }
    private List<ChatNode> Collection { get; set; }
    private ChatNode CurrentNode { get; set; }
    private Action<string> OnChatComplete { get; set; }

    public ChatIterator(List<ChatNode> _collection, Action<string> _onChatComplete = null)
    {
        Collection = new List<ChatNode>(_collection);
        OnChatComplete = _onChatComplete;
    }

    private ChatNode QueryAndAssignNode(string query)
    {
        // This will never (at present) scan for choice nodes (may not ever need to).
        var nextNode = Collection.FirstOrDefault(node => node.Id == query);

        if (nextNode == null)
        {
            Log("There was a problem finding a node. Try running 'start' first.");
            return null;
        }

        if (NodeDataNotValid(nextNode))
        {
            Log("The current node is invalid. It must have a 'to' OR 'choices', or, an 'endConversation' action if this was intended.");
            return null;
        }

        if (NodeDataConflict(nextNode))
        {
            Log("The current node is invalid. It must have either 'to' OR 'choices', and not both.");
            return null;
        }

        CurrentNode = nextNode;
        return CurrentNode;
    }

    private bool NodeDataNotValid(ChatNode node)
    {
        return node.To == null && !HasChoices(node) && !node.Actions.Any(action => action == EndConversationAction);
    }

    private bool NodeDataConflict(ChatNode node)
    {
        return node.To != null && HasChoices(node);
    }

    private void Log<T>(T thing)
    {
        UnityEngine.Debug.Log(thing);
    }

    public bool HasChoices(ChatNode node)
    {
        return node != null && node.Choices.Count() > 0;
    }

    public bool HasActions(ChatNode node)
    {
        return node != null && node.Actions.Count() > 0;
    }

    public ChatNode GoToNext()
    {
        // Without a 'to', you cannot proceed to the next node.
        if (HasChoices(CurrentNode) && CurrentNode.To == null)
        {
            Log("This node has no 'next', however it does have choices that can be used.");
            return CurrentNode;
        }

        // TODO: Please use consts. We also make sure to check if the endConvo action is present. This is really important to remember to add.
        if (CurrentNode.Actions.Any(action => action == EndConversationAction))
        {
            if (CurrentNode.Actions.Any(action => action == SaveConversationAction))
            {
                // ... onSave, etc
                ChainPosition = CurrentNode.Id;
                Log("Saved chain up to ID.");
            }

            if (CurrentNode.Actions.Any(action => action == CancelConversationAction))
            {
                // ... onCancel, etc
                Log("Cancelled chain, nothing saved.");
            }

            Log("Reached end.");

            OnChatComplete?.Invoke(ChainPosition);
        }

        return QueryAndAssignNode(CurrentNode.To);
    }

    public ChatNode GoToExact(string query)
    {
        return QueryAndAssignNode(query);
    }

    public ChatNode Start(string startId)
    {
        return GoToExact(startId);
    }
}