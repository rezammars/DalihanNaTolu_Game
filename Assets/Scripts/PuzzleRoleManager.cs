using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleRoleManager : MonoBehaviour
{
    public static PuzzleRoleManager instance;
    public string nextScene;

    public DropSlot[] slots;

    public Text hintText;
    public Text feedbackText;

    int wrongCount = 0;
    float idleTimer = 0f;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer >= 15f)
        {
            ShowHint();
            idleTimer = 0f;
        }
    }

    public void ShowCorrectPlacement()
    {
        idleTimer = 0f;

        feedbackText.text = "Sepertinya ini sudah sesuai.";
    }

    public void ShowWrongPlacement()
    {
        idleTimer = 0f;

        wrongCount++;

        if (wrongCount >= 2)
        {
            feedbackText.text = "Posisinya terasa tidak sesuai... suasana jadi kurang nyaman.";
        }
        else
        {
            feedbackText.text = "Hmm... sepertinya ada yang tidak tepat.";
        }

        ShowHint();
    }

    public void CheckAllCorrect()
    {
        foreach (DropSlot slot in slots)
        {
            if (!slot.IsCorrect())
            {
                return;
            }
        }

        feedbackText.text = "Semua terlihat lebih rapi...";

        Invoke(nameof(FinishPuzzle), 1.5f);
    }

    void ShowHint()
    {
        if (wrongCount == 0)
        {
            hintText.text = "Sepertinya posisi mereka punya aturan tertentu...";
        }
        else if (wrongCount == 1)
        {
            hintText.text = "Mungkin ada yang harus kutempatkan di posisi yang lebih dihormati.";
        }
        else
        {
            hintText.text = "Coba perhatikan siapa yang harus dihormati, siapa yang setara, dan siapa yang membantu.";
        }
    }

    void FinishPuzzle()
    {
        SceneManager.LoadScene(nextScene);
    }
}