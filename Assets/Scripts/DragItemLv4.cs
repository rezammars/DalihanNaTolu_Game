using UnityEngine;
using UnityEngine.EventSystems;

public class DragItemLv4 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Data Item")]
    public string itemNama;

    [Header("Gambar Duduk Default")]
    public Sprite gambarDuduk;

    [Header("Rotasi Default Gambar Duduk")]
    [Tooltip("Rotasi default gambar saat tidak di slot")]
    public float rotasiDefault = 0f;

    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    Transform startParent;
    Vector2 startPosition;
    Quaternion startRotation;
    bool locked;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    void Start()
    {
        startParent = transform.parent;
        startPosition = rectTransform.anchoredPosition;
        startRotation = rectTransform.localRotation;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (locked) return;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayDrag();

        canvasGroup.blocksRaycasts = false;
        transform.SetParent(startParent);
        transform.SetAsLastSibling();
        rectTransform.localRotation = Quaternion.identity;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (locked) return;
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (locked) return;
        canvasGroup.blocksRaycasts = true;
    }

    public void ResetPosition()
    {
        transform.SetParent(startParent);
        rectTransform.anchoredPosition = startPosition;
        rectTransform.localRotation = startRotation;
    }

    public void SetLocked(bool value)
    {
        locked = value;
        canvasGroup.blocksRaycasts = !value;
    }

    public bool IsLocked()
    {
        return locked;
    }
}