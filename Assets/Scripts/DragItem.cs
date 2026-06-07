using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string roleName;

    RectTransform rect;
    CanvasGroup canvasGroup;

    public DropSlot currentSlot;
    
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
        if (currentSlot != null)
        {
            currentSlot.currentItem = null;
            currentSlot = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void SnapToSlot(DropSlot slot)
    {
        currentSlot = slot;

        transform.SetParent(slot.transform);

        rect.anchoredPosition = Vector2.zero;
    }
}
