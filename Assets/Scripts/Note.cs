using UnityEngine;

public class Note : MonoBehaviour
{
    public int laneIndex;
    public float speed = 300f;

    private RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        rect.anchoredPosition += Vector2.left * speed * Time.deltaTime;

        if (rect.anchoredPosition.x < -800f)
        {
            RhythmManager.instance.MissNote(this);
        }
    }

    public RectTransform GetRect()
    {
        return rect;
    }
}
