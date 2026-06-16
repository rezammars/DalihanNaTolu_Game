using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CutsceneManager : MonoBehaviour
{
    [System.Serializable]
    public class CharacterData
    {
        public string characterName;
        public Image characterImage;
    }

    [Header("UI")]
    public Text nameText;
    public Text dialogText;
    public GameObject dialogPanel;
    public Text cutsceneText;
    public GameObject overlayGelap;

    [Header("Dialog Data")]
    public string[] names;

    [TextArea(3, 6)]
    public string[] dialogs;

    [Header("Characters")]
    public CharacterData[] characters;

    [Header("After Dialog Finished")]
    public UnityEvent onFinished;

    int index = 0;
    bool finished = false;

    void OnEnable()
    {
        index = 0;
        finished = false;
        ShowDialog();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        if (finished) return;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            NextDialog();
        }
    }

    void ShowDialog()
    {
        if (dialogs == null || dialogs.Length == 0)
        {
            Debug.LogWarning("Dialog belum diisi di " + gameObject.name);
            return;
        }

        if (index >= dialogs.Length) return;

        bool isNarration = string.IsNullOrEmpty(names[index]);

        if (isNarration)
        {
            if (overlayGelap != null)
                overlayGelap.SetActive(true);

            if (cutsceneText != null)
            {
                cutsceneText.gameObject.SetActive(true);
                cutsceneText.text = dialogs[index];
            }

            if (dialogPanel != null)
                dialogPanel.SetActive(false);
        }
        else
        {
            if (overlayGelap != null)
                overlayGelap.SetActive(false);
                
            if (cutsceneText != null)
                cutsceneText.gameObject.SetActive(false);

            if (dialogPanel != null)
                dialogPanel.SetActive(true);

            if (nameText != null)
                nameText.text = names[index];

            if (dialogText != null)
                dialogText.text = dialogs[index];
        }

        UpdateCharacterUI();
    }

    void UpdateCharacterUI()
    {
        if (characters == null) return;

        bool isNarration = string.IsNullOrEmpty(names[index]);

        foreach (CharacterData character in characters)
        {
            if (character.characterImage == null) continue;

            if (isNarration)
            {
                character.characterImage.gameObject.SetActive(false);
                continue;
            }

            character.characterImage.gameObject.SetActive(true);

            string currentSpeaker = names[index].Trim().ToLower();
            string characterName = character.characterName.Trim().ToLower();

            if (currentSpeaker == characterName)
                character.characterImage.color = Color.white;
            else
                character.characterImage.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    void NextDialog()
    {
        index++;

        if (index >= dialogs.Length)
        {
            FinishDialog();
        }
        else
        {
            ShowDialog();
        }
    }

    void FinishDialog()
    {
        finished = true;
        onFinished.Invoke();
    }
}