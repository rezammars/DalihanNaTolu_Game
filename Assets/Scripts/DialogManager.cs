using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public GameObject dialogPanel;
    public PlayerMovement playerMovement;

    public Text npcNameText;
    public Text dialogText;

    string[] currentDialogs;
    int dialogIndex;
    public bool isTalking = false;
    public NPCInteract currentNPC;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (isTalking)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                NextDialog();
            }
        }
    }

    public void StartDialog(string npcName, string[] dialogs)
    {
        dialogPanel.SetActive(true);

        isTalking = true;

        playerMovement.canMove = false;

        npcNameText.text = npcName;

        currentDialogs = dialogs;

        dialogIndex = 0;

        ShowDialog();
    }

    public void ShowDialog()
    {
        dialogText.text = currentDialogs[dialogIndex];
    }
    
    public void NextDialog()
    {
        dialogIndex++;

        if (dialogIndex >= currentDialogs.Length)
        {
           EndDialog();
        }
        else
        {
            ShowDialog();
        }
    }

    public void EndDialog()
    {
        dialogPanel.SetActive(false);
        isTalking = false;
        playerMovement.canMove = true;

        if (currentNPC != null && !currentNPC.alreadyTalked)
        {
            currentNPC.alreadyTalked = true;
            Debug.Log("Dialog NPC selesai: " + currentNPC.npcName);
            LevelCompleted.instance.NPCFinished();
            currentNPC = null;
        }
    }
}