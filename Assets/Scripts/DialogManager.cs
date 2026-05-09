using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public GameObject dialogPanel;

    public Text npcName;
    public Text dialogText;

    void Awake()
    {
        instance = this;
    }

    public void ShowDialog(string name, string text)
    {
        dialogPanel.SetActive(true);

        npcName.text = name;
        dialogText.text = text;
    }

    public void CloseDialog()
    {
        dialogPanel.SetActive(false);
    }
}