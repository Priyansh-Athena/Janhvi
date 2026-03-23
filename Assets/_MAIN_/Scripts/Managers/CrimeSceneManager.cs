using DG.Tweening;
using UnityEngine;

public class CrimeSceneManager : MonoBehaviour
{
    [Header("4. ARRIVAL AT CRIME SCENE")]
    [SerializeField] DialogueSequenceRunner arrivalAtCrimeScene_Dialogues;
    [SerializeField] CanvasGroup openToolCg, openEvidenceCg;

    private void Start()
    {
        switch(Persisting.Instance.dialogueNumber)
        {
            case 4:
                ArrivalAtCrimeScene();
                break;
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
}
