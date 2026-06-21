using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DragDropPuzzleLv4 : MonoBehaviour
{
    [Header("Slots")]
    public GameObject[] slotObjects;

    [Header("UI")]
    public Text feedbackText;
    public Text hintText;

    [Header("Text")]
    public string startFeedback = "Seret kartu ke tempat yang benar.";
    public string correctFeedback = "Semua sudah tersusun dengan benar!";
    public string wrongFeedback1 = "Ada yang terasa kurang tepat.";
    public string wrongFeedback2 = "Coba perhatikan urutannya kembali.";

    [Header("After Complete")]
    public UnityEvent onPuzzleComplete;

    public Stage4Hasil stage4Hasil;
    public bool puzzlePeran;
    public bool puzzleUrutan;

    int wrongCount = 0;
    bool finished = false;
    bool allSlotsFilled = false;
    bool puzzleBerhasil = false;

    void OnEnable()
    {
        wrongCount = 0;
        finished = false;
        allSlotsFilled = false;

        if (feedbackText != null)
            feedbackText.text = startFeedback;

        if (hintText != null)
            hintText.text = "";
    }

    public void CheckAllSlotsFilled()
    {
        if (finished || allSlotsFilled) return;

        if (slotObjects == null || slotObjects.Length == 0)
            return;

        int totalSlots = 0;
        int filledSlots = 0;

        foreach (GameObject slotObj in slotObjects)
        {
            if (slotObj == null) continue;

            DropSlotLv4_1 slot1 = slotObj.GetComponent<DropSlotLv4_1>();
            if (slot1 != null)
            {
                totalSlots++;
                if (slot1.isCorrect || slot1.isWrong)
                    filledSlots++;
                continue;
            }

            DropSlotLv4_2 slot2 = slotObj.GetComponent<DropSlotLv4_2>();
            if (slot2 != null)
            {
                totalSlots++;
                if (slot2.isCorrect || slot2.isWrong)
                    filledSlots++;
                continue;
            }
        }

        if (totalSlots > 0 && filledSlots >= totalSlots)
        {
            allSlotsFilled = true;

            puzzleBerhasil = CheckAllCorrect();

            Invoke(nameof(CompletePuzzle), 1.2f);
        }
    }

    bool CheckAllCorrect()
    {
        foreach (GameObject slotObj in slotObjects)
        {
            if (slotObj == null) continue;

            DropSlotLv4_1 slot1 = slotObj.GetComponent<DropSlotLv4_1>();
            if (slot1 != null && !slot1.isCorrect)
                return false;

            DropSlotLv4_2 slot2 = slotObj.GetComponent<DropSlotLv4_2>();
            if (slot2 != null && !slot2.isCorrect)
                return false;
        }
        return true;
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
        finished = true;

        if (stage4Hasil != null)
        {
            if (puzzlePeran)
                stage4Hasil.SetPuzzlePeranResult(puzzleBerhasil);
            if (puzzleUrutan)
                stage4Hasil.SetPuzzleUrutanResult(puzzleBerhasil);
        }

        onPuzzleComplete.Invoke();
    }

    public bool IsFinished()
    {
        return finished;
    }

    public void ResetPuzzle()
    {
        finished = false;
        allSlotsFilled = false;
        wrongCount = 0;

        if (feedbackText != null)
            feedbackText.text = startFeedback;

        if (hintText != null)
            hintText.text = "";

        foreach (GameObject slotObj in slotObjects)
        {
            if (slotObj == null) continue;

            DropSlotLv4_1 slot1 = slotObj.GetComponent<DropSlotLv4_1>();
            if (slot1 != null)
                slot1.ResetSlot();

            DropSlotLv4_2 slot2 = slotObj.GetComponent<DropSlotLv4_2>();
            if (slot2 != null)
                slot2.ResetSlot();
        }
    }

    public bool IsSuccess()
    {
        return puzzleBerhasil;
    }
}