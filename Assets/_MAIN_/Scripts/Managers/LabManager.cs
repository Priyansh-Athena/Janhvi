using DG.Tweening;
using UnityEngine;

public class LabManager : MonoBehaviour
{
    [SerializeField] FirstPersonController player;
    [SerializeField] CameraMoveController camMove;
    [SerializeField] Camera cutsceneCamera;

    [SerializeField] Transform targetAssistant;

    [Header("2. Role Introduction")]
    [SerializeField] DialogueSequenceRunner roleIntroduction_Dialogues;
    [SerializeField] Transform targetChiefInvestigator;

    [Header("3. What is Forensics")]
    [SerializeField] DialogueSequenceRunner whatIsForensics_Dialogues_1;
    [SerializeField] DialogueSequenceRunner whatIsForensics_Dialogues_2;

    [Header("4. Forensics Tranining")]
    [SerializeField] DialogueSequenceRunner forensicsTraining_Dialogues_1;
    [SerializeField] DialogueSequenceRunner forensicsTraining_Dialogues_2;
    [SerializeField] DialogueSequenceRunner forensicsTraining_Dialogues_3;
    [SerializeField] DialogueSequenceRunner forensicsTraining_Dialogues_4;
    [SerializeField] DialogueSequenceRunner forensicsTraining_Dialogues_5;
    [SerializeField] DialogueSequenceRunner forensicsTraining_Dialogues_6;
    [SerializeField] CanvasGroup openToolCg, openEvidenceCg;
    [SerializeField] Transform gateTransform;
    [SerializeField] GameObject fingerprintObj, tweezerObj, scaleObj;
    int picturesClicked = 0;


    private void Start()
    {
        switch(Persisting.Instance.dialogueNumber)
        {
            case 2:
                RoleIntroduction();
                break;
        }
    }

    public void RoleIntroduction()
    {
        camMove.RotateToLookAt(targetChiefInvestigator, 0.5f, () =>
        {
            roleIntroduction_Dialogues.Run();
        });
    }

    public void RoleIntroduction_1()
    {
        Persisting.Instance.ShowPlayerInstruction("Objective: Complete forensic training");
        DOVirtual.DelayedCall(5f, () =>
        {
            Persisting.Instance.HidePlayerInstruction();

            WhatIsForensics();
        });
    }

    public void WhatIsForensics()
    {
        whatIsForensics_Dialogues_1.Run();
    }

    public void WhatIsForensics_1()
    {
        camMove.RotateToLookAt(targetAssistant, 1f);
        whatIsForensics_Dialogues_2.Run();
    }

    public void WhatIsForensics_2()
    {
        Persisting.Instance.ShowPlayerInstruction("FORENSIC TRAINING");
        DOVirtual.DelayedCall(5f, () =>
        {
            Persisting.Instance.HidePlayerInstruction();

            DOVirtual.DelayedCall(1f, () =>
            {
                ForensicsTraining();
            });
        });
    }

    public void ForensicsTraining()
    {
        forensicsTraining_Dialogues_1.Run();
    }

    public void ForensicsTraining_1()
    {
        openToolCg.DOFade(1f, 0.5f).OnComplete(() =>
        {
            openToolCg.blocksRaycasts = true;
        });
    }

    public void ForensicsTraining_2()
    {
        Persisting.Instance.ShowPlayerInstruction("Objective: Take 3 photographs");

        PhotoCamera.OnPictureClicked += ForensicsTraining_3;

        DOVirtual.DelayedCall(5f, () =>
        {
            Persisting.Instance.HidePlayerInstruction();
            camMove.gameObject.SetActive(false);
            player.gameObject.SetActive(true);
        });
    }

    public void ForensicsTraining_3()
    {
        picturesClicked++;

        if(picturesClicked == 3)
        {
            PhotoCamera.OnPictureClicked -= ForensicsTraining_3;
            forensicsTraining_Dialogues_2.Run();
            camMove.gameObject.SetActive(true);
            player.gameObject.SetActive(false);
        }
    }

