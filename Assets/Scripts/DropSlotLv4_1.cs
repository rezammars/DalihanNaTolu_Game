using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropSlotLv4_1 : MonoBehaviour, IDropHandler
{
    [Header("Jawaban Benar")]
    public string itemID;

    [Header("Visual Slot")]
    public Image gambarSlot;
    public Sprite spriteAwal;

    [Header("Rotasi Default Slot")]
    public float rotasiDefaultSlot = 0f;

    [Header("Emoticon")]
    public Image emoticonImage;
    public Sprite emoticonSenang;
    public Sprite emoticonMarah;

    [Header("Settings")]
    public bool lockItemOnDrop = true;

    [Header("Puzzle Manager")]
    public DragDropPuzzleLv4 puzzleManager;

    [HideInInspector] public bool isCorrect = false;
    [HideInInspector] public bool isWrong = false;

    DragItemLv4 currentItem = null;

    RectTransform slotRect;
    Vector2 slotSize;
    Vector2 slotPosition;

    void Awake()
    {
        if (gambarSlot == null)
            gambarSlot = GetComponent<Image>();

        if (emoticonImage != null)
            emoticonImage.gameObject.SetActive(false);

        slotRect = gambarSlot.GetComponent<RectTransform>();
        if (slotRect != null)
        {
            slotSize = slotRect.rect.size;
            slotPosition = slotRect.anchoredPosition;
        }
    }

    void Start()
    {
        SetSpriteAwal();
        HideEmoticon();
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
            SetSpriteItemDuduk(item);
            ShowEmoticon(true);
        }
        else
        {
            isWrong = true;
            SetSpriteItemDuduk(item);
            ShowEmoticon(false);

            if (puzzleManager != null)
                puzzleManager.ShowWrongFeedback();
        }

        if (puzzleManager != null)
            puzzleManager.CheckAllSlotsFilled();
    }

    void SetSpriteItemDuduk(DragItemLv4 item)
    {
        if (gambarSlot == null || slotRect == null) return;

        if (item.gambarDuduk != null)
        {
            // SET SPRITE
            gambarSlot.sprite = item.gambarDuduk;
            gambarSlot.color = Color.white;

            // ====== CARA PALING MUDAH ======

            // 1. SET TYPE SIMPLE
            gambarSlot.type = Image.Type.Simple;

            // 2. AKTIFKAN PRESERVE ASPECT
            gambarSlot.preserveAspect = true;

            // 3. SET UKURAN KE UKURAN SPRITE ASLI
            slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, item.gambarDuduk.rect.width);
            slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, item.gambarDuduk.rect.height);

            // ATAU PAKAI INI (ALTERNATIF):
            // gambarSlot.SetNativeSize();

            // 4. RESET SKALA
            slotRect.localScale = Vector3.one;

            // 5. POSISI DI TENGAH SLOT
            slotRect.anchoredPosition = slotPosition;

            // 6. HITUNG ROTASI
            float rotasiAkhir = item.rotasiDefault - rotasiDefaultSlot;
            rotasiAkhir = NormalizeAngle(rotasiAkhir);
            slotRect.localRotation = Quaternion.Euler(0, 0, rotasiAkhir);

            // 7. PIVOT CENTER
            slotRect.pivot = new Vector2(0.5f, 0.5f);

            // 8. ANCHOR CENTER (AGAR TIDAK STRETCH)
            slotRect.anchorMin = new Vector2(0.5f, 0.5f);
            slotRect.anchorMax = new Vector2(0.5f, 0.5f);

            // 9. FORCE UPDATE
            LayoutRebuilder.ForceRebuildLayoutImmediate(slotRect);
        }
        else
        {
            Image itemImage = item.GetComponent<Image>();
            if (itemImage != null && itemImage.sprite != null)
            {
                gambarSlot.sprite = itemImage.sprite;
                gambarSlot.color = itemImage.color;
                gambarSlot.type = Image.Type.Simple;
                gambarSlot.preserveAspect = true;

                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, itemImage.sprite.rect.width);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, itemImage.sprite.rect.height);

                slotRect.localScale = Vector3.one;
                slotRect.anchoredPosition = slotPosition;

                float rotasiAkhir = item.rotasiDefault - rotasiDefaultSlot;
                rotasiAkhir = NormalizeAngle(rotasiAkhir);
                slotRect.localRotation = Quaternion.Euler(0, 0, rotasiAkhir);

                slotRect.pivot = new Vector2(0.5f, 0.5f);
                slotRect.anchorMin = new Vector2(0.5f, 0.5f);
                slotRect.anchorMax = new Vector2(0.5f, 0.5f);

                LayoutRebuilder.ForceRebuildLayoutImmediate(slotRect);
            }
            else
            {
                SetSpriteAwal();
            }
        }
    }

    float NormalizeAngle(float angle)
    {
        angle = angle % 360f;
        if (angle < 0) angle += 360f;
        return angle;
    }

    void SetSpriteAwal()
    {
        if (gambarSlot != null && spriteAwal != null)
        {
            gambarSlot.sprite = spriteAwal;
            gambarSlot.color = Color.white;
            gambarSlot.type = Image.Type.Simple;
            gambarSlot.preserveAspect = true;

            if (slotRect != null)
            {
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize.x);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize.y);
                slotRect.anchoredPosition = slotPosition;
                slotRect.localRotation = Quaternion.identity;
                slotRect.localScale = Vector3.one;
                slotRect.pivot = new Vector2(0.5f, 0.5f);
                slotRect.anchorMin = new Vector2(0.5f, 0.5f);
                slotRect.anchorMax = new Vector2(0.5f, 0.5f);

                LayoutRebuilder.ForceRebuildLayoutImmediate(slotRect);
            }
        }
    }

    void ShowEmoticon(bool senang)
    {
        if (emoticonImage == null) return;

        emoticonImage.gameObject.SetActive(true);

        if (senang && emoticonSenang != null)
        {
            emoticonImage.sprite = emoticonSenang;
        }
        else if (!senang && emoticonMarah != null)
        {
            emoticonImage.sprite = emoticonMarah;
        }
    }

    void HideEmoticon()
    {
        if (emoticonImage != null)
            emoticonImage.gameObject.SetActive(false);
    }

    public void ResetSlot()
    {
        isCorrect = false;
        isWrong = false;
        SetSpriteAwal();
        HideEmoticon();

        if (currentItem != null)
        {
            currentItem.ResetPosition();
            currentItem.gameObject.SetActive(true);
            currentItem.SetLocked(false);
            currentItem = null;
        }
    }
}