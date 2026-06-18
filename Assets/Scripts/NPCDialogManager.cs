using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NPCDialogManager : MonoBehaviour
{

    [Header("UI")]
    public GameObject panelDialog;
    public Image gambarNPC;
    public Text teksNama;
    public Text teksDialog;
    public Text teksTugas;

    [Header("Player")]
    public PlayerMovement pergerakanPemain;

    [Header("Progress")]
    public int totalNPC = 3;
    public UnityEvent onAllNPCFinished;

    string[] currentDialogs;
    int indexDialog = 0;
    bool isDialogAktif = false;
    string npcSedangBicara;
    int jumlahNPCSelesai = 0;
    string[] npcYangSudahDiajakBicara = new string[10];

    void Start()
    {
        if (panelDialog != null)
            panelDialog.SetActive(false);

        UpdateTeksTugas();
    }

    void Update()
    {
        if (!isDialogAktif) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            NextDialog();
        }
    }

    public void MulaiDialog(string namaNPC, Sprite spriteNPC, string[] dialogs)
    {
        npcSedangBicara = namaNPC;

        if (panelDialog != null)
            panelDialog.SetActive(true);

        if (gambarNPC != null)
            gambarNPC.sprite = spriteNPC;

        if (teksNama != null)
            teksNama.text = namaNPC;

        currentDialogs = dialogs;
        indexDialog = 0;
        isDialogAktif = true;

        if (pergerakanPemain != null)
            pergerakanPemain.canMove = false;

        ShowDialog();
    }
    
    void ShowDialog()
    {
        if (currentDialogs == null || currentDialogs.Length == 0)
        {
            AkhiriDialog();
            return;
        }

        if (teksDialog != null)
            teksDialog.text = currentDialogs[indexDialog];
    }

    void NextDialog()
    {
        indexDialog++;

        if (indexDialog >= currentDialogs.Length)
            AkhiriDialog();
        else
            ShowDialog();
    }

    void AkhiriDialog()
    {
        isDialogAktif = false;

        if (panelDialog != null)
            panelDialog.SetActive(false);

        if (pergerakanPemain != null)
            pergerakanPemain.canMove = true;
        
        TandaiNPCSelesai(npcSedangBicara);
    }

    void TandaiNPCSelesai(string namaNPC)
    {
        if (string.IsNullOrEmpty(namaNPC)) return;

        for (int i = 0; i < jumlahNPCSelesai; i++)
        {
            if (npcYangSudahDiajakBicara[i] == namaNPC)
                return;
        }

        npcYangSudahDiajakBicara[jumlahNPCSelesai] = namaNPC;
        jumlahNPCSelesai++;

        UpdateTeksTugas();

        Debug.Log("NPC selesai diajak bicara: " + jumlahNPCSelesai + "/" + totalNPC);

        if (jumlahNPCSelesai >= totalNPC)
        {
            Debug.Log("Semua NPC sudah diajak bicara.");
            onAllNPCFinished.Invoke();
        }
    }

    void UpdateTeksTugas()
    {
        if (teksTugas != null)
        {
            teksTugas.text =    "Tugas: Bicara dengan keluarga " + jumlahNPCSelesai + "/" + totalNPC;
        }
    }
}