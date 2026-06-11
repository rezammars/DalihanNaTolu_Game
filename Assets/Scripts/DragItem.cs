using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string itemId;

    RectTransform rectTransform;
    CanvasGroup canvasGroup;

    Transform startParent;
    Vector2 startPosition;

    DropSlot currentSlot;
    bool locked = false;

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
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (locked) return;

        canvasGroup.blocksRaycasts = false;

        if (currentSlot != null)
        {
            currentSlot.ClearSlot();
            currentSlot = null;
        }

        transform.SetParent(startParent);
        transform.SetAsLastSibling();
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

    public void SnapToSlot(DropSlot slot)
    {
        currentSlot = slot;

        transform.SetParent(slot.transform);
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void SetLocked(bool value)
    {
        locked = value;
        canvasGroup.blocksRaycasts = !value;
    }

    public void ResetPosition()
    {
        transform.SetParent(startParent);
        rectTransform.anchoredPosition = startPosition;
        currentSlot = null;
        locked = false;
        canvasGroup.blocksRaycasts = true;
    }
}