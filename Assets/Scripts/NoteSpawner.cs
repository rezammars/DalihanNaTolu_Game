using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public RectTransform[] spawnPoints;
    public Transform noteContainer;

    public float spawnRate = 1.2f;
    public float noteSpeed = 300f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnNote), 1f, spawnRate);
    }

    void SpawnNote()
    {
        int lane = Random.Range(0, spawnPoints.Length);

        GameObject noteObj = Instantiate(notePrefab, noteContainer);

        RectTransform noteRect = noteObj.GetComponent<RectTransform>();
        noteRect.anchoredPosition = spawnPoints[lane].anchoredPosition;

        Note note = noteObj.GetComponent<Note>();
        note.laneIndex = lane;
        note.speed = noteSpeed;
    }
}
