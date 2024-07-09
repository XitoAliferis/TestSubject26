using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeTrigger : MonoBehaviour
{
    public Dialouge dialouge;


    private void OnTriggerEnter(Collider other)
    {
        // Check if the GameObject entering the trigger is the player
        // This can be done by comparing tags. Make sure your player GameObject is tagged appropriately
        if (other.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        FindAnyObjectByType<DialougeManager>().StartDialouge(dialouge);
    }

}
