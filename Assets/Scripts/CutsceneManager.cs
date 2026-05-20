using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogText;

    [TextArea(3, 6)]
    public string[] names;

    [TextArea(3, 6)]
    public string[] dialogs;

    public Image mcImage;
    public Image tetuaImage;

    int index = 0;

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
        nameText.text = names[index];
        dialogText.text = dialogs[index];
        UpdateCharacterUI();
    }

    void UpdateCharacterUI()
    {
        Color active= Color.white;

        Color inactive = new Color(0.5f, 0.5f, 0.5f);

        if (string.IsNullOrEmpty(names[index]))
        {
            nameText.gameObject.SetActive(false);

            mcImage.gameObject.SetActive(false);

            tetuaImage.gameObject.SetActive(false);

            dialogText.alignment = TextAnchor.UpperLeft;

            return;
        }

        if (names[index].Trim().ToLower() == "alfredo")
        {
            mcImage.gameObject.SetActive(true);
            tetuaImage.gameObject.SetActive(true);

            mcImage.color = active;
            tetuaImage.color = inactive;

            dialogText.alignment = TextAnchor.UpperLeft;
        }
        else
        {
            mcImage.gameObject.SetActive(true);
            tetuaImage.gameObject.SetActive(true);

            mcImage.color = inactive;
            tetuaImage.color = active;

            dialogText.alignment = TextAnchor.UpperLeft;
        }
    }

    void NextDialog()
    {
        index++;
        if (index >= dialogs.Length)
        {
            SceneManager.LoadScene("Level 1");
        }
        else
        {
            ShowDialog();
        }
    }
}
