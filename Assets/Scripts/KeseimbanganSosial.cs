using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SocialBalanceManager : MonoBehaviour
{
    [Header("NPC UI")]
    public NPCStatusUI rajaParhata;
    public NPCStatusUI hulaHulaA;
    public NPCStatusUI hulaHulaB;
    public NPCStatusUI donganA;
    public NPCStatusUI donganB;
    public NPCStatusUI anakBoru;

    [Header("Option Buttons")]
    public Button optionButton1;
    public Button optionButton2;
    public Button optionButton3;
    public Button optionButton4;
    public Button optionButton5;
    public Button optionButton6;

    [Header("Option Texts")]
    public Text optionText1;
    public Text optionText2;
    public Text optionText3;
    public Text optionText4;
    public Text optionText5;
    public Text optionText6;

    [Header("UI")]
    public Text feedbackText;
    public Text progressText;

    public GameObject resultPanel;
    public Text resultText;

    [Header("Scene")]
    public string nextSceneName;

    [Header("Rules")]
    public int maxStep = 3;
    public int successScore = 5;

    int step = 0;

    bool usedOption1 = false;
    bool usedOption2 = false;
    bool usedOption3 = false;
    bool usedOption4 = false;
    bool usedOption5 = false;
    bool usedOption6 = false;

    void Start()
    {
        resultPanel.SetActive(false);

        SetupNPC();
        SetupOptions();
        UpdateProgress();
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
        optionText1.text = "Berikan Ulos kepada Hula-hula";
        optionText2.text = "Dengarkan Keluhan Dongan Tubu";
        optionText3.text = "Minta Nasihat Raja Parhata";
        optionText4.text = "Adakan Musyawarah";
        optionText5.text = "Mulai Acara Terlalu Cepat";
        optionText6.text = "Tugaskan Anak Boru Membantu Persiapan";

        optionButton1.onClick.RemoveAllListeners();
        optionButton2.onClick.RemoveAllListeners();
        optionButton3.onClick.RemoveAllListeners();
        optionButton4.onClick.RemoveAllListeners();
        optionButton5.onClick.RemoveAllListeners();
        optionButton6.onClick.RemoveAllListeners();

        optionButton1.onClick.AddListener(ChooseBeriUlos);
        optionButton2.onClick.AddListener(ChooseDengarkanDongan);
        optionButton3.onClick.AddListener(ChooseNasehatRaja);
        optionButton4.onClick.AddListener(ChooseMusyawarah);
        optionButton5.onClick.AddListener(ChooseAcaraCepat);
        optionButton6.onClick.AddListener(ChooseTugaskanBoru);
    }

    void UpdateProgress()
    {
        progressText.text = "Keputusan: " + step + "/" + maxStep;
    }

    void NextStep()
    {
        step++;
        UpdateProgress();

        if (step >= maxStep)
        {
            FinishPuzzle();
        }
    }

    void DisableAllOptions()
    {
        optionButton1.interactable = false;
        optionButton2.interactable = false;
        optionButton3.interactable = false;
        optionButton4.interactable = false;
        optionButton5.interactable = false;
        optionButton6.interactable = false;
    }

    void ChooseBeriUlos()
    {
        if (usedOption1) return;

        usedOption1 = true;
        optionButton1.interactable = false;

        hulaHulaA.AddEmotion(2);
        hulaHulaB.AddEmotion(1);
        donganA.AddEmotion(-1);

        feedbackText.text = "Hula-hula merasa dihormati, tetapi Dongan Tubu mulai merasa kurang didengar.";

        NextStep();
    }

    void ChooseDengarkanDongan()
    {
        if (usedOption2) return;

        usedOption2 = true;
        optionButton2.interactable = false;

        donganA.AddEmotion(2);
        donganB.AddEmotion(1);

        feedbackText.text = "Dongan Tubu merasa didengar. Suasana mulai lebih tenang.";

        NextStep();
    }

    void ChooseNasehatRaja()
    {
        if (usedOption3) return;

        usedOption3 = true;
        optionButton3.interactable = false;

        rajaParhata.AddEmotion(2);
        hulaHulaA.AddEmotion(1);
        donganA.AddEmotion(1);
        anakBoru.AddEmotion(1);

        feedbackText.text = "Nasihat Raja Parhata membantu menenangkan suasana.";

        NextStep();
    }

    void ChooseMusyawarah()
    {
        if (usedOption4) return;

        usedOption4 = true;
        optionButton4.interactable = false;

        rajaParhata.AddEmotion(1);
        hulaHulaA.AddEmotion(1);
        hulaHulaB.AddEmotion(1);
        donganA.AddEmotion(1);
        donganB.AddEmotion(1);
        anakBoru.AddEmotion(1);

        feedbackText.text = "Musyawarah membuat semua pihak merasa dilibatkan.";

        NextStep();
    }

    void ChooseAcaraCepat()
    {
        if (usedOption5) return;

        usedOption5 = true;
        optionButton5.interactable = false;

        hulaHulaA.AddEmotion(1);
        hulaHulaB.AddEmotion(1);
        rajaParhata.AddEmotion(-2);
        donganA.AddEmotion(-1);
        anakBoru.AddEmotion(-1);

        feedbackText.text = "Acara dimulai terlalu cepat. Beberapa pihak merasa diabaikan.";

        NextStep();
    }

    void ChooseTugaskanBoru()
    {
        if (usedOption6) return;

        usedOption6 = true;
        optionButton6.interactable = false;

        anakBoru.AddEmotion(2);
        rajaParhata.AddEmotion(1);
        donganA.AddEmotion(1);

        feedbackText.text = "Anak Boru merasa dihargai karena diberi peran membantu persiapan.";

        NextStep();
    }

    void FinishPuzzle()
    {
        DisableAllOptions();

        int total = rajaParhata.emotionValue + hulaHulaA.emotionValue + hulaHulaB.emotionValue +
            donganA.emotionValue + donganB.emotionValue + anakBoru.emotionValue;

        resultPanel.SetActive(true);

        if (total >= successScore)
        {
            resultText.text = "Prosesi berjalan harmonis.\nKamu berhasil menjaga keseimbangan sosial.";
        }
        else
        {
            resultText.text = "Keseimbangan belum tercapai.\nBeberapa pihak masih merasa diabaikan.";
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}