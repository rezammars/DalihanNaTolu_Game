using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TutorialPanel : MonoBehaviour
{
    [Header("UI")]
    public GameObject tutorialPanel;
    public Text tutorialText;

    [TextArea(3, 10)]
    public string tutorialMessage;

    [Header("Settings")]
    public bool showOnEnable = true;
    public bool pauseGameWhileOpen = false;
    public float inputDelay = 0.2f;

    [Header("After Closed")]
    public UnityEvent onClosed;

    bool isOpen = false;
    bool canClose = false;

    void OnEnable()
    {
        if (showOnEnable)
        {
            ShowTutorial();
        }
        else
        {
            HideTutorialOnly();
        }
    }

    public void ShowTutorial()
    {
        isOpen = true;
        canClose = false;

        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);

        if (tutorialText != null)
            tutorialText.text = tutorialMessage;

        if (pauseGameWhileOpen)
            Time.timeScale = 0f;

        Invoke(nameof(AllowClose), inputDelay);
    }

    void AllowClose()
    {
        canClose = true;
    }

    void Update()
    {
        if (!isOpen) return;
        if (!canClose) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            CloseTutorial();
        }
    }

    public void CloseTutorial()
    {
        isOpen = false;
        canClose = false;

        HideTutorialOnly();

        if (pauseGameWhileOpen)
            Time.timeScale = 1f;

        onClosed.Invoke();
    }

    void HideTutorialOnly()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
    }
}