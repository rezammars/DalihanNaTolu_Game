using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NPCDialogManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelDialog;
    public Image gambarNPC;
    public Text teksNama;
    public Text teksDialog;

    [Header("Player")]
    public PlayerMovement pergerakanPemain;

    string[] currentDialogs;
    int indexDialog = 0;
    bool isDialogAktif = false;

    UnityAction aksiSetelahDialog;

    void Start()
    {
        if (panelDialog != null)
            panelDialog.SetActive(false);
    }

    void Update()
    {
        if (!isDialogAktif) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            NextDialog();
        }
    }

    public void MulaiDialog(string namaNPC, Sprite spriteNPC, string[] dialogs, UnityAction onDialogFinished = null)
    {
        aksiSetelahDialog = onDialogFinished;

        if (panelDialog != null)
            panelDialog.SetActive(true);

        if (gambarNPC != null)
            gambarNPC.sprite = spriteNPC;

        if (teksNama != null)
            teksNama.text = namaNPC;

        currentDialogs = dialogs;
        indexDialog = 0;
        isDialogAktif = true;

        if (pergerakanPemain != null)
            pergerakanPemain.canMove = false;

        ShowDialog();
    }

    void ShowDialog()
    {
        if (currentDialogs == null || currentDialogs.Length == 0)
        {
            AkhiriDialog();
            return;
        }

        if (teksDialog != null)
            teksDialog.text = currentDialogs[indexDialog];
    }

    void NextDialog()
    {
        indexDialog++;

        if (indexDialog >= currentDialogs.Length)
            AkhiriDialog();
        else
            ShowDialog();
    }

    void AkhiriDialog()
    {
        isDialogAktif = false;

        if (panelDialog != null)
            panelDialog.SetActive(false);

        if (pergerakanPemain != null)
            pergerakanPemain.canMove = true;

        if (aksiSetelahDialog != null)
        {
            aksiSetelahDialog.Invoke();
            aksiSetelahDialog = null;
        }
    }
}