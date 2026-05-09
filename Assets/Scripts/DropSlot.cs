using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public string slotName;

    public void OnDrop(PointerEventData eventData)
    {
        DragItem item = eventData.pointerDrag.GetComponent<DragItem>();

        if (item.correctSlot == slotName)
        {
            item.transform.position = transform.position;
            Debug.Log("Benar");
        }
        else
        {
            item.ResetPosition();
            Debug.Log("Salah");
        }
    }
}
