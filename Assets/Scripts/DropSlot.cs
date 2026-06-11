using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public string correctItemId;
    public DragItem currentItem;
    public bool isCorrect = false;

    public DragDropPuzzle puzzleManager;

    public void OnDrop(PointerEventData eventData)
    {
        if (isCorrect) return;

        DragItem item = eventData.pointerDrag.GetComponent<DragItem>();

        if (item == null) return;

        currentItem = item;
        item.SnapToSlot(this);

        if (item.itemId == correctItemId)
        {
            isCorrect = true;
            item.SetLocked(true);

            if (puzzleManager != null)
                puzzleManager.CheckPuzzle();
        }
        else
        {
            if (puzzleManager != null)
                puzzleManager.ShowWrongFeedback();
        }
    }

    public void ClearSlot()
    {
        currentItem = null;
        isCorrect = false;
    }
}