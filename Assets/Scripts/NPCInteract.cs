using UnityEngine;
using UnityEngine.Events;

public class NPCInteract : MonoBehaviour
{
    [Header("NPC Data")]
    public string namaNPC;
    public Sprite npcDialogSprite;

    [TextArea(3, 10)]
    public string[] dialogs;

    [Header("Dialog")]
    public NPCDialogManager dialogManager;

    [Header("Mission")]
    public MisiLevel1 misiLevel1;
    public MisiLevel2 misiLevel2;

    [Header("Interaction")]
    public bool canInteract = true;
    public bool directEvent = false;

    public enum EventType
    {
        None,
        TetuaPuzzlePeran,
        BoruUlos,
        BoruPuzzleUrutan,
        TetuaDialogLevel2,
        PemusikLagu
    }

    public EventType eventType;

    bool pemainDekat;

    void Update()
    {
        if (!canInteract)
            return;

        if (!pemainDekat)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        if (directEvent)
        {
            RunEvent();
            return;
        }

        if (dialogManager != null)
        {
            dialogManager.MulaiDialog( namaNPC, npcDialogSprite, dialogs, GetDialogFinishAction());
        }
    }

    UnityAction GetDialogFinishAction()
    {
        if (misiLevel1 == null)
        return null;

        if (namaNPC == "Tetua Adat" && !directEvent)
        return misiLevel1.OnTetuaPertamaSelesai;

        if (namaNPC == "Hula")
        return misiLevel1.OnHulaSelesai;

        if (namaNPC == "Dongan")
        return misiLevel1.OnDonganSelesai;

        if (namaNPC == "Boru" && !directEvent)
        return misiLevel1.OnBoruPertamaSelesai;

        return null;
    }

    void RunEvent()
    {
        switch (eventType)
        {
            case EventType.TetuaPuzzlePeran:
                if (misiLevel1 != null)
                misiLevel1.OnTetuaBukaPuzzlePeran();
                break;
            case EventType.BoruUlos:
                if (misiLevel1 != null)
                misiLevel1.OnBoruUlos();
                break;            
            case EventType.BoruPuzzleUrutan:
                if (misiLevel2 != null)
                misiLevel2.OnBukaPuzzleUrutan();
                break;
            case EventType.TetuaDialogLevel2:
                if (misiLevel2 != null)
                misiLevel2.OnDialogTetua();
                break;
            case EventType.PemusikLagu:
                if (misiLevel2 != null)
                misiLevel2.OnDialogPemusik();
                break;
        }
    }

    public void SetInteractable(bool value)
    {
        canInteract = value;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pemainDekat = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pemainDekat = false;
        }
    }
}