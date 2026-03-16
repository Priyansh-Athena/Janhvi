using DG.Tweening;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    [SerializeField] CameraMoveController moveCam;
    [SerializeField] FirstPersonController player;
    [SerializeField] Camera cutsceneCamera;
    [SerializeField] GameObject minimap, map;

    [Space(10), Header("1. GAME INTRODUCTION")]
    [SerializeField] DialogueSequenceRunner gameIntroduction_Dialogues;
    [SerializeField] Transform gameIntroduction_Pos_1;
    [SerializeField] CanvasGroup blackPanel, gameTitle, gameSubtitle;


    private void Start()
    {
        GameIntroduction();
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
                    });
                });
            });
        });
    }
}
