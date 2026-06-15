using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Nama Item")]
    public string itemNama;

    RectTransform rectTransform;
    CanvasGroup canvasGroup;

    Transform startParent;
    Vector2 startPosition;

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
        ResetPosition();
    }

    public void ResetPosition()
    {
        transform.SetParent(startParent);
        rectTransform.anchoredPosition = startPosition;
    }

    public void SetLocked(bool value)
    {
        locked = value;
        canvasGroup.blocksRaycasts = !value;
    }
   
}