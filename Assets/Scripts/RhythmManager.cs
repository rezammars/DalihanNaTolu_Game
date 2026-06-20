using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RhythmManager : MonoBehaviour
{
    [System.Serializable]
    public class NoteData
    {
        public float spawnTime;
        public int laneIndex;
    }

    [Header("Music")]
    public AudioSource rhythmMusicSource;
    public AudioClip rhythmMusicClip;
    public float songDuration = 60f;

    [Header("Note Pattern")]
    public NoteData[] notePattern;

    [Header("Lane Settings")]
    public RectTransform playhead;
    public RectTransform[] lanePositions;

    [Header("Note Spawn")]
    public GameObject notePrefab;
    public RectTransform[] spawnPoints;
    public Transform noteContainer;
    public float noteSpeed = 300f;
    public Sprite[] noteSpritesTiapLane;

    [Header("Hit Settings")]
    public float hitRange = 80f;

    [Header("Score Settings")]
    public int hitScore = 100;
    public int minScore = 1200;

    [Header("UI")]
    public Text feedbackText;
    public Text scoreText;
    public Text resultText;
    public GameObject resultPanel;

    [Header("Unlock Index")]
    public IndexManager indexManager;
    public int unlockGroupIndex = 3;

    [Header("After Success")]
    public UnityEvent onRhythmSuccess;
    public float delayLanjut = 2f;

    public bool IsPlaying { get; private set; }

    int currentLane = 2;
    int score = 0;
    int spawnedNoteIndex = 0;
    int maxScore = 0;

    float timer = 0f;
    bool finished = false;

    void Start()
    {
        StopGame();

        if (resultPanel != null)
            resultPanel.SetActive(false);
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        if (!IsPlaying) return;
        if (finished) return;

        timer += Time.deltaTime;

        SpawnNotesByTime();

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            MoveLane(-1);

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            MoveLane(1);

        if (Input.GetKeyDown(KeyCode.Space))
            TryHit();

        if (timer >= songDuration)
            FinishGame();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        score = 0;
        timer = 0f;
        spawnedNoteIndex = 0;
        currentLane = 2;
        finished = false;
        IsPlaying = true;

        maxScore = notePattern.Length * hitScore;

        ClearNotes();
        UpdatePlayheadPosition();
        UpdateUI();

        if (resultPanel != null)
            resultPanel.SetActive(false);

        if (feedbackText != null)
            feedbackText.text = "";

        if (rhythmMusicSource != null && rhythmMusicClip != null)
        {
            rhythmMusicSource.clip = rhythmMusicClip;
            rhythmMusicSource.loop = false;
            rhythmMusicSource.Play();
        }
    }

    public void StopGame()
    {
        IsPlaying = false;

        if (rhythmMusicSource != null)
            rhythmMusicSource.Stop();

        ClearNotes();
    }

    void SpawnNotesByTime()
    {
        if (notePattern == null || notePattern.Length == 0) return;

        while (spawnedNoteIndex < notePattern.Length &&
               timer >= notePattern[spawnedNoteIndex].spawnTime)
        {
            SpawnNote(notePattern[spawnedNoteIndex].laneIndex);
            spawnedNoteIndex++;
        }
    }

    void SpawnNote(int lane)
    {
        if (notePrefab == null) return;
        if (spawnPoints == null || spawnPoints.Length == 0) return;
        if (noteContainer == null) return;

        lane = Mathf.Clamp(lane, 0, spawnPoints.Length - 1);

        GameObject noteObj = Instantiate(notePrefab, noteContainer);
        RectTransform noteRect = noteObj.GetComponent<RectTransform>();
        noteRect.anchoredPosition = spawnPoints[lane].anchoredPosition;

        Image noteImage = noteObj.GetComponent<Image>();
        if (noteImage != null && noteSpritesTiapLane != null && lane < noteSpritesTiapLane.Length && noteSpritesTiapLane[lane] != null)
        {
            noteImage.sprite = noteSpritesTiapLane[lane];
            noteImage.preserveAspect = true;
        }

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

            float distance = Mathf.Abs(note.GetRect().anchoredPosition.x - playhead.anchoredPosition.x);

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
            Hit(closestNote);
        else
            Miss();
    }

    void Hit(RhythmNote note)
    {
        score += hitScore;
        UpdateUI();

        if (feedbackText != null)
            feedbackText.text = "HIT!";

        Destroy(note.gameObject);
    }

    void Miss()
    {
        if (feedbackText != null)
            feedbackText.text = "MISS!";
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
            scoreText.text = "Score: " + score + " / " + maxScore;
    }

    void FinishGame()
    {
        if (finished) return;

        finished = true;
        IsPlaying = false;

        if (rhythmMusicSource != null)
            rhythmMusicSource.Stop();

        ClearNotes();

        if (resultPanel != null)
            resultPanel.SetActive(true);

        if (score >= minScore)
        {
            UnlockIndexKhusus();
            if (resultText != null)
                resultText.text = "Berhasil!\nScore: " + score + " / " + maxScore;

            if (feedbackText != null)
                feedbackText.text = "Irama yang indah! Anda berhasil memainkan gondang dengan baik.";
        }
        else
        {
            if (resultText != null)
                resultText.text = "Belum berhasil.\nScore: " + score + " / " + maxScore;

            if (feedbackText != null)
                feedbackText.text = "Kurang indah memainkan gondang.";
        }
        Invoke(nameof(LanjutOtomatis), delayLanjut);
    }

    void UnlockIndexKhusus()
    {
        if (indexManager != null)
            indexManager.UnlockGroup(unlockGroupIndex);
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

    void LanjutOtomatis()
    {
        onRhythmSuccess.Invoke();
    }
}