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
    public Sprite spriteSalah;

    [Header("Stage 4")]
    public bool lockWrongDrop = false;

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

        if (item == null)
            return;

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
            SetSpriteSalah();

            if (lockWrongDrop)
            {
                item.gameObject.SetActive(false);

                if (puzzleManager != null)
                    puzzleManager.CheckPuzzle();
            }
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

    void SetSpriteSalah()
    {
        if (gambarSlot != null && spriteSalah != null)
            gambarSlot.sprite = spriteSalah;
    }
}