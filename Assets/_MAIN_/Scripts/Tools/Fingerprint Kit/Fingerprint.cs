using DG.Tweening;
using UnityEngine;

public class Fingerprint : MonoBehaviour
{
    [SerializeField] CanvasGroup fingerprintCg;

    public void Run()
    {
        fingerprintCg.DOFade(1f, 0.1f);

        Destroy(this, 0.5f);
    }
}