    public void ForensicsTraining_4()
    {
        openEvidenceCg.DOFade(1f, 0.5f).OnComplete(() =>
        {
            openEvidenceCg.blocksRaycasts = true;

            PhotoGalleryUI.OnToggleGallery += ForensicsTraining_5;
        });
    }

    public void ForensicsTraining_5(bool toggle)
    {
        if(!toggle)
        {
            PhotoGalleryUI.OnToggleGallery -= ForensicsTraining_5;
            forensicsTraining_Dialogues_3.Run();
        }
    }

    public void ForensicsTraining_6()
    {
        Persisting.Instance.ShowPlayerInstruction("Objective: Use the fingerprint kit on the glass");
        DOVirtual.DelayedCall(5f, () =>
        {
            Persisting.Instance.HidePlayerInstruction();

            DOVirtual.DelayedCall(1f, () =>
            {
                camMove.gameObject.SetActive(false);
                player.gameObject.SetActive(true);

                fingerprintObj.SetActive(true);
                FingerprintKit.OnFingerPrintKitUsed += ForensicsTraining_7;
            });
        });
    }

    public void ForensicsTraining_7()
    {
        FingerprintKit.OnFingerPrintKitUsed -= ForensicsTraining_7;
        forensicsTraining_Dialogues_4.Run();

        player.gameObject.SetActive(false);
        camMove.gameObject.SetActive(true);
    }

    public void ForensicsTraining_8()
    {
        Persisting.Instance.ShowPlayerInstruction("Objective: Collect the fiber from the table using Tweezer");
        DOVirtual.DelayedCall(5f, () =>
        {
            Persisting.Instance.HidePlayerInstruction();

            DOVirtual.DelayedCall(1f, () =>
            {
                camMove.gameObject.SetActive(false);
                player.gameObject.SetActive(true);

                fingerprintObj.SetActive(false);
                tweezerObj.SetActive(true);
                Tweezer.OnTweezerUsed += ForensicsTraining_9;
            });
        });
    }

    public void ForensicsTraining_9()
    {
        Tweezer.OnTweezerUsed -= ForensicsTraining_9;
        forensicsTraining_Dialogues_5.Run();

        player.gameObject.SetActive(false);
        camMove.gameObject.SetActive(true);
    }

    public void ForensicsTraining_10()
    {
        Persisting.Instance.ShowPlayerInstruction("Objective: Use scale to measure the length of the object on table");
        DOVirtual.DelayedCall(5f, () =>
        {
            Persisting.Instance.HidePlayerInstruction();

            DOVirtual.DelayedCall(1f, () =>
            {
                camMove.gameObject.SetActive(false);
                player.gameObject.SetActive(true);

                //tweezerObj?.SetActive(false);
                scaleObj.SetActive(true);

                Scale.OnScaleUsed += ForensicsTraining_11;

                Debug.Log("Scale Reached");
            });
        });
    }

    public void ForensicsTraining_11()
    {
        Scale.OnScaleUsed -= ForensicsTraining_11;
        forensicsTraining_Dialogues_6.Run();

        player.gameObject.SetActive(false);
        camMove.gameObject.SetActive(true);
    }

    public void ForensicsTraining_12()
    {
        Persisting.Instance.ShowPlayerInstruction("Objective: Go outside in the city for petroling");
        DOVirtual.DelayedCall(5f, () =>
        {
            Persisting.Instance.HidePlayerInstruction();
            camMove.RotateToLookAt(gateTransform, 1f);
            cutsceneCamera.DOFieldOfView(25, 1f).OnComplete(() =>
            {
                cutsceneCamera.DOFieldOfView(60, 0.35f).OnComplete(() =>
                {
                    camMove.gameObject.SetActive(false);
                    player.gameObject.SetActive(true);

                    scaleObj.SetActive(false);
                    player.playerCanMove = true;

                    openToolCg.blocksRaycasts = false;
                    openEvidenceCg.blocksRaycasts = false;

                    openToolCg.DOFade(1f, 0.5f);
                    openEvidenceCg.DOFade(1f, 0.5f);

                    Persisting.Instance.capturedPhotos.Clear();

                    Persisting.Instance.dialogueNumber++;
                });
            });
        });
    }
}
