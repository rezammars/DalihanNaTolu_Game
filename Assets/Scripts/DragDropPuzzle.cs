using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DragDropPuzzle : MonoBehaviour
{
    [Header("Slots")]
    public DropSlot[] slots;

    [Header("UI")]
    public Text feedbackText;
    public Text hintText;

    [Header("Text")]
    public string startFeedback = "Seret item ke slot yang benar.";
    public string correctFeedback = "Semua sudah tersusun dengan benar.";
    public string wrongFeedback1 = "Ada yang terasa kurang tepat.";
    public string wrongFeedback2 = "Coba perhatikan urutannya kembali.";

    [Header("After Complete")]
    public UnityEvent onPuzzleComplete;

    int wrongCount = 0;
    bool finished = false;

    void OnEnable()
    {
        wrongCount = 0;
        finished = false;

        if (feedbackText != null)
            feedbackText.text = startFeedback;
    }

    public void CheckPuzzle()
    {
        if (finished) return;

        foreach (DropSlot slot in slots)
        {
            if (slot == null) continue;

            if (!slot.isCorrect)
                return;
        }

        finished = true;

        if (feedbackText != null)
            feedbackText.text = correctFeedback;

        Invoke(nameof(CompletePuzzle), 1.2f);
    }

    public void ShowWrongFeedback()
    {
        if (finished) return;

        wrongCount++;

        if (feedbackText != null)
        {
            if (wrongCount <= 1)
                feedbackText.text = wrongFeedback1;
            else
                feedbackText.text = wrongFeedback2;
        }

        if (hintText != null && wrongCount >= 2)
        {
            hintText.text = "Perhatikan kembali hubungan antara item dan slot.";
        }
    }

    void CompletePuzzle()
    {
        onPuzzleComplete.Invoke();
    }
}