using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sumber Audio")]
    public AudioSource sumberBGM;
    public AudioSource sumberSFX;

    [Header("BGM")]
    public AudioClip bgmClip;

    [Header("SFX")]
    public AudioClip sfxTombolKlik;
    public AudioClip sfxDrag;
    public AudioClip sfxDrop;
    public AudioClip sfxGeser;

    [Header("Volume")]
    [Range(0f, 1f)] public float bgmVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        if (sumberBGM != null)
            sumberBGM.volume = bgmVolume;

        if (sumberSFX != null)
            sumberSFX.volume = sfxVolume;
    }

    void Start()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (sumberBGM == null || bgmClip == null) return;

        if (sumberBGM.clip != bgmClip)
            sumberBGM.clip = bgmClip;

        sumberBGM.loop = true;

        if (!sumberBGM.isPlaying)
            sumberBGM.Play();
    }

    public void StopBGM()
    {
        if (sumberBGM != null)
            sumberBGM.Stop();
    }

    public void SetBGMVolume(float value)
    {
        bgmVolume = value;

        if (sumberBGM != null)
            sumberBGM.volume = bgmVolume;

        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;

        if (sumberSFX != null)
            sumberSFX.volume = sfxVolume;

        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    public float GetBGMVolume()
    {
        return bgmVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    public void PlayKlikTombol()
    {
        PlaySFX(sfxTombolKlik);
    }

    public void PlayDrag()
    {
        PlaySFX(sfxDrag);
    }

    public void PlayDrop()
    {
        PlaySFX(sfxDrop);
    }

    public void PlayGeser()
    {
        PlaySFX(sfxGeser);
    }

    void PlaySFX(AudioClip clip)
    {
        if (sumberSFX != null && clip != null)
            sumberSFX.PlayOneShot(clip);
    }
}