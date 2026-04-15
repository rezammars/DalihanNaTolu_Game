using UnityEngine;

public class HitLine : MonoBehaviour
{
    public PlayheadController playhead;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Note note = other.GetComponent<Note>();

        if (note != null)
        {
            if (note.laneIndex == playhead.currentLane)
            {
                Debug.Log("Perfect!");
            }
            else
            {
                Debug.Log("Miss!");
            }
            Destroy(other.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
