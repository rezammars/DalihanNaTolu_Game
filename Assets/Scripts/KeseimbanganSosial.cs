using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class KeseimbanganSosial : MonoBehaviour
{
    [System.Serializable]
    public class OptionData
    {
        public string optionText;

        [Header("Efek Emosi")]
        public int efekRajaParhata;
        public int efekHulaHulaA;
        public int efekHulaHulaB;
        public int efekDonganA;
        public int efekDonganB;
        public int efekAnakBoru;

        [Header("UI")]
        public Button button;
        public Text buttonText;
    }

    [Header("NPC Status")]
    public NPCStatusUI rajaParhata;
    public NPCStatusUI hulaHulaA;
    public NPCStatusUI hulaHulaB;
    public NPCStatusUI donganA;
    public NPCStatusUI donganB;
    public NPCStatusUI anakBoru;

    [Header("Option Data")]
    public OptionData[] options;

    [Header("Control Button")]
    public Button lanjutButton;

    [Header("Result")]
    public GameObject resultPanel;
    public Text resultText;

    [Header("Rules")]
    public int maxPilihan = 3;

    [Header("After Complete")]
    public UnityEvent onPuzzleComplete;

    List<int> selectedOptions = new List<int>();
    bool finished = false;

    void OnEnable()
    {
        ResetGame();
    }

    void ResetGame()
    {
        CancelInvoke();

        finished = false;
        selectedOptions.Clear();

        if (resultPanel != null)
            resultPanel.SetActive(false);

        SetupNPC();
        SetupOptions();
        SetupLanjutButton();
    }

    void SetupNPC()
    {
        rajaParhata.Setup("Raja Parhata", "Tetua Adat", 0);
        hulaHulaA.Setup("Hula-hula A", "Hula-hula", -2);
        hulaHulaB.Setup("Hula-hula B", "Hula-hula", 0);
        donganA.Setup("Dongan Tubu A", "Semarga", -1);
        donganB.Setup("Dongan Tubu B", "Semarga", 0);
        anakBoru.Setup("Anak Boru", "Pelayan Adat", -1);
    }

    void SetupOptions()
    {
        for (int i = 0; i < options.Length; i++)
        {
            int index = i;

            if (options[i].buttonText != null)
                options[i].buttonText.text = options[i].optionText;

            if (options[i].button != null)
            {
                options[i].button.onClick.RemoveAllListeners();
                options[i].button.onClick.AddListener(() => ToggleOption(index));
                options[i].button.interactable = true;
                SetButtonNormal(options[i].button);
            }
        }
    }

    void SetupLanjutButton()
    {
        if (lanjutButton == null) return;

        lanjutButton.onClick.RemoveAllListeners();
        lanjutButton.onClick.AddListener(FinishPuzzle);
        lanjutButton.interactable = false;
    }

    void ToggleOption(int index)
    {
        if (finished) return;
        if (index < 0 || index >= options.Length) return;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayKlikTombol();

        if (selectedOptions.Contains(index))
        {
            selectedOptions.Remove(index);
            ApplyOptionEffect(index, -1);
            SetButtonNormal(options[index].button);
        }
        else
        {
            if (selectedOptions.Count >= maxPilihan)
                return;

            selectedOptions.Add(index);
            ApplyOptionEffect(index, 1);
            SetButtonSelected(options[index].button);
        }

        UpdateLanjutButton();
    }

    void ApplyOptionEffect(int index, int multiplier)
    {
        OptionData option = options[index];

        rajaParhata.AddEmotion(option.efekRajaParhata * multiplier);
        hulaHulaA.AddEmotion(option.efekHulaHulaA * multiplier);
        hulaHulaB.AddEmotion(option.efekHulaHulaB * multiplier);
        donganA.AddEmotion(option.efekDonganA * multiplier);
        donganB.AddEmotion(option.efekDonganB * multiplier);
        anakBoru.AddEmotion(option.efekAnakBoru * multiplier);
    }

    void UpdateLanjutButton()
    {
        if (lanjutButton != null)
            lanjutButton.interactable = selectedOptions.Count > 0;
    }

    void FinishPuzzle()
    {
        if (finished) return;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayKlikTombol();

        finished = true;

        DisableAllOptions();

        if (lanjutButton != null)
            lanjutButton.interactable = false;

        if (resultPanel != null)
            resultPanel.SetActive(true);

        bool allAtLeastNeutral = CheckAllAtLeastNeutral();
        bool hulaHulaHappy = CheckHulaHulaHappy();

        if (allAtLeastNeutral && hulaHulaHappy)
        {
            if (resultText != null)
            {
                resultText.text =
                    "Prosesi berjalan harmonis.\n" +
                    "Semua pihak sudah tenang, dan Hula-hula merasa dihormati.";
            }

            Invoke(nameof(CompletePuzzle), 2f);
        }
        else if (allAtLeastNeutral)
        {
            if (resultText != null)
            {
                resultText.text =
                    "Suasana sudah cukup stabil.\n" +
                    "Namun Hula-hula belum sepenuhnya merasa dihormati.";
            }

            Invoke(nameof(CompletePuzzle), 2f);
        }
        else
        {
            if (resultText != null)
            {
                resultText.text =
                    "Keseimbangan belum tercapai.\n" +
                    "Masih ada pihak yang merasa kecewa.";
            }

            Invoke(nameof(ResetGame), 2f);
        }
    }

    bool CheckAllAtLeastNeutral()
    {
        return
            rajaParhata.emotionValue >= 0 &&
            hulaHulaA.emotionValue >= 0 &&
            hulaHulaB.emotionValue >= 0 &&
            donganA.emotionValue >= 0 &&
            donganB.emotionValue >= 0 &&
            anakBoru.emotionValue >= 0;
    }

    bool CheckHulaHulaHappy()
    {
        return
            hulaHulaA.emotionValue >= 1 &&
            hulaHulaB.emotionValue >= 1;
    }

    void DisableAllOptions()
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i].button != null)
                options[i].button.interactable = false;
        }
    }

    void SetButtonSelected(Button button)
    {
        if (button == null) return;

        Image image = button.GetComponent<Image>();

        if (image != null)
            image.color = new Color(0.65f, 0.85f, 1f, 1f);
    }

    void SetButtonNormal(Button button)
    {
        if (button == null) return;

        Image image = button.GetComponent<Image>();

        if (image != null)
            image.color = Color.white;
    }

    void CompletePuzzle()
    {
        onPuzzleComplete.Invoke();
    }
}