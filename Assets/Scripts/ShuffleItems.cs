using UnityEngine;
using System.Collections.Generic;

public class ShuffleItems : MonoBehaviour
{
    public RectTransform[] items;
    public RectTransform[] startPositions;

    void Start()
    {
        Invoke(nameof(Shuffle), 0.1f);
    }

    void Shuffle()
    {
        List<Vector2> positions = new List<Vector2>();

        foreach (RectTransform pos in startPositions)
        {
            positions.Add(pos.anchoredPosition);
        }

        foreach (RectTransform item in items)
        {
            int randomIndex = Random.Range(0, positions.Count);

            item.anchoredPosition = positions[randomIndex];
            
            DragItem dragItem = item.GetComponent<DragItem>();
            if (dragItem != null)
            {
                dragItem.SaveStartPosition();
            }

            positions.RemoveAt(randomIndex);
        }
    }
}