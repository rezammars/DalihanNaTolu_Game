using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [Header("Sumber Audio")]
    public AudioSource sumberAudio;

    [Header("Bagian SFX")]
    public AudioClip sfxKlikTombol;
    public AudioClip sfxDrag;
    public AudioClip sfxDrop;
    public AudioClip sfxGeser;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (sumberAudio == null)
            sumberAudio = GetComponent<AudioSource>();
    }
    
    public void MainkanSFXKlikTombol()
    {
        MainkanSFX(sfxKlikTombol);
    }

    public void MainkanSFXDrag()
    {
        MainkanSFX(sfxDrag);
    }

    public void MainkanSFXDrop()
    {
        MainkanSFX(sfxDrop);
    }

    public void MainkanSFXGeser()
    {
        MainkanSFX(sfxGeser);
    }

    public void MainkanSFX(AudioClip clip)
    {
        if (sumberAudio != null && clip != null)
            sumberAudio.PlayOneShot(clip);
    }
}
