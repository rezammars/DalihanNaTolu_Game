using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int correctIndex;
    public int currentIndex;

    private PuzzleManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<PuzzleManager>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        manager.TryMove(this);
    }
}