using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidingPuzzleManager : MonoBehaviour
{
    [Header("Puzzle")]
    public List<Tile> tiles;
    public int gridSize = 3;

    [Header("Tingkat Kesulitan")]
    public int jumlahLangkahAcak = 8;

    [Header("Objek Puzzle")]
    public GameObject papanPuzzle;
    public GameObject fullImage;

    [Header("Timer")]
    public float waktuMain = 60f;
    public Text teksWaktu;

    [Header("Finish Dialog")]
    public GameObject finishDialogPanel;
    public Text finishDialogText;

    [TextArea(2, 4)]
    public string teksBerhasil = "Bagus. Sekarang ulos sudah tersusun dengan rapi.";

    [TextArea(2, 4)]
    public string teksWaktuHabis = "Ulos belum tersusun sempurna, tetapi proses tetap dilanjutkan.";

    [Header("Unlock Index")]
    public IndexManager indexManager;
    public int unlockGroupIndex = 1;

    [Header("Panel Flow")]
    public FlowPanel panelFlow;
    public int nextStepIndex = 2;
    public float delayLanjut = 2f;

    int emptyIndex;
    float timer;
    bool puzzleBerakhir = false;

    void OnEnable()
    {
        Setup();
    }

    void Update()
    {
        if (puzzleBerakhir) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            UpdateTimerText();
            WaktuHabis();
            return;
        }

        UpdateTimerText();
    }

    void Setup()
    {
        CancelInvoke();

        puzzleBerakhir = false;
        timer = waktuMain;

        if (papanPuzzle != null)
            papanPuzzle.SetActive(true);

        if (fullImage != null)
            fullImage.SetActive(false);

        if (finishDialogPanel != null)
            finishDialogPanel.SetActive(false);

        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].currentIndex = i;

            if (tiles[i].name == "Empty")
                emptyIndex = i;
        }

        Shuffle();
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        if (teksWaktu != null)
            teksWaktu.text = Mathf.CeilToInt(timer).ToString();
    }

    void Shuffle()
    {
        for (int i = 0; i < jumlahLangkahAcak; i++)
        {
            List<int> adjacentTiles = new List<int>();

            for (int j = 0; j < tiles.Count; j++)
            {
                if (IsAdjacent(j, emptyIndex))
                    adjacentTiles.Add(j);
            }

            if (adjacentTiles.Count == 0) return;

            int rand = adjacentTiles[Random.Range(0, adjacentTiles.Count)];
            SwapTiles(rand, emptyIndex);
        }
    }

    public void TryMove(Tile tile)
    {
        if (puzzleBerakhir) return;

        int index = tile.currentIndex;

        if (IsAdjacent(index, emptyIndex))
        {
            SwapTiles(index, emptyIndex);

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayGeser();

            CheckWin();
        }
    }

    bool IsAdjacent(int a, int b)
    {
        int rowA = a / gridSize;
        int colA = a % gridSize;

        int rowB = b / gridSize;
        int colB = b % gridSize;

        return Mathf.Abs(rowA - rowB) + Mathf.Abs(colA - colB) == 1;
    }

    void SwapTiles(int a, int b)
    {
        Tile temp = tiles[a];
        tiles[a] = tiles[b];
        tiles[b] = temp;

        int tempIndex = tiles[a].currentIndex;
        tiles[a].currentIndex = tiles[b].currentIndex;
        tiles[b].currentIndex = tempIndex;

        tiles[a].transform.SetSiblingIndex(a);
        tiles[b].transform.SetSiblingIndex(b);

        if (tiles[a].name == "Empty")
            emptyIndex = a;

        if (tiles[b].name == "Empty")
            emptyIndex = b;
    }

    void CheckWin()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].correctIndex != i)
                return;
        }

        PuzzleSelesai();
    }

    void PuzzleSelesai()
    {
        if (puzzleBerakhir) return;

        puzzleBerakhir = true;

        UnlockIndexKhusus();

        if (papanPuzzle != null)
            papanPuzzle.SetActive(false);

        if (fullImage != null)
            fullImage.SetActive(true);

        if (finishDialogPanel != null)
            finishDialogPanel.SetActive(true);

        if (finishDialogText != null)
            finishDialogText.text = teksBerhasil;

        Invoke(nameof(LoadNextScene), delayLanjut);
    }

    void WaktuHabis()
    {
        if (puzzleBerakhir) return;

        puzzleBerakhir = true;

        UnlockIndexKhusus();

        if (papanPuzzle != null)
            papanPuzzle.SetActive(false);

        if (fullImage != null)
            fullImage.SetActive(false);

        if (finishDialogPanel != null)
            finishDialogPanel.SetActive(true);

        if (finishDialogText != null)
            finishDialogText.text = teksWaktuHabis;

        Invoke(nameof(LoadNextScene), delayLanjut);
    }

    void UnlockIndexKhusus()
    {
        if (indexManager != null)
            indexManager.UnlockGroup(unlockGroupIndex);
    }

    void LoadNextScene()
    {
        if (panelFlow != null)
            panelFlow.ShowStep(nextStepIndex);
    }
}