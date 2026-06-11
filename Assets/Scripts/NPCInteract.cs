using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    [Header("NPC Data")]
    public string npcName;

    [TextArea(3, 6)]
    public string[] dialogs;

    [Header("State")]
    public bool alreadyTalked = false;

    bool playerNear = false;

    void Update()
    {
        if (!playerNear) return;
        if (alreadyTalked) return;
        if (NPCDialogManager.instance == null) return;
        if (NPCDialogManager.instance.IsTalking()) return;

        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            NPCDialogManager.instance.StartDialog(this);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNear = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNear = false;
    }
}