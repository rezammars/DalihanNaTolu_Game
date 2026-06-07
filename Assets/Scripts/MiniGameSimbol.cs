using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SymbolGameManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string conflict;
        public string hint;

        public string option1;
        public string option2;
        public string option3;
        public string option4;

        public int correctAnswer;
    }

    public Question[] questions;

    public Text conflictText;
    public Text hintText;

    public Text progressText;
    public Text feedbackText;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    public Text button1Text;
    public Text button2Text;
    public Text button3Text;
    public Text button4Text;

    public GameObject resultPanel;
    public Text resultText;

    public string nextSceneName;

    int currentQuestion = 0;
    int score = 0;

    void Start()
    {
        ShowQuestion();
    }

    void ShowQuestion()
    {
        Question q = questions[currentQuestion];

        conflictText.text = q.conflict;
        hintText.text = q.hint;

        button1Text.text = q.option1;
        button2Text.text = q.option2;
        button3Text.text = q.option3;
        button4Text.text = q.option4;

        progressText.text =
            "Soal " +
            (currentQuestion + 1) +
            "/" +
            questions.Length;

        feedbackText.text = "";
    }

    public void ChooseAnswer(int answer)
    {
        Question q = questions[currentQuestion];

        if(answer == q.correctAnswer)
        {
            score++;

            feedbackText.text = "Suasana mulai kembali tenang.";
        }
        else
        {
            feedbackText.text = "Beberapa orang masih merasa kecewa.";
        }

        Invoke(nameof(NextQuestion),1.5f);
    }

    void NextQuestion()
    {
        currentQuestion++;

        if(currentQuestion >= questions.Length)
        {
            FinishGame();
        }
        else
        {
            ShowQuestion();
        }
    }

    void FinishGame()
    {
        resultPanel.SetActive(true);

        if(score >= 2)
        {
            resultText.text = "Kamu berhasil memahami simbol adat.";
        }
        else
        {
            resultText.text = "Masih ada simbol adat yang perlu dipelajari.";
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}