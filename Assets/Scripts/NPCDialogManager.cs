using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NPCDialogManager : MonoBehaviour
{
    public static NPCDialogManager instance;

    [Header("UI")]
    public GameObject dialogPanel;
    public Text nameText;
    public Text dialogText;

    [Header("Player")]
    public PlayerMovement playerMovement;

    [Header("Progress")]
    public int totalNPC = 3;
    public UnityEvent onAllNPCFinished;

    string[] currentDialogs;
    int index = 0;

    bool isTalking = false;
    NPCInteract currentNPC;

    int talkedCount = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (dialogPanel != null)
            dialogPanel.SetActive(false);
    }

    void Update()
    {
        if (!isTalking) return;
        if (Time.timeScale == 0f) return;

        if (Input.GetMouseButtonDown(0) ||
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.E))
        {
            NextDialog();
        }
    }

    public void StartDialog(NPCInteract npc)
    {
        if (npc == null) return;

        currentNPC = npc;
        currentDialogs = npc.dialogs;
        index = 0;
        isTalking = true;

        if (dialogPanel != null)
            dialogPanel.SetActive(true);

        if (nameText != null)
            nameText.text = npc.npcName;

        if (playerMovement != null)
            playerMovement.canMove = false;

        ShowDialog();
    }

    void ShowDialog()
    {
        if (currentDialogs == null || currentDialogs.Length == 0) return;

        if (dialogText != null)
            dialogText.text = currentDialogs[index];
    }

    void NextDialog()
    {
        index++;

        if (index >= currentDialogs.Length)
            EndDialog();
        else
            ShowDialog();
    }

    void EndDialog()
    {
        isTalking = false;

        if (dialogPanel != null)
            dialogPanel.SetActive(false);

        if (playerMovement != null)
            playerMovement.canMove = true;

        if (currentNPC != null && !currentNPC.alreadyTalked)
        {
            currentNPC.alreadyTalked = true;
            talkedCount++;

            if (talkedCount >= totalNPC)
            {
                onAllNPCFinished.Invoke();
            }
        }

        currentNPC = null;
    }

    public bool IsTalking()
    {
        return isTalking;
    }
}