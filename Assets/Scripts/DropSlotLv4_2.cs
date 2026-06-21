using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropSlotLv4_2 : MonoBehaviour, IDropHandler
{
    [Header("Jawaban Benar")]
    public string itemID;

    [Header("Visual Slot")]
    public Image gambarSlot;
    public Sprite spriteAwal;
    public Sprite spriteBenar;

    [Header("Settings")]
    public bool lockItemOnDrop = true;

    [Header("Puzzle Manager")]
    public DragDropPuzzleLv4 puzzleManager;

    [HideInInspector] public bool isCorrect = false;
    [HideInInspector] public bool isWrong = false;

    DragItemLv4 currentItem = null;

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
        if (isCorrect || isWrong) return;

        DragItemLv4 item = eventData.pointerDrag.GetComponent<DragItemLv4>();
        if (item == null) return;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayDrop();

        currentItem = item;
        bool benar = item.itemNama == itemID;

        item.gameObject.SetActive(false);

        if (benar)
        {
            isCorrect = true;
            SetSpriteBenar();
        }
        else
        {
            isWrong = true;
            SetSpriteItem(item);
            if (puzzleManager != null)
                puzzleManager.ShowWrongFeedback();
        }

    // PASTIKAN INI DIPANGGIL
        if (puzzleManager != null)
        {
            Debug.Log("Memanggil CheckAllSlotsFilled dari Puzzle2");
            puzzleManager.CheckAllSlotsFilled();
        }
    }

    void SetSpriteItem(DragItemLv4 item)
    {
        if (gambarSlot == null) return;

        Image itemImage = item.GetComponent<Image>();
        if (itemImage != null && itemImage.sprite != null)
        {
            gambarSlot.sprite = itemImage.sprite;
            gambarSlot.color = itemImage.color;
        }
        else
        {
            Image childImage = item.GetComponentInChildren<Image>();
            if (childImage != null && childImage.sprite != null)
            {
                gambarSlot.sprite = childImage.sprite;
                gambarSlot.color = childImage.color;
            }
            else
            {
                SetSpriteAwal();
            }
        }
    }

    void SetSpriteAwal()
    {
        if (gambarSlot != null && spriteAwal != null)
        {
            gambarSlot.sprite = spriteAwal;
            gambarSlot.color = Color.white;
        }
    }

    void SetSpriteBenar()
    {
        if (gambarSlot != null && spriteBenar != null)
        {
            gambarSlot.sprite = spriteBenar;
            gambarSlot.color = Color.white;
        }
    }

    public void ResetSlot()
    {
        isCorrect = false;
        isWrong = false;
        SetSpriteAwal();

        if (currentItem != null)
        {
            currentItem.ResetPosition();
            currentItem.SetLocked(false);
            currentItem = null;
        }
    }
}