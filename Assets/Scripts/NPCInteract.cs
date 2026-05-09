using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    public string npcName;
    public string[] dialogs;

    bool playerNear = false;

    // Update is called once per frame
    void Update()
    {
        if (playerNear && Input.GetMouseButtonDown(0))
        {
            DialogManager.instance.ShowDialog(npcName, dialogs);
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
