using UnityEngine;

public class RhythmNote : MonoBehaviour
{
    public int laneIndex;
    public float speed;

    RectTransform rectTransform;
    RhythmManager rhythmManager;

    public void Init(int lane, float noteSpeed, RhythmManager manager)
    {
        laneIndex = lane;
        speed = noteSpeed;
        rhythmManager = manager;
        rectTransform = GetComponent<RectTransform>();
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (rhythmManager == null) return;
        if (!rhythmManager.IsPlaying) return;

        rectTransform.anchoredPosition += Vector2.left * speed * Time.deltaTime;

        if (rectTransform.anchoredPosition.x < -900f)
            rhythmManager.MissNote(this);
    }

    public RectTransform GetRect()
    {
        return rectTransform;
    }
}