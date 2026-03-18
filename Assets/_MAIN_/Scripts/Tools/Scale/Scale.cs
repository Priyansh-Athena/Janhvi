using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Scale : MonoBehaviour
{
    public bool isSelected = false, canRun = false;

    [SerializeField] string tagToLookFor;
    [SerializeField] string toolName;
    [SerializeField] CanvasGroup cg, scaleMesaurementCg;
    [SerializeField] PhotoCamera photoCamera;
    [SerializeField] TMP_Text scaleTxt;

    GameObject triggeredObj;

    Tween rollTween;
    Tween fadeTween;
    Tween hideTween;

    public static UnityAction OnScaleUsed;

    private void Awake()
    {
        photoCamera = GetComponent<PhotoCamera>();
    }

    private void Update()
    {
        if (isSelected && canRun && Input.GetKeyDown(KeyCode.Q))
        {
            // Prevent multiple runs
            if (rollTween != null && rollTween.IsActive()) return;

            Measure();
        }
    }

    void Measure()
    {
        fadeTween?.Kill();

        fadeTween = scaleMesaurementCg.DOFade(1f, 0.5f).OnComplete(() =>
        {
            RollNumber(scaleTxt, 1f, 50f, 1f);
        });
    }

    public void OnToolSelected(ToolItem tool)
    {
        if (tool == null)
        {
            isSelected = false;
            cg?.DOFade(0f, 0.5f);
            return;
        }

        if (tool.Name == toolName)
        {
            isSelected = true;
            cg?.DOFade(1f, 0.5f);
        }
        else
        {
            isSelected = false;
            cg?.DOFade(0f, 0.5f);
        }
    }

    public void TriggerEntered(GameObject from, GameObject to, Collider col)
    {
        if (col.CompareTag(tagToLookFor))
        {
            triggeredObj = to;
            canRun = true;
        }
        else
        {
            canRun = false;
        }
    }

    public void TriggerStay(GameObject from, GameObject to, Collider col)
    {
        if (col.CompareTag(tagToLookFor))
        {
            canRun = true;
        }
        else
        {
            canRun = false;
        }
    }

    public void TriggerExited(GameObject from, GameObject to, Collider col)
    {
        canRun = false;
        CancelMeasurement(); // 🔥 stop everything
    }

    void CancelMeasurement()
    {
        rollTween?.Kill();
        fadeTween?.Kill();
        hideTween?.Kill();

        scaleMesaurementCg.alpha = 0f;
        scaleTxt.text = "";
    }

    public void RollNumber(TMP_Text text, float min, float max, float duration = 1f, int decimals = 2)
    {
        rollTween?.Kill();

        float currentValue = 0f;

        rollTween = DOTween.To(
            () => currentValue,
            x =>
            {
                currentValue = x;
                text.text = currentValue.ToString($"F{decimals}") + " cms";
            },
            max,
            duration
        )
        .SetEase(Ease.Linear)
        .OnUpdate(() =>
        {
            float randomValue = Random.Range(min, max);
            text.text = randomValue.ToString($"F{decimals}") + " cms";
        })
        .OnComplete(() =>
        {
            float finalValue = Random.Range(min, max);
            text.text = finalValue.ToString($"F{decimals}") + " cms";

            photoCamera.TakePhoto();
            OnScaleUsed?.Invoke();

            hideTween?.Kill();
            hideTween = DOVirtual.DelayedCall(1f, () =>
            {
                scaleMesaurementCg.DOFade(0f, 0.5f);

            });
        });
    }
}