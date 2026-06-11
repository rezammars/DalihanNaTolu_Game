using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class RhythmManager : MonoBehaviour
{
    [Header("Lane Settings")]
    public RectTransform playhead;
    public RectTransform[] lanePositions;

    [Header("Note Spawn")]
    public GameObject notePrefab;
    public RectTransform[] spawnPoints;
    public Transform noteContainer;
    public float spawnRate = 1.2f;
    public float noteSpeed = 300f;

    [Header("Hit Settings")]
    public float hitRange = 80f;

    [Header("Score Settings")]
    public int hitScore = 100;
    public int targetScore = 1000;

    [Header("UI")]
    public Text feedbackText;
    public Text scoreText;

    [Header("After Finished")]
    public UnityEvent onRhythmFinished;

    public bool IsPlaying { get; private set; }

    int currentLane = 2;
    int score = 0;
    bool finished = false;

    Coroutine spawnRoutine;

    void Start()
    {
        StopGame();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        if (!IsPlaying) return;
        if (finished) return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            MoveLane(-1);

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            MoveLane(1);

        if (Input.GetKeyDown(KeyCode.Space))
            TryHit();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        score = 0;
        currentLane = 2;
        finished = false;
        IsPlaying = true;

        ClearNotes();
        UpdatePlayheadPosition();
        UpdateUI();

        if (feedbackText != null)
            feedbackText.text = "";

        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);

        spawnRoutine = StartCoroutine(SpawnRoutine());

        Debug.Log("RhythmManager: StartGame");
    }

    public void StopGame()
    {
        IsPlaying = false;

        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }

        ClearNotes();
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(1f);

        while (IsPlaying && !finished)
        {
            SpawnNote();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnNote()
    {
        if (notePrefab == null)
        {
            Debug.LogWarning("Note Prefab belum diisi.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Spawn Points belum diisi.");
            return;
        }

        if (noteContainer == null)
        {
            Debug.LogWarning("Note Container belum diisi.");
            return;
        }

        int lane = Random.Range(0, spawnPoints.Length);

        GameObject noteObj = Instantiate(notePrefab, noteContainer);

        RectTransform noteRect = noteObj.GetComponent<RectTransform>();
        noteRect.anchoredPosition = spawnPoints[lane].anchoredPosition;

        RhythmNote note = noteObj.GetComponent<RhythmNote>();

        if (note == null)
            note = noteObj.AddComponent<RhythmNote>();

        note.Init(lane, noteSpeed, this);
    }

    void MoveLane(int direction)
    {
        currentLane += direction;
        currentLane = Mathf.Clamp(currentLane, 0, lanePositions.Length - 1);

        UpdatePlayheadPosition();
    }

    void UpdatePlayheadPosition()
    {
        if (playhead == null) return;
        if (lanePositions == null || lanePositions.Length == 0) return;

        Vector2 pos = playhead.anchoredPosition;
        pos.y = lanePositions[currentLane].anchoredPosition.y;
        playhead.anchoredPosition = pos;
    }

    void TryHit()
    {
        RhythmNote[] notes = FindObjectsOfType<RhythmNote>();

        RhythmNote closestNote = null;
        float closestDistance = Mathf.Infinity;

        foreach (RhythmNote note in notes)
        {
            if (note == null) continue;
            if (note.laneIndex != currentLane) continue;

            float distance = Mathf.Abs(
                note.GetRect().anchoredPosition.x - playhead.anchoredPosition.x
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNote = note;
            }
        }

        if (closestNote == null)
        {
            Miss();
            return;
        }

        if (closestDistance <= hitRange)
        {
            Hit(closestNote);
        }
        else
        {
            Miss();
        }
    }

    void Hit(RhythmNote note)
    {
        score += hitScore;
        UpdateUI();

        if (feedbackText != null)
            feedbackText.text = "Hit!";

        Destroy(note.gameObject);

        if (score >= targetScore)
            FinishGame();
    }

    void Miss()
    {
        if (feedbackText != null)
            feedbackText.text = "Miss!";
    }

    public void MissNote(RhythmNote note)
    {
        if (!IsPlaying) return;
        if (finished) return;
        if (note == null) return;

        if (feedbackText != null)
            feedbackText.text = "Miss!";

        Destroy(note.gameObject);
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score : " + score;
    }

    void FinishGame()
    {
        if (finished) return;

        finished = true;
        IsPlaying = false;

        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }

        ClearNotes();

        if (feedbackText != null)
            feedbackText.text = "Gondang terdengar harmonis!";

        Invoke(nameof(CallFinishEvent), 1.2f);
    }

    void CallFinishEvent()
    {
        onRhythmFinished.Invoke();
    }

    void ClearNotes()
    {
        RhythmNote[] notes = FindObjectsOfType<RhythmNote>();

        foreach (RhythmNote note in notes)
        {
            if (note != null)
                Destroy(note.gameObject);
        }
    }
}