using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class CharacterData
{
    public string characterName;
    public Image characterImage;
}

public class CutsceneManager : MonoBehaviour
{
    [Header("UI")]
    public Text nameText;
    public Text dialogText;
    public GameObject dialogPanel;
    public Text cutsceneText;

    [Header("Dialog Data")]
    [TextArea(3, 6)]
    public string[] names;

    [TextArea(3, 6)]
    public string[] dialogs;

    [Header("Characters")]
    public CharacterData[] characters;

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
        if (index >= dialogs.Length) return;
        if (string.IsNullOrEmpty(names[index]))
        {
            cutsceneText.text = dialogs[index];
        }
        else
        {
            nameText.text = names[index];
            dialogText.text = dialogs[index];
        }

        UpdateCharacterUI();
    }

    void UpdateCharacterUI()
    {
        Color active = Color.white;
        Color inactive = new Color(0.5f, 0.5f, 0.5f);

        bool isNarration = string.IsNullOrEmpty(names[index]);

        if (isNarration)
        {
            cutsceneText.gameObject.SetActive(true);
            dialogPanel.SetActive(false);

            foreach (CharacterData character in characters)
            {
                if (character.characterImage != null)
                {
                    character.characterImage.gameObject.SetActive(false);
                }
            }

            return;
        }

        cutsceneText.gameObject.SetActive(false);
        dialogPanel.SetActive(true);
        nameText.gameObject.SetActive(true);
        string currentSpeaker = names[index].Trim().ToLower();

        foreach (CharacterData character in characters)
        {
            if (character.characterImage == null) continue;

            character.characterImage.gameObject.SetActive(true);

            string characterName = character.characterName.Trim().ToLower();

            if (characterName == currentSpeaker)
            {
                character.characterImage.color = active;
            }
            else
            {
                character.characterImage.color = inactive;
            }
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