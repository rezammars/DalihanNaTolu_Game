using UnityEngine;

public class PlayheadController : MonoBehaviour
{
    public RectTransform[] lanes;
    public RectTransform hitLine;
    public int currentLane = 2;

    // Start is called before the first frame update
    void Start()
    {
        SnapToLane();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentLane--;
            if (currentLane < 0) currentLane = 0;
            SnapToLane();
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentLane++;
            if (currentLane > 4) currentLane = 4;
            SnapToLane();
        }
    }

    void SnapToLane()
    {
        Vector3 pos = transform.position;
        pos.x = lanes[currentLane].position.x;
        pos.y = hitLine.position.y; 
        transform.position = pos;
    }
}
