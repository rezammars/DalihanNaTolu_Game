using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    [Header("Nama Item yang Benar")]
    public string ItemID;

    [Header("Gambar Slot")]
    public Image gambarSlot;
    public Sprite spriteAwal;
    public Sprite spriteSenang;
    public Sprite spriteMarah;

    [Header("Status Slot")]
    public bool isCorrect = false;

    public DragDropPuzzle puzzleManager;

    void Awake()
    {
        if(gambarSlot == null)
            gambarSlot = GetComponent<Image>();
    }

    void Start()
    {
        ShowGambarAwal();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (isCorrect) return;

        DragItem item = eventData.pointerDrag.GetComponent<DragItem>();
        if (item == null) return;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayDrop();

        if (item.itemNama == ItemID)
        {
            isCorrect = true;
            ShowGambarSenang();
            item.gameObject.SetActive(false);

            if (puzzleManager != null)
                puzzleManager.CheckPuzzle();
        }
        else
        {
            ShowGambarMarah();
            if (puzzleManager != null)
                puzzleManager.ShowWrongFeedback();

            Invoke(nameof(ShowGambarAwal), 0.7f);
        }
    }

    public void ShowGambarAwal()
    {
        if (gambarSlot != null && spriteAwal != null)
            gambarSlot.sprite = spriteAwal;
    }

    public void ShowGambarSenang()
    {
        if (gambarSlot != null && spriteSenang != null)
            gambarSlot.sprite = spriteSenang;
    }

    public void ShowGambarMarah()
    {
        if (gambarSlot != null && spriteMarah != null)
            gambarSlot.sprite = spriteMarah;
    }
}