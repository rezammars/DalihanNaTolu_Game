using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [Header("UI")]
    public Text nameText;
    public Text dialogText;

    [Header("Dialog Data")]
    [TextArea(3, 6)]
    public string[] names;

    [TextArea(3, 6)]
    public string[] dialogs;

    [Header("Character Images")]
    public Image mcImage;
    public Image tetuaImage;

    [Header("Next Scene")]
    public string nextSceneName;

    [Header("Unlock Level")]
    public bool unlockLevelAfterDialog = false;

    public int unlockLevel = 2;

    int index = 0;

    void Start()
    {
        ShowDialog();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        
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
        Color active = Color.white;

        Color inactive =
        new Color(0.5f, 0.5f, 0.5f);

        if (string.IsNullOrEmpty(names[index]))
        {
            nameText.gameObject.SetActive(false);

            mcImage.gameObject.SetActive(false);

            tetuaImage.gameObject.SetActive(false);

            dialogText.alignment = TextAnchor.MiddleCenter;

            return;
        }

        nameText.gameObject.SetActive(true);

        mcImage.gameObject.SetActive(true);

        tetuaImage.gameObject.SetActive(true);

        dialogText.alignment = TextAnchor.UpperLeft;

        if (names[index].Trim().ToLower() == "alfredo")
        {
            mcImage.color = active;
            tetuaImage.color = inactive;
        }

        else
        {
            mcImage.color = inactive;
            tetuaImage.color = active;
        }
    }

    void NextDialog()
    {
        index++;

        if (index >= dialogs.Length)
        {
            if (unlockLevelAfterDialog)
            {
                UnlockLevel();
            }

            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            ShowDialog();
        }
    }

    void UnlockLevel()
    {
        int unlockedLevel =
        PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (unlockLevel > unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", unlockLevel);

            PlayerPrefs.Save();
        }
    }
}