using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public Text mcName;
    public Text dialogText;
    [TextArea(3, 5)]
    public string[] dialogs;
    int dialogIndex = 0;

    void Start()
    {
        ShowDialog();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            NextDialog();
        }
    }
    void ShowDialog()
    {
        mcName.text = "Alfredo";
        dialogText.text = dialogs[dialogIndex];
    }

    void NextDialog()
    {
        dialogIndex++;
        if (dialogIndex >= dialogs.Length)
        {
            SceneManager.LoadScene("Level 1");
        }
        else
        {
            ShowDialog();
        }
    }
}
