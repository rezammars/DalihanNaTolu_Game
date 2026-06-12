using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class KeseimbanganSosial : MonoBehaviour
{
    [Header("NPC Status")]
    public NPCStatusUI rajaParhata;
    public NPCStatusUI hulaHulaA;
    public NPCStatusUI hulaHulaB;
    public NPCStatusUI donganA;
    public NPCStatusUI donganB;
    public NPCStatusUI anakBoru;

    [Header("Option Buttons")]
    public Button option1Button;
    public Button option2Button;
    public Button option3Button;
    public Button option4Button;
    public Button option5Button;
    public Button option6Button;

    [Header("Option Text")]
    public Text option1Text;
    public Text option2Text;
    public Text option3Text;
    public Text option4Text;
    public Text option5Text;
    public Text option6Text;

    [Header("UI")]
    public Text feedbackText;
    public Text progressText;
    public GameObject resultPanel;
    public Text resultText;

    [Header("Rules")]
    public int maxStep = 3;
    public int successScore = 5;

    [Header("After Complete")]
    public UnityEvent onPuzzleComplete;

    int step = 0;
    bool finished = false;

    bool usedOption1 = false;
    bool usedOption2 = false;
    bool usedOption3 = false;
    bool usedOption4 = false;
    bool usedOption5 = false;
    bool usedOption6 = false;

    void OnEnable()
    {
        ResetGame();
    }

    void ResetGame()
    {
        step = 0;
        finished = false;

        usedOption1 = false;
        usedOption2 = false;
        usedOption3 = false;
        usedOption4 = false;
        usedOption5 = false;
        usedOption6 = false;

        if (resultPanel != null)
            resultPanel.SetActive(false);

        SetupNPC();
        SetupOptions();
        UpdateProgress();

        if (feedbackText != null)
            feedbackText.text = "Pilih tindakan yang dapat menjaga keseimbangan sosial.";
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
        option1Text.text = "Berikan Ulos kepada Hula-hula";
        option2Text.text = "Dengarkan Keluhan Dongan Tubu";
        option3Text.text = "Minta Nasihat Raja Parhata";
        option4Text.text = "Adakan Musyawarah";
        option5Text.text = "Mulai Acara Terlalu Cepat";
        option6Text.text = "Tugaskan Anak Boru Membantu Persiapan";

        option1Button.interactable = true;
        option2Button.interactable = true;
        option3Button.interactable = true;
        option4Button.interactable = true;
        option5Button.interactable = true;
        option6Button.interactable = true;

        option1Button.onClick.RemoveAllListeners();
        option2Button.onClick.RemoveAllListeners();
        option3Button.onClick.RemoveAllListeners();
        option4Button.onClick.RemoveAllListeners();
        option5Button.onClick.RemoveAllListeners();
        option6Button.onClick.RemoveAllListeners();

        option1Button.onClick.AddListener(ChooseGiveUlos);
        option2Button.onClick.AddListener(ChooseListenDongan);
        option3Button.onClick.AddListener(ChooseAskRaja);
        option4Button.onClick.AddListener(ChooseMusyawarah);
        option5Button.onClick.AddListener(ChooseStartTooFast);
        option6Button.onClick.AddListener(ChooseBoruHelp);
    }

    void UpdateProgress()
    {
        if (progressText != null)
            progressText.text = "Keputusan: " + step + "/" + maxStep;
    }

    void NextStep()
    {
        step++;
        UpdateProgress();

        if (step >= maxStep)
            FinishPuzzle();
    }

    void ChooseGiveUlos()
    {
        if (usedOption1 || finished) return;

        usedOption1 = true;
        option1Button.interactable = false;

        hulaHulaA.AddEmotion(2);
        hulaHulaB.AddEmotion(1);
        donganA.AddEmotion(-1);

        feedbackText.text = "Hula-hula merasa dihormati, tetapi Dongan Tubu mulai merasa kurang didengar.";

        NextStep();
    }

    void ChooseListenDongan()
    {
        if (usedOption2 || finished) return;

        usedOption2 = true;
        option2Button.interactable = false;

        donganA.AddEmotion(2);
        donganB.AddEmotion(1);

        feedbackText.text = "Dongan Tubu merasa didengar. Suasana mulai lebih tenang.";

        NextStep();
    }

    void ChooseAskRaja()
    {
        if (usedOption3 || finished) return;

        usedOption3 = true;
        option3Button.interactable = false;

        rajaParhata.AddEmotion(2);
        hulaHulaA.AddEmotion(1);
        hulaHulaB.AddEmotion(1);
        donganA.AddEmotion(1);
        donganB.AddEmotion(1);
        anakBoru.AddEmotion(1);

        feedbackText.text = "Nasihat Raja Parhata membantu menenangkan suasana.";

        NextStep();
    }

    void ChooseMusyawarah()
    {
        if (usedOption4 || finished) return;

        usedOption4 = true;
        option4Button.interactable = false;

        rajaParhata.AddEmotion(1);
        hulaHulaA.AddEmotion(1);
        hulaHulaB.AddEmotion(1);
        donganA.AddEmotion(1);
        donganB.AddEmotion(1);
        anakBoru.AddEmotion(1);

        feedbackText.text = "Musyawarah membuat semua pihak merasa dilibatkan.";

        NextStep();
    }

    void ChooseStartTooFast()
    {
        if (usedOption5 || finished) return;

        usedOption5 = true;
        option5Button.interactable = false;

        hulaHulaA.AddEmotion(1);
        hulaHulaB.AddEmotion(1);
        rajaParhata.AddEmotion(-2);
        donganA.AddEmotion(-1);
        anakBoru.AddEmotion(-1);

        feedbackText.text = "Acara dimulai terlalu cepat. Beberapa pihak merasa diabaikan.";

        NextStep();
    }

    void ChooseBoruHelp()
    {
        if (usedOption6 || finished) return;

        usedOption6 = true;
        option6Button.interactable = false;

        anakBoru.AddEmotion(2);
        rajaParhata.AddEmotion(1);

        feedbackText.text = "Anak Boru merasa dihargai karena diberi peran membantu persiapan.";

        NextStep();
    }

    void FinishPuzzle()
    {
        finished = true;

        option1Button.interactable = false;
        option2Button.interactable = false;
        option3Button.interactable = false;
        option4Button.interactable = false;
        option5Button.interactable = false;
        option6Button.interactable = false;

        int total =
            rajaParhata.emotionValue +
            hulaHulaA.emotionValue +
            hulaHulaB.emotionValue +
            donganA.emotionValue +
            donganB.emotionValue +
            anakBoru.emotionValue;

        if (resultPanel != null)
            resultPanel.SetActive(true);

        if (total >= successScore)
        {
            resultText.text = "Prosesi berjalan harmonis.\nKamu berhasil menjaga keseimbangan sosial.";
        }
        else
        {
            resultText.text = "Keseimbangan belum tercapai.\nBeberapa pihak masih merasa diabaikan.";
        }

        Invoke(nameof(CompletePuzzle), 2f);
    }

    void CompletePuzzle()
    {
        onPuzzleComplete.Invoke();
    }
}