using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public GameObject dialogPanel;

    public Text npcName;
    public Text dialogText;

    string[] currentDialogs;
    int dialogIndex;

    void Awake()
    {
        instance = this;
    }

    public void ShowDialog(string name, string[] dialogs)
    {
        dialogPanel.SetActive(true);

        npcName.text = name;
        currentDialogs = dialogs;
        dialogIndex = 0;

        ShowCurrentDialog();
    }

    void ShowCurrentDialog()
    {
        dialogText.text = currentDialogs[dialogIndex];
    }
    
    public void NextDialog()
    {
        dialogIndex++;

        if (dialogIndex >= currentDialogs.Length)
        {
            CloseDialog();
        }
        else
        {
            ShowCurrentDialog();
        }
    }

    public void CloseDialog()
    {
        dialogPanel.SetActive(false);
    }
}