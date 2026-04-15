using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<Tile> tiles;
    public int gridSize = 3;
    public GameObject papanPuzzle;
    public GameObject fullImage;

    private int emptyIndex;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    void Setup()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].currentIndex = i;

            if (tiles[i].name == "Empty")
            {
                emptyIndex = i;
            }
        }
        Shuffle();
    }

    void Shuffle()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            int rand = Random.Range(0, tiles.Count);
            SwapTiles(i, rand);
        }
    }

    public void TryMove(Tile tile)
    {
        int index = tile.currentIndex;

        if (IsAdjacent(index, emptyIndex))
        {
            SwapTiles(index, emptyIndex);
            CheckWin();
        }
    }

    bool IsAdjacent(int a, int b)
    {
        int rowA = a / gridSize;
        int colA = a % gridSize;

        int rowB = b / gridSize;
        int colB = b % gridSize;

        return (Mathf.Abs(rowA - rowB) + Mathf.Abs(colA - colB)) == 1;
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

        if (tiles[a].name == "Empty") emptyIndex = a;
        if (tiles[b].name == "Empty") emptyIndex = b;
    }

    void CheckWin()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].correctIndex != i)
            {
                return;
            }
        }
        PuzzleSelesai();
    }

    void PuzzleSelesai()
    {
        papanPuzzle.SetActive(false);
        fullImage.SetActive(true);
    }
}
