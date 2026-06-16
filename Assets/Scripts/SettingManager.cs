using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SettingManager : MonoBehaviour
{
    [Header("Panel")]
    public GameObject pengaturanPanel;
    public GameObject audioPanel;
    public GameObject kontrolPanel;

    [Header("Audio Slider")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    [Header("Scene")]
    public string namaSceneMainMenu = "MainMenu";

    [Header("Delay")]
    public float delay = 0.15f;

    void Start()
    {
        if (pengaturanPanel != null)
            pengaturanPanel.SetActive(false);

        if (audioPanel != null)
            audioPanel.SetActive(false);

        if (kontrolPanel != null)
            kontrolPanel.SetActive(false);

        SetupSlider();
    }

    void SetupSlider()
    {
        if (AudioManager.Instance == null) return;

        if (bgmSlider != null)
        {
            bgmSlider.value = AudioManager.Instance.GetBGMVolume();
            bgmSlider.onValueChanged.RemoveAllListeners();
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = AudioManager.Instance.GetSFXVolume();
            sfxSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    public void BukaPengaturan()
    {
        PlayClickSFX();

        if (pengaturanPanel != null)
            pengaturanPanel.SetActive(true);

        if (audioPanel != null)
            audioPanel.SetActive(false);

        if (kontrolPanel != null)
            kontrolPanel.SetActive(false);
    }

    public void TutupPengaturan()
    {
        PlayClickSFX();

        if (pengaturanPanel != null)
            pengaturanPanel.SetActive(false);

        if (audioPanel != null)
            audioPanel.SetActive(false);

        if (kontrolPanel != null)
            kontrolPanel.SetActive(false);
    }

    public void BukaAudio()
    {
        PlayClickSFX();

        if (pengaturanPanel != null)
            pengaturanPanel.SetActive(false);

        if (audioPanel != null)
            audioPanel.SetActive(true);

        if (kontrolPanel != null)
            kontrolPanel.SetActive(false);

        SetupSlider();
    }

    public void BukaKontrol()
    {
        PlayClickSFX();

        if (pengaturanPanel != null)
            pengaturanPanel.SetActive(false);

        if (audioPanel != null)
            audioPanel.SetActive(false);

        if (kontrolPanel != null)
            kontrolPanel.SetActive(true);
    }

    public void KembaliKePengaturan()
    {
        PlayClickSFX();

        if (pengaturanPanel != null)
            pengaturanPanel.SetActive(true);

        if (audioPanel != null)
            audioPanel.SetActive(false);

        if (kontrolPanel != null)
            kontrolPanel.SetActive(false);
    }

    public void SetBGMVolume(float value)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetBGMVolume(value);
    }

    public void SetSFXVolume(float value)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetSFXVolume(value);
    }

    public void KeluarGame()
    {
        StartCoroutine(KeluarGameDenganSFX());
    }

    IEnumerator KeluarGameDenganSFX()
    {
        PlayClickSFX();

        yield return new WaitForSecondsRealtime(delay);

        Application.Quit();
        Debug.Log("Keluar dari game.");
    }

    void PlayClickSFX()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayKlikTombol();
    }
}