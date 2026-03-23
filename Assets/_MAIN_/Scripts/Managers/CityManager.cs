using AAMAP;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    [SerializeField] CameraMoveController moveCam;
    [SerializeField] FirstPersonController player;
    [SerializeField] Camera cutsceneCamera;
    [SerializeField] GameObject minimap, map;
    [SerializeField] BoxCollider crimeSceneCollider, forensicsCollider;
    [SerializeField] GameObject crimeSceneMapIcon;

    [Space(10), Header("1. GAME INTRODUCTION")]
    [SerializeField] DialogueSequenceRunner gameIntroduction_Dialogues;
    [SerializeField] Transform gameIntroduction_Pos_1;
    [SerializeField] CanvasGroup blackPanel, gameTitle, gameSubtitle;

    [Space(10), Header("2. CITY PATROL START")]
    [SerializeField] DialogueSequenceRunner cityPetrolStart_Dialogues;
    [SerializeField] Transform levelStartPlayerPos;


    private void Start()
    {
        Debug.Log($"Dialogue Number: {Persisting.Instance.dialogueNumber}");
        switch(Persisting.Instance.dialogueNumber)
        {
            case 1:
                GameIntroduction();
                break;
            case 3:
                StartCoroutine(CityPetrolStart());
                break;
        }
    }

    public void GameIntroduction()
    {
        Persisting.Instance.UnlockCursor();
        gameIntroduction_Dialogues.Run();
    }

    public void GameIntroduction_1()
    {
        blackPanel.DOFade(0f, 2f);
    }

    public void GameIntroduction_2()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(gameTitle.DOFade(1f, 1f));
        seq.Append(gameSubtitle.DOFade(1f, 0.35f));
        seq.AppendInterval(2f);
        seq.Append(gameSubtitle.DOFade(0f, 1f));
        seq.Append(gameTitle.DOFade(0f, 0.35f));
        seq.OnComplete(() =>
        {
            cutsceneCamera.DOFieldOfView(5, 5f).OnComplete(() =>
            {
                Persisting.Instance.ShowPlayerInstruction("Walk into the Forensics Lab for Further Instructions");
                DOVirtual.DelayedCall(5f, () =>
                {
                    Persisting.Instance.HidePlayerInstruction();
                    cutsceneCamera.DOFieldOfView(60, 1f).OnComplete(() =>
                    {
                        minimap.SetActive(true);
                        map.SetActive(true);
                        player.gameObject.SetActive(true);
                        cutsceneCamera.gameObject.SetActive(false);

                        Persisting.Instance.dialogueNumber++;
                    });
                });
            });
        });
    }

    public IEnumerator CityPetrolStart()
    {
        forensicsCollider.enabled = false;
        cutsceneCamera.gameObject.SetActive(false);
        player.gameObject.SetActive(true);

        player.transform.position = levelStartPlayerPos.position;
        player.transform.rotation = levelStartPlayerPos.rotation;

        yield return new WaitForSeconds(2f);

        Persisting.Instance.ShowPlayerInstruction("Objective: Petrol the city and wait for further instructions!");
        DOVirtual.DelayedCall(5f, () =>
        {
            Persisting.Instance.HidePlayerInstruction();
        });

        yield return new WaitForSeconds(Random.Range(15, 30));

        cityPetrolStart_Dialogues.Run();
        crimeSceneCollider.enabled = true;

        minimap.SetActive(true);
        map.SetActive(true);

        crimeSceneMapIcon.SetActive(true);
    }
}
