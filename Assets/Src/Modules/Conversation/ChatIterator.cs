using System;
using System.Linq;
using System.Collections.Generic;

/*
 Okay so some ways to make this work the same yet simpler:
 - When the 'talk' response happens, fire up and pass in the start point
 id in to the iterator. Easy. This returns a node straight away relating
 to that particular 'thing'.
 - When you hit 'next', now unless you run in to a set of choices, you basically
 create a queue chain using the 'to' of each node.
 - When you first loaded the convo, this will have already generated said queue,
 so we deQueue the first item straight away, then when we hit 'next' again, we
 deQueue another and send that back. This carries on until either a) the end
 has been reached, or b) some choices have appeared.
 - When choices appear we're at the end of a queue, but we won't generate one
 until we've chosen one. This actively kicks off another conversation via
 the same means, only it doesn't close the current event. The minute you click,
 the minute you can queue the next chain of sentences.
 - You can also kick off different events and all sorts this way so it makes it
 a lot nicer to deal with. I'll re-write some of what's in here to fit this in,
 but for the most part it's almost there.
     */
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

    private ChatNode QueryNode(string query)
    {
        /// Consider setting up a queue (.Queue, Enqueue, etc)
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
        
        return nextNode;
    }

    private void Log<T>(T thing) => UnityEngine.Debug.Log(thing);

    private bool NodeDataNotValid(ChatNode node) => node.To == null && !HasChoices(node) && !node.Actions.Any(action => action == EndConversationAction);
    private bool NodeDataConflict(ChatNode node) => node.To != null && HasChoices(node);

    public bool HasChoices(ChatNode node) => node != null && node.Choices.Count() > 0;
    public bool HasActions(ChatNode node) => node != null && node.Actions.Count() > 0;

    private ChatNode PreviousNode;
    public ChatNode GoToNext(string query = null)
    {
        var NextNode = query != null ? QueryNode(query) : PreviousNode != null ? QueryNode(PreviousNode.To) : null;
        PreviousNode = NextNode;
        
        if (NextNode == null)
            throw new Exception("Couldn't find that node.");

        if (NextNode.HasActions && NextNode.Actions.Any(action => action == EndConversationAction))
        {
            if (NextNode.Actions.Any(action => action == SaveConversationAction))
            {
                // ... onSave, etc
                ChainPosition = CurrentNode.Id;
                Log("Saved chain up to ID.");
            }

            if (NextNode.Actions.Any(action => action == CancelConversationAction))
            {
                // ... onCancel, etc
                Log("Cancelled chain, nothing saved.");
            }

            Log("Reached end.");

            OnChatComplete?.Invoke(ChainPosition);

            return null;
        }

        return NextNode;
    }
}