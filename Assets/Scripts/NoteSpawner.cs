using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public RectTransform[] lanes;
    public Transform noteContainer;
    public float spawnRate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnNote), 1f, spawnRate);
    }

    void SpawnNote()
    {
        int lane = Random.Range(0, 5);

        GameObject note = Instantiate(notePrefab, noteContainer);
        RectTransform rt = note.GetComponent<RectTransform>();

        rt.anchoredPosition = lanes[lane].anchoredPosition + Vector2.up * 1000f;

        note.GetComponent<Note>().laneIndex = lane;
    }
}
