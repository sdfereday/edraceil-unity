using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatManager : MonoBehaviour
{
    public delegate void CompleteAction();
    public static event CompleteAction OnConversationComplete;

    public GameObject DialogBox;
    public Text NameField;
    public Text DialogueField;
    public GameObject NextButton;
    public GameObject ButtonA;
    public GameObject ButtonB;

    private ChatIterator chatIterator;
    private bool WaitingForChoices { get; set; }
    public bool IsActive { get; private set; }

    // TODO: Set up a simple animation slide in, as this is a bit crap.
    private void Awake() => DialogBox.SetActive(false);

    public void StartDialogue(string startPoint)
    {
        chatIterator = new ChatIterator(ConversationStub.Collection, OnChatComplete);

        IsActive = true;
        DialogBox.SetActive(IsActive);
        NextSentence(startPoint);
    }

    public void NextSentence(string startPoint = null)
    {
        NextButton.SetActive(false);
        ButtonA.SetActive(false);
        ButtonB.SetActive(false);

        if (WaitingForChoices || !IsActive) return;

        // If you try to call 'goToNext' and there's no 'to' set, things will error out. It could be
        // handled internally of course but it's just easier to see what's going on in here as you might
        // want different implementation.
        // OnChatComplete("notSet");
        ChatNode node = chatIterator.GoToNext(startPoint);

        Debug.Log(node.Text);
        DialogueField.text = node.Text;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(node));
    }

    IEnumerator TypeSentence(ChatNode node)
    {
        DialogueField.text = "";
        foreach (char letter in node.Text.ToCharArray())
        {
            DialogueField.text += letter;
            yield return null;
        }

        if (node.HasChoices)
        {
            WaitingForChoices = true;
            // Load choices available
            Debug.Log(node.Choices.Count);

            // TODO: You'll have to somehow pass things with the nodes here. Perhaps make
            // a small class to pass, or, some sort of event listener?
            // Or however many you need...
            ButtonA.SetActive(true);
            ButtonB.SetActive(true);
        } else
        {
            NextButton.SetActive(true);
        }
    }

    public void OnChatComplete(string endId)
    {
        IsActive = false;
        DialogBox.SetActive(IsActive);

        Debug.Log("Got chat complete message:");
        Debug.Log(endId);

        OnConversationComplete?.Invoke();
    }
}
