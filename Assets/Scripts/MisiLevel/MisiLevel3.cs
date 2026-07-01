using UnityEngine;
using UnityEngine.UI;

public class MisiLevel3 : MonoBehaviour
{
    [Header("Flow")]
    public FlowPanel flowPanel;

    [Header("NPC")]
    public NPCInteract tetua;

    [Header("Bubble")]
    public GameObject bubbleNPC1;
    public GameObject bubbleNPC2;

    [Header("UI")]
    public Text teksMisi;

    int misiTahap;

    void Start()
    {
        ResetMisi();
    }

    void ResetMisi()
    {
        misiTahap = 0;

        tetua.SetInteractable(false);

        if (bubbleNPC1 != null)
            bubbleNPC1.SetActive(false);

        if (bubbleNPC2 != null)
            bubbleNPC2.SetActive(false);

        SetMisi("");
    }

    void SetMisi(string text)
    {
        if (teksMisi != null)
            teksMisi.text = text;
    }

    //=========================
    // Cutscene selesai
    //=========================

    public void OnCutsceneSelesai()
    {
        flowPanel.ShowStep(1);

        misiTahap = 1;

        tetua.SetInteractable(true);

        if (bubbleNPC1 != null)
            bubbleNPC1.SetActive(true);

        if (bubbleNPC2 != null)
            bubbleNPC2.SetActive(true);

        SetMisi("Ada keributan terjadi, bicara pada tetua untuk mengetahui apa yang terjadi");
    }

    //=========================
    // Interaksi Tetua pertama
    //=========================

    public void OnDialogAwal()
    {
        if (misiTahap != 1)
            return;

        if (bubbleNPC1 != null)
            bubbleNPC1.SetActive(false);

        if (bubbleNPC2 != null)
            bubbleNPC2.SetActive(false);

        flowPanel.ShowStep(2);
    }

    //=========================
    // Panel Dialog selesai
    //=========================

    public void OnDialogAwalSelesai()
    {
        misiTahap = 2;

        flowPanel.ShowStep(3);
    }

    //=========================
    // Puzzle Keseimbangan selesai
    //=========================

    public void OnPuzzleKeseimbanganSelesai()
    {
        flowPanel.ShowStep(1);

        misiTahap = 3;

        tetua.SetInteractable(true);
        tetua.eventType = NPCInteract.EventType.TetuaSimbolAdat;

        SetMisi("Bicara pada tetua mengenai simbol adat");
    }

    //=========================
    // Interaksi Tetua kedua
    //=========================

    public void OnPuzzleSimbol()
    {
        if (misiTahap != 3)
            return;

        misiTahap = 4;

        flowPanel.ShowStep(4);
    }

    //=========================
    // Puzzle Simbol selesai
    //=========================

    public void OnPuzzleSimbolSelesai()
    {
        misiTahap = 5;

        flowPanel.ShowStep(5);
    }

    //=========================
    // Dialog akhir selesai
    //=========================

    public void OnDialogAkhirSelesai()
    {
        flowPanel.ShowStep(6);
    }
}