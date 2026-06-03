using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RhythmManager : MonoBehaviour
{
    public static RhythmManager instance;

    [Header("Lane")]
    public RectTransform playhead;
    public RectTransform[] lanePositions;

    [Header("Hit")]
    public float perfectRange = 35f;
    public float goodRange = 70f;

    [Header("Score Settings")]
    public int perfectScore = 100;
    public int goodScore = 50;

    [Header("UI")]
    public Text feedbackText;
    public Text scoreText;

    [Header("Result")]
    public int targetScore = 1500;
    public string nextSceneName;

    private int currentLane = 2;
    private int score = 0;

    private bool isFinished = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdatePlayheadPosition();
        UpdateUI();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        if (isFinished) return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveLane(-1);
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveLane(1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryHit();
        }
    }

    void MoveLane(int direction)
    {
        currentLane += direction;

        currentLane = Mathf.Clamp(currentLane, 0, lanePositions.Length - 1);

        UpdatePlayheadPosition();
        UpdateUI();
    }

    void UpdatePlayheadPosition()
    {
        Vector2 pos = playhead.anchoredPosition;

        pos.y = lanePositions[currentLane].anchoredPosition.y;

        playhead.anchoredPosition = pos;
    }

    void TryHit()
    {
        Note[] notes = FindObjectsOfType<Note>();

        Note closestNote = null;
        float closestDistance = Mathf.Infinity;

        foreach (Note note in notes)
        {
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
            feedbackText.text = "Miss!";
            return;
        }

        if (closestDistance <= perfectRange)
        {
            score += perfectScore;
            feedbackText.text = "Perfect!";
            Destroy(closestNote.gameObject);
        }
        else if (closestDistance <= goodRange)
        {
            score += goodScore;
            feedbackText.text = "Good!";
            Destroy(closestNote.gameObject);
        }
        else
        {
            feedbackText.text = "Miss!";
        }

        UpdateUI();
        CheckFinish();
    }

    public void MissNote(Note note)
    {
        if (note == null) return;
        if (isFinished) return;

        feedbackText.text = "Miss!";

        Destroy(note.gameObject);
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
    }

    void CheckFinish()
    {
        if (score >= targetScore)
        {
            isFinished = true;

            feedbackText.text = "Gondang terdengar harmonis!";

            Invoke(nameof(LoadNextScene), 2f);
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
