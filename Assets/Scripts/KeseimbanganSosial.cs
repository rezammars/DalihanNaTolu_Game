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

    [Header("Buttons")]
    public Button optionButton1;
    public Button optionButton2;
    public Button optionButton3;

    public Text optionText1;
    public Text optionText2;
    public Text optionText3;

    [Header("UI")]
    public Text feedbackText;
    public Text progressText;

    public GameObject resultPanel;
    public Text resultText;

    [Header("Scene")]
    public string nextSceneName = "EndingLvl3";

    int step = 0;
    int maxStep = 3;

    void Start()
    {
        resultPanel.SetActive(false);

        SetupNPC();
        ShowStep();
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

    void ShowStep()
    {
        progressText.text = "Keputusan: " + step + "/" + maxStep;

        optionButton1.onClick.RemoveAllListeners();
        optionButton2.onClick.RemoveAllListeners();
        optionButton3.onClick.RemoveAllListeners();

        if (step == 0)
        {
            optionText1.text = "Berikan Ulos kepada Hula-hula";
            optionText2.text = "Dengarkan Keluhan Dongan Tubu";
            optionText3.text = "Mulai Acara Terlalu Cepat";

            optionButton1.onClick.AddListener(ChooseGiveUlos);
            optionButton2.onClick.AddListener(ChooseListenDongan);
            optionButton3.onClick.AddListener(ChooseStartTooFast);
        }
        else if (step == 1)
        {
            optionText1.text = "Minta Nasihat Raja Parhata";
            optionText2.text = "Adakan Musyawarah";
            optionText3.text = "Abaikan Anak Boru";

            optionButton1.onClick.AddListener(ChooseAskRaja);
            optionButton2.onClick.AddListener(ChooseMusyawarah);
            optionButton3.onClick.AddListener(ChooseIgnoreBoru);
        }
        else if (step == 2)
        {
            optionText1.text = "Tugaskan Anak Boru Membantu Persiapan";
            optionText2.text = "Dahulukan Hula-hula Lagi";
            optionText3.text = "Biarkan Semua Menunggu";

            optionButton1.onClick.AddListener(ChooseBoruHelp);
            optionButton2.onClick.AddListener(ChooseHulaAgain);
            optionButton3.onClick.AddListener(ChooseWait);
        }
    }

    void NextStep()
    {
        step++;

        if (step >= maxStep)
        {
            FinishPuzzle();
        }
        else
        {
            ShowStep();
        }
    }

    void ChooseGiveUlos()
    {
        hulaHulaA.AddEmotion(2);
        hulaHulaB.AddEmotion(1);
        donganA.AddEmotion(-1);

        feedbackText.text =
            "Hula-hula merasa dihormati, tetapi Dongan Tubu mulai merasa kurang didengar.";

        NextStep();
    }

    void ChooseListenDongan()
    {
        donganA.AddEmotion(2);
        donganB.AddEmotion(1);

        feedbackText.text =
            "Dongan Tubu merasa didengar. Suasana mulai lebih tenang.";

        NextStep();
    }

    void ChooseStartTooFast()
    {
        hulaHulaA.AddEmotion(1);
        hulaHulaB.AddEmotion(1);
        rajaParhata.AddEmotion(-2);
        donganA.AddEmotion(-1);

        feedbackText.text =
            "Acara dimulai terlalu cepat. Beberapa pihak merasa diabaikan.";

        NextStep();
    }

    void ChooseAskRaja()
    {
        rajaParhata.AddEmotion(2);
        hulaHulaA.AddEmotion(1);
        donganA.AddEmotion(1);

        feedbackText.text =
            "Nasihat Raja Parhata membantu menenangkan suasana.";

        NextStep();
    }

    void ChooseMusyawarah()
    {
        rajaParhata.AddEmotion(1);
        hulaHulaA.AddEmotion(1);
        hulaHulaB.AddEmotion(1);
        donganA.AddEmotion(1);
        donganB.AddEmotion(1);
        anakBoru.AddEmotion(1);

        feedbackText.text =
            "Musyawarah membuat semua pihak merasa dilibatkan.";

        NextStep();
    }

    void ChooseIgnoreBoru()
    {
        anakBoru.AddEmotion(-2);
        rajaParhata.AddEmotion(-1);

        feedbackText.text =
            "Anak Boru merasa bebannya tidak diperhatikan.";

        NextStep();
    }

    void ChooseBoruHelp()
    {
        anakBoru.AddEmotion(2);
        rajaParhata.AddEmotion(1);

        feedbackText.text =
            "Anak Boru merasa dihargai karena diberi peran membantu persiapan.";

        NextStep();
    }

    void ChooseHulaAgain()
    {
        hulaHulaA.AddEmotion(1);
        donganA.AddEmotion(-1);
        anakBoru.AddEmotion(-1);

        feedbackText.text =
            "Hula-hula senang, tetapi pihak lain mulai merasa tidak seimbang.";

        NextStep();
    }

    void ChooseWait()
    {
        hulaHulaA.AddEmotion(-1);
        donganA.AddEmotion(-1);
        anakBoru.AddEmotion(-1);

        feedbackText.text =
            "Semua menunggu terlalu lama. Suasana menjadi canggung.";

        NextStep();
    }

    void FinishPuzzle()
    {
        int total =
            rajaParhata.emotionValue +
            hulaHulaA.emotionValue +
            hulaHulaB.emotionValue +
            donganA.emotionValue +
            donganB.emotionValue +
            anakBoru.emotionValue;

        resultPanel.SetActive(true);

        if (total >= 5)
        {
            resultText.text =
                "Prosesi berjalan harmonis.\nKamu berhasil menjaga keseimbangan sosial.";
        }
        else
        {
            resultText.text =
                "Keseimbangan belum tercapai.\nBeberapa pihak masih merasa diabaikan.";
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