using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    [Header("Jawaban Benar")]
    public string itemID;

    [Header("Visual Slot")]
    public Image gambarSlot;
    public Sprite spriteAwal;
    public Sprite spriteBenar;

    [HideInInspector]
    public bool isCorrect = false;

    public DragDropPuzzle puzzleManager;

    void Awake()
    {
        if (gambarSlot == null)
            gambarSlot = GetComponent<Image>();
    }

    void Start()
    {
        SetSpriteAwal();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (isCorrect) return;

        DragItem item = eventData.pointerDrag.GetComponent<DragItem>();
        if (item == null) return;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayDrop();

        bool benar = item.itemNama == itemID;

        if (benar)
        {
            isCorrect = true;
            SetSpriteBenar();
            item.gameObject.SetActive(false);

            if (puzzleManager != null)
                puzzleManager.CheckPuzzle();
        }
        else
        {
            if (puzzleManager != null)
                puzzleManager.ShowWrongFeedback();
        }
    }

    void SetSpriteAwal()
    {
        if (gambarSlot != null && spriteAwal != null)
            gambarSlot.sprite = spriteAwal;
    }

    void SetSpriteBenar()
    {
        if (gambarSlot != null && spriteBenar != null)
            gambarSlot.sprite = spriteBenar;
    }

    public void ResetSlot()
    {
        isCorrect = false;
        SetSpriteAwal();
    }
}