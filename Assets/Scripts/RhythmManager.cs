using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    public Transform hitLine;
    public float hitRange = 50f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckHit();
        }
    }

    void CheckHit()
    {
        Note[] notes = FindObjectsOfType<Note>();

        foreach (Note note in notes)
        {
            float distance = Mathf.Abs(note.transform.position.y - hitLine.position.y);

            if (distance <= hitRange)
            {
                Debug.Log("Perfect!");
                Destroy(note.gameObject);
                return;
            }
        }
        Debug.Log("Miss!");
    }
}
