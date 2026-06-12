using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MiniGameSimbol : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        [TextArea(2, 5)]
        public string conflictText;

        [TextArea(2, 5)]
        public string hintText;

        public string option1;
        public string option2;
        public string option3;
        public string option4;

        public int correctAnswerIndex;
    }

    [Header("Questions")]
    public Question[] questions;

    [Header("UI")]
    public Text conflictText;
    public Text hintText;
    public Text feedbackText;
    public Text progressText;

    [Header("Buttons")]
    public Button optionButton1;
    public Button optionButton2;
    public Button optionButton3;
    public Button optionButton4;

    [Header("Button Text")]
    public Text optionText1;
    public Text optionText2;
    public Text optionText3;
    public Text optionText4;

    [Header("Result")]
    public GameObject resultPanel;
    public Text resultText;

    [Header("After Finished")]
    public UnityEvent onQuizFinished;

    int currentQuestion = 0;
    int score = 0;
    bool answered = false;

    void OnEnable()
    {
        ResetQuiz();
    }

    void ResetQuiz()
    {
        currentQuestion = 0;
        score = 0;
        answered = false;

        if (resultPanel != null)
            resultPanel.SetActive(false);

        SetupButtons();
        ShowQuestion();
    }

    void SetupButtons()
    {
        optionButton1.onClick.RemoveAllListeners();
        optionButton2.onClick.RemoveAllListeners();
        optionButton3.onClick.RemoveAllListeners();
        optionButton4.onClick.RemoveAllListeners();

        optionButton1.onClick.AddListener(() => ChooseAnswer(0));
        optionButton2.onClick.AddListener(() => ChooseAnswer(1));
        optionButton3.onClick.AddListener(() => ChooseAnswer(2));
        optionButton4.onClick.AddListener(() => ChooseAnswer(3));
    }

    void ShowQuestion()
    {
        if (questions == null || questions.Length == 0)
        {
            Debug.LogWarning("Questions belum diisi.");
            return;
        }

        answered = false;

        Question q = questions[currentQuestion];

        conflictText.text = q.conflictText;
        hintText.text = q.hintText;

        optionText1.text = q.option1;
        optionText2.text = q.option2;
        optionText3.text = q.option3;
        optionText4.text = q.option4;

        progressText.text = "Soal " + (currentQuestion + 1) + "/" + questions.Length;
        feedbackText.text = "";

        SetButtonsInteractable(true);
    }

    void ChooseAnswer(int answerIndex)
    {
        if (answered) return;

        answered = true;
        SetButtonsInteractable(false);

        Question q = questions[currentQuestion];

        if (answerIndex == q.correctAnswerIndex)
        {
            score++;

            feedbackText.text = "Suasana mulai kembali tenang.";
        }
        else
        {
            feedbackText.text = "Beberapa orang masih merasa kecewa.";
        }

        Invoke(nameof(NextQuestion), 1.5f);
    }

    void NextQuestion()
    {
        currentQuestion++;

        if (currentQuestion >= questions.Length)
        {
            FinishQuiz();
        }
        else
        {
            ShowQuestion();
        }
    }

    void FinishQuiz()
    {
        if (resultPanel != null)
            resultPanel.SetActive(true);

        if (score >= 2)
        {
            resultText.text = "Kamu berhasil memahami simbol adat.";
        }
        else
        {
            resultText.text = "Masih ada simbol adat yang perlu dipelajari.";
        }

        Invoke(nameof(CallFinishEvent), 2f);
    }

    void CallFinishEvent()
    {
        onQuizFinished.Invoke();
    }

    void SetButtonsInteractable(bool value)
    {
        optionButton1.interactable = value;
        optionButton2.interactable = value;
        optionButton3.interactable = value;
        optionButton4.interactable = value;
    }
}