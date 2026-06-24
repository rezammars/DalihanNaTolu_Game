using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Stage4Hasil : MonoBehaviour
{
    [Header("Puzzle Status")]
    public bool puzzlePeranSelesai = false;
    public bool puzzlePeranBerhasil = false;

    public bool puzzleUrutanSelesai = false;
    public bool puzzleUrutanBerhasil = false;

    public bool puzzleKeseimbanganSelesai = false;
    public bool puzzleKeseimbanganBerhasil = false;

    [Header("Result UI")]
    public GameObject resultPanel;
    public Text judulTeks;
    public Text isiTeks;

    [Header("Unlock Index Stage 4")]
    public IndexManager indexManager;
    public int unlockGroupIndexStage4 = 6;

    [Header("After Result")]
    public float delayLanjut = 2f;
    public UnityEvent onStage4Finished;

    void Start()
    {
        if (resultPanel != null)
            resultPanel.SetActive(false);
    }

    public void SetPuzzlePeranResult(bool berhasil)
    {
        puzzlePeranSelesai = true;
        puzzlePeranBerhasil = berhasil;

        CheckAllPuzzleFinished();
    }

    public void SetPuzzleUrutanResult(bool berhasil)
    {
        puzzleUrutanSelesai = true;
        puzzleUrutanBerhasil = berhasil;

        CheckAllPuzzleFinished();
    }

    public void SetPuzzleKeseimbanganResult(bool berhasil)
    {
        puzzleKeseimbanganSelesai = true;
        puzzleKeseimbanganBerhasil = berhasil;

        CheckAllPuzzleFinished();
    }

    void CheckAllPuzzleFinished()
    {
        if (!puzzlePeranSelesai) return;
        if (!puzzleUrutanSelesai) return;
        if (!puzzleKeseimbanganSelesai) return;
    }

    public void ShowFinal()
    {
        TampilkanHasilAkhir();
    }
    void TampilkanHasilAkhir()
    {
        bool semuaBerhasil =
            puzzlePeranBerhasil &&
            puzzleUrutanBerhasil &&
            puzzleKeseimbanganBerhasil;

        if (resultPanel != null)
            resultPanel.SetActive(true);

        if (semuaBerhasil)
        {
            judulTeks.text = "KAMU BERHASIL";

            isiTeks.text = "Kamu sudah memahami Dalihan Na Tolu \n" + "INDEX UPDATE!";

            UnlockIndexStage4();
        }
        else
        {
            judulTeks.text = "KAMU GAGAL";

            isiTeks.text = "Perhatikan petunjuk yang kamu dapatkan di sekitar";
        }

        Invoke(nameof(LanjutSetelahHasil), delayLanjut);
    }

    void UnlockIndexStage4()
    {
        if (indexManager != null)
            indexManager.UnlockGroup(unlockGroupIndexStage4);
    }

    void LanjutSetelahHasil()
    {
        onStage4Finished.Invoke();
    }
}