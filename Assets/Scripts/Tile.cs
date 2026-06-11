using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int correctIndex;
    public int currentIndex;

    private SlidingPuzzleManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<SlidingPuzzleManager>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        manager.TryMove(this);
    }
}