using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Import the namespace for UnityEvent
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;

    // Define UnityEvents for start and end of the conversation
    public UnityEvent onConversationStart;
    public UnityEvent onConversationEnd;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            // Invoke the start event and start the conversation
            onConversationStart.Invoke();
            ConversationManager.Instance.StartConversation(myConversation);
            // Optional: You may want to unsubscribe and then resubscribe to ensure it's only called once per conversation
            ConversationManager.OnConversationEnded -= OnConversationEnded;
            ConversationManager.OnConversationEnded += OnConversationEnded;
        }
    }

    private void OnConversationEnded()
    {
        // Invoke the end event
        onConversationEnd.Invoke();
        // Unsubscribe from the event
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }
}
