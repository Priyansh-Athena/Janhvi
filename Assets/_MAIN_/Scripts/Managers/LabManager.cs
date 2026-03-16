using DG.Tweening;
using UnityEngine;

public class LabManager : MonoBehaviour
{
    [SerializeField] FirstPersonController player;
    [SerializeField] CameraMoveController camMove;

    [SerializeField] Transform targetAssistant;

    [Header("2. Role Introduction")]
    [SerializeField] DialogueSequenceRunner roleIntroduction_Dialogues;
    [SerializeField] Transform targetChiefInvestigator;

    [Header("3. What is Forensics")]
    [SerializeField] DialogueSequenceRunner whatIsForensics_Dialogues_1;
    [SerializeField] DialogueSequenceRunner whatIsForensics_Dialogues_2;


    private void Start()
    {
        RoleIntroduction();
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
        });
    }
}
