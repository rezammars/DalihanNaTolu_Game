using UnityEngine;

public class AturY : MonoBehaviour
{
    [Header("Renderer")]
    public SpriteRenderer spriteRenderer;

    [Header("Sorting")]
    public int baseOrder = 5000;
    public int multiplier = 100;
    public int sortingOffset = 0;

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (spriteRenderer == null) return;

        spriteRenderer.sortingOrder = baseOrder + Mathf.RoundToInt(-transform.position.y * multiplier) + sortingOffset;
    }
}