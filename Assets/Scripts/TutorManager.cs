using UnityEngine;
using UnityEngine.UI;

public class TutorManager : MonoBehaviour
{
    public GameObject tutorPanel;
    public Text tutorText;

    [TextArea(3, 10)]
    public string tutorMessages;

    bool tutorClosed = false;

    // Start is called before the first frame update
    void Start()
    {
        tutorPanel.SetActive(true);
        tutorText.text = tutorMessages;
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorClosed) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            CloseTutor();
        }
    }

    void CloseTutor()
    {
        tutorPanel.SetActive(false);
        Time.timeScale = 1f;
        tutorClosed = true;
    }

}
