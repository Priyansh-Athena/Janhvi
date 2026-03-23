using DG.Tweening;
using UnityEngine;

public class CrimeSceneManager : MonoBehaviour
{
    [Header("4. ARRIVAL AT CRIME SCENE")]
    [SerializeField] DialogueSequenceRunner arrivalAtCrimeScene_Dialogues;
    [SerializeField] CanvasGroup openToolCg, openEvidenceCg;

    [Space(10), Header("5. Evidence Collected")]
    [SerializeField] DialogueSequenceRunner evidenceCollected_Dialogues;
    [SerializeField] BoxCollider doorCollider;

    int evidenceTypesCollected = 0;
    bool photoCameraUsed, fingerprintKitUsed, tweezerUsed, scaleUsed;


    private void OnEnable()
    {
        PhotoCamera.OnPictureClicked += PictureClicked;
        FingerprintKit.OnFingerPrintKitUsed += OnFingerprintKitUsed;
        Tweezer.OnTweezerUsed += OnTweezerUsed;
        Scale.OnScaleUsed += OnScaleUsed;
    }

    private void OnDisable()
    {
        PhotoCamera.OnPictureClicked -= PictureClicked;
        FingerprintKit.OnFingerPrintKitUsed -= OnFingerprintKitUsed;
        Tweezer.OnTweezerUsed -= OnTweezerUsed;
        Scale.OnScaleUsed -= OnScaleUsed;
    }

    private void Start()
    {
        switch(Persisting.Instance.dialogueNumber)
        {
            case 4:
                ArrivalAtCrimeScene();
                break;
        }
    }

    void PictureClicked()
    {
        if (photoCameraUsed) return;

        photoCameraUsed = true;
        evidenceTypesCollected++;

        if(evidenceTypesCollected == 4)
        {
            EvidenceCollected();
        }
    }

    void OnFingerprintKitUsed()
    {
        if (fingerprintKitUsed) return;

        fingerprintKitUsed = true;
        evidenceTypesCollected++;

        if (evidenceTypesCollected == 4)
        {
            EvidenceCollected();
        }
    }

    void OnTweezerUsed()
    {
        if (tweezerUsed) return;

        tweezerUsed = true;
        evidenceTypesCollected++;

        if (evidenceTypesCollected == 4)
        {
            EvidenceCollected();
        }
    }

    void OnScaleUsed()
    {
        if (scaleUsed) return;

        scaleUsed = true;   
        evidenceTypesCollected++;

        if (evidenceTypesCollected == 4)
        {
            EvidenceCollected();
        }
    }

    public void ArrivalAtCrimeScene()
    {
        arrivalAtCrimeScene_Dialogues.Run();
    }

    public void ArrivalAtCrimeScene_1()
    {
        Persisting.Instance.ShowPlayerInstruction("Objective: Investigate the crime scene");
        DOVirtual.DelayedCall(5f, () =>
        {
            Persisting.Instance.HidePlayerInstruction();

            openToolCg.DOFade(1f, 0.5f);
            openToolCg.blocksRaycasts = true;
            openEvidenceCg.DOFade(1f, 0.5f);
            openEvidenceCg.blocksRaycasts = true;
        });
    }

    public void EvidenceCollected()
    {
        evidenceCollected_Dialogues.Run();
    }

    public void EvidenceCollected_1()
    {
        Persisting.Instance.ShowPlayerInstruction("Objective: Return to headquarters for analysis.");
        DOVirtual.DelayedCall(5f, () =>
        {
            Persisting.Instance.HidePlayerInstruction();

            doorCollider.enabled = true;
            Persisting.Instance.dialogueNumber++;
        });
    }
}
