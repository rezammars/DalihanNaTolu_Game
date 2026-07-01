using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MisiLevel2 : MonoBehaviour
{
    [Header("Flow")]
    public FlowPanel flowPanel;

    [Header("NPC")]
    public NPCInteract boru;
    public NPCInteract pemusik;
    public NPCInteract tetua;

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
        boru.SetInteractable(false);
        pemusik.SetInteractable(false);

        SetMisi("");
    }

    void SetMisi(string text)
    {
        if (teksMisi != null)
            teksMisi.text = text;
    }

    // Dialog awal selesai
    public void OnCutsceneSelesai()
    {
        Debug.Log("tetua bisa interaksi");
        flowPanel.ShowStep(1);

        misiTahap = 1;

        tetua.SetInteractable(true);
        boru.SetInteractable(false);
        pemusik.SetInteractable(false);
        
        SetMisi("Temui tetua adat");
    }

    public void OnDialogTetua()
    {
        if (misiTahap != 1)
            return;
        
        flowPanel.ShowStep(2);
    }

    public void OnDialogTetuaSelesai()
    {
        flowPanel.ShowStep(1);
        misiTahap = 2;
        tetua.SetInteractable(false);
        boru.SetInteractable(true);

        SetMisi("Temui anak boru untuk susun rangkaian kegiatan acara");
    }

    // Interaksi Boru -> Puzzle Urutan
    public void OnBukaPuzzleUrutan()
    {
        if (misiTahap != 2)
            return;

        flowPanel.ShowStep(3);
    }

    // Puzzle Urutan selesai
    public void OnPuzzleUrutanSelesai()
    {
        flowPanel.ShowStep(1);

        misiTahap = 3;

        boru.SetInteractable(false);

        pemusik.SetInteractable(true);

        SetMisi("Bicara dengan pemusik gondang di ujung sana");
    }

    // Dialog Pemusik
    public void OnDialogPemusik()
    {
        if (misiTahap != 3)
            return;

        misiTahap = 3;

        flowPanel.ShowStep(4);
    }

    // Rhythm Game selesai
    public void OnRhythmGameSelesai()
    {
        flowPanel.ShowStep(5);
    }

    // Dialog akhir selesai
    public void OnDialogAkhirSelesai()
    {
        flowPanel.ShowStep(6);
    }
}