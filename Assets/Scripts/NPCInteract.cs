using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    public string npcName;
    [TextArea(3, 5)]
    public string[] dialogs;

    bool playerNear = false;
    public bool alreadyTalked = false;

    // Update is called once per frame
    void Update()
    {
        if (playerNear)
        {
            Debug.Log("Player dekat NPC: " + npcName);
        }

        if (playerNear && !DialogManager.instance.isTalking && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
        {
            Debug.Log("Mulai dialog NPC: " + npcName);

            DialogManager.instance.StartDialog(npcName, dialogs);

            if (!alreadyTalked)
            {

                DialogManager.instance.currentNPC = this;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}
