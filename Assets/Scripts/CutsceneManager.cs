using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CutsceneManager : MonoBehaviour
{
    [System.Serializable]
    public class CharacterData
    {
        public string namaKarakter;
        public Image gambarKarakter;
    }

    [Header("UI Dialog Biasa")]
    public Text teksNama;
    public Text teksDialog;
    public GameObject panelDialog;
    public Text teksCutscene;
    public GameObject overlayGelap;

    [Header("Bubble NPC Ribut")]
    public GameObject bubbleNPC_A;
    public Text teksBubbleA;

    public GameObject bubbleNPC_B;
    public Text teksBubbleB;

    [Header("Nama Untuk Bubble")]
    public string namaNPC_A = "NPC A";
    public string namaNPC_B = "NPC B";

    [Header("Dialog Data")]
    public string[] semuaNama;

    [TextArea(3, 6)]
    public string[] semuaDialog;

    [Header("Characters")]
    public CharacterData[] semuaKarakter;

    [Header("After Dialog Finished")]
    public UnityEvent onFinished;

    int index = 0;
    bool finished = false;

    void OnEnable()
    {
        index = 0;
        finished = false;
        ShowDialog();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        if (finished) return;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            NextDialog();
        }
    }

    void ShowDialog()
    {
        if (semuaDialog == null || semuaDialog.Length == 0)
        {
            Debug.LogWarning("Dialog belum diisi di " + gameObject.name);
            return;
        }

        if (index >= semuaDialog.Length) return;

        HideAllUI();

        string namaSekarang = "";

        if (semuaNama != null && index < semuaNama.Length)
            namaSekarang = semuaNama[index];

        bool isNarration = string.IsNullOrEmpty(namaSekarang);
        bool isBubbleA = namaSekarang == namaNPC_A;
        bool isBubbleB = namaSekarang == namaNPC_B;

        if (isNarration)
        {
            ShowNarration();
        }
        else if (isBubbleA)
        {
            ShowBubbleA();
        }
        else if (isBubbleB)
        {
            ShowBubbleB();
        }
        else
        {
            ShowDialogBiasa(namaSekarang);
        }

        UpdateCharacterUI(namaSekarang, isNarration);
    }

    void HideAllUI()
    {
        if (overlayGelap != null)
            overlayGelap.SetActive(false);

        if (teksCutscene != null)
            teksCutscene.gameObject.SetActive(false);

        if (panelDialog != null)
            panelDialog.SetActive(false);

        if (bubbleNPC_A != null)
            bubbleNPC_A.SetActive(false);

        if (bubbleNPC_B != null)
            bubbleNPC_B.SetActive(false);
    }

    void ShowNarration()
    {
        if (overlayGelap != null)
            overlayGelap.SetActive(true);

        if (teksCutscene != null)
        {
            teksCutscene.gameObject.SetActive(true);
            teksCutscene.text = semuaDialog[index];
        }
    }

    void ShowBubbleA()
    {
        if (bubbleNPC_A != null)
            bubbleNPC_A.SetActive(true);

        if (teksBubbleA != null)
            teksBubbleA.text = semuaDialog[index];
    }

    void ShowBubbleB()
    {
        if (bubbleNPC_B != null)
            bubbleNPC_B.SetActive(true);

        if (teksBubbleB != null)
            teksBubbleB.text = semuaDialog[index];
    }

    void ShowDialogBiasa(string namaSekarang)
    {
        if (panelDialog != null)
            panelDialog.SetActive(true);

        if (teksNama != null)
            teksNama.text = namaSekarang;

        if (teksDialog != null)
            teksDialog.text = semuaDialog[index];
    }

    void UpdateCharacterUI(string namaSekarang, bool isNarration)
    {
        if (semuaKarakter == null) return;

        bool isBubbleA = namaSekarang == namaNPC_A;
        bool isBubbleB = namaSekarang == namaNPC_B;
        bool isBubbleScene = isBubbleA || isBubbleB;

        foreach (CharacterData character in semuaKarakter)
        {
            if (character.gambarKarakter == null) continue;

            string characterName = character.namaKarakter.Trim().ToLower();
            string currentSpeaker = namaSekarang.Trim().ToLower();

            if (isNarration)
            {
                character.gambarKarakter.gameObject.SetActive(false);
                continue;
            }

            if (isBubbleScene)
            {
                bool isNPC_A_Character = character.namaKarakter == namaNPC_A;
                bool isNPC_B_Character = character.namaKarakter == namaNPC_B;

                if (isNPC_A_Character || isNPC_B_Character)
                {
                    character.gambarKarakter.gameObject.SetActive(true);

                    if (characterName == currentSpeaker)
                        character.gambarKarakter.color = Color.white;
                    else
                        character.gambarKarakter.color = new Color(0.5f, 0.5f, 0.5f);
                }
                else
                {
                    character.gambarKarakter.gameObject.SetActive(false);
                }

                continue;
            }

            bool isMainDialogCharacter =
                character.namaKarakter == "Alfredo" ||
                character.namaKarakter == "Tetua Adat";

            if (isMainDialogCharacter)
            {
                character.gambarKarakter.gameObject.SetActive(true);

                if (characterName == currentSpeaker)
                    character.gambarKarakter.color = Color.white;
                else
                    character.gambarKarakter.color = new Color(0.5f, 0.5f, 0.5f);
            }
            else
            {
                character.gambarKarakter.gameObject.SetActive(false);
            }
        }
    }

    void NextDialog()
    {
        index++;

        if (index >= semuaDialog.Length)
        {
            FinishDialog();
        }
        else
        {
            ShowDialog();
        }
    }

    void FinishDialog()
    {
        finished = true;
        HideAllUI();
        HideAllCharacters();
        onFinished.Invoke();
    }

    void HideAllCharacters()
    {
        if (semuaKarakter == null) return;

        foreach (CharacterData character in semuaKarakter)
        {
            if (character.gambarKarakter != null)
                character.gambarKarakter.gameObject.SetActive(false);
        }
    }
}