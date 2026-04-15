using UnityEngine;

public class Note : MonoBehaviour
{
    public float speed = 500f;
    public int laneIndex;

    RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rect.anchoredPosition += Vector2.down * speed * Time.deltaTime;
    }
}
