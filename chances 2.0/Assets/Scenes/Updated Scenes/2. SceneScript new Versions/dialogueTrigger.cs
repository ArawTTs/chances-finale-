using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class dialogueTrigger : MonoBehaviour
{
    public NPCConversation TestConversation;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ConversationManager.Instance.StartConversation(TestConversation);
        }
    }
}
