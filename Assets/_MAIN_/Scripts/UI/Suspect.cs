using DG.Tweening;
using UnityEngine;

public class Suspect : MonoBehaviour
{
    [SerializeField] CanvasGroup pic, suspectName, connection, suspicion, alibi, evidence;


    public void ShowPic()
    {
        pic.DOFade(1f, 0.5f);
    }

    public void ShowName()
    {
        suspectName.DOFade(1f, 0.5f);
    }

    public void ShowConnection()
    {
        connection.DOFade(1f, 0.5f);
    }

    public void ShowSuspicion()
    {
        suspicion.DOFade(1f, 0.5f);
    }

    public void ShowAlibi()
    {
        alibi.DOFade(1f, 0.5f);
    }

    public void ShowEvidence()
    {
        evidence.DOFade(1f, 0.5f);
    }
}
