using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();

        Debug.Log("Progress Reset");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
