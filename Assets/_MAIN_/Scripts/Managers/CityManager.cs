using DG.Tweening;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    [SerializeField] CameraMoveController moveCam;
    [SerializeField] FirstPersonController player;

    [Space(10), Header("1. GAME INTRODUCTION")]
    [SerializeField] DialogueSequenceRunner gameIntroduction_Dialogues;
    [SerializeField] Transform gameIntroduction_Pos_1;
    [SerializeField] CanvasGroup blackPanel, gameTitle, gameSubtitle;


    private void Start()
    {
        gameIntroduction_Dialogues.Run();
    }

    public void GameIntroduction_1()
    {
        blackPanel.DOFade(0f, 2f).OnComplete(() =>
        {
            moveCam.MoveToTransform(gameIntroduction_Pos_1, 10f);
        });
    }

    public void GameIntroduction_2()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(gameTitle.DOFade(1f, 1f));
        seq.Append(gameSubtitle.DOFade(1f, 0.35f));
        seq.AppendInterval(3f);
        seq.Append(blackPanel.DOFade(1f, 1f));
        seq.OnComplete(() =>
        {
            Persisting.Instance.LoadScene("Lab");
        });
    }
}
