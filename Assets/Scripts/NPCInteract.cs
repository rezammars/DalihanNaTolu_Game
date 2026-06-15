using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    [Header("NPC Data")]
    public string namaNPC;
    public Sprite npcDialogSprite;

    [TextArea(3, 6)]
    public string[] dialogs;

    public NPCDialogManager dialogManager;

    bool pemainDekat = false;

    void Update()
    {

        if (pemainDekat && (Input.GetKeyDown(KeyCode.E)))
        {
            Debug.Log("Tombol E ditekan dekat NPC: " + namaNPC);
            if (dialogManager != null)
            {
                dialogManager.MulaiDialog(namaNPC, npcDialogSprite, dialogs);
            }
            else
            {
                Debug.LogWarning("NPCDialogManager tidak ditemukan di scene.");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            pemainDekat = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            pemainDekat = false;
    }
}