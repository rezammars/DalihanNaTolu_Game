using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    [Header("NPC Data")]
    public string namaNPC;
    public Sprite npcDialogSprite;

    [TextArea(3, 6)]
    public string[] dialogs;

    [Header("Dialog Manager")]
    public NPCDialogManager dialogManager;

    [Header("Lanjut ke Puzzle")]
    public bool langsungKePuzzle = false;
    public FlowPanel panelFlowManager;
    public int nextStepIndex = 2;

    bool pemainDekat = false;

    void Update()
    {
        if (!pemainDekat) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interaksi NPC: " + namaNPC);

            if (langsungKePuzzle)
            {
                LanjutKePuzzle();
                return;
            }

            if (dialogManager != null)
            {
                dialogManager.MulaiDialog(namaNPC, npcDialogSprite, dialogs);
            }
            else
            {
                Debug.LogWarning("DialogManager belum diisi pada NPC: " + gameObject.name);
            }
        }
    }

    void LanjutKePuzzle()
    {
        if (panelFlowManager != null)
        {
            panelFlowManager.ShowStep(nextStepIndex);
        }
        else
        {
            Debug.LogWarning("PanelFlowManager belum diisi pada NPC: " + gameObject.name);
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