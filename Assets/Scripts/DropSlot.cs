using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public string slotName;
    public DragItem currentItem;

    public void OnDrop(PointerEventData eventData)
    {
        DragItem item = eventData.pointerDrag.GetComponent<DragItem>();

        if (item == null) return;

        currentItem = item;

        item.SnapToSlot(this);

        if (IsCorrect())
        {
            PuzzleRoleManager.instance.ShowCorrectPlacement();
        }
        else
        {
            PuzzleRoleManager.instance.ShowWrongPlacement();
        }

        PuzzleRoleManager.instance.CheckAllCorrect();
    }

    public bool IsCorrect()
    {
        if (currentItem == null) return false;

        return currentItem.roleName == slotName;
    }
}
