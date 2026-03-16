using UnityEngine;
using System;
using DG.Tweening;

public class CameraMoveController : MonoBehaviour
{
    public float defaultMoveDuration = 1.5f;
    public Ease easeType = Ease.InOutSine;

    Tween moveTween;
    Tween rotateTween;

    // Move using Transform
    public void MoveToTransform(Transform target, float duration = -1f, Action OnComplete = null)
    {
        if (duration <= 0)
            duration = defaultMoveDuration;

        StartMove(target.position, target.rotation, duration, OnComplete);
    }

    // Move using position + quaternion
    public void MoveToPositionRotation(Vector3 position, Quaternion rotation, float duration = -1f, Action OnComplete = null)
    {
        if (duration <= 0)
            duration = defaultMoveDuration;

        StartMove(position, rotation, duration, OnComplete);
    }

    // Move using position + euler rotation
    public void MoveToPositionEuler(Vector3 position, Vector3 eulerRotation, float duration = -1f, Action OnComplete = null)
    {
        if (duration <= 0)
            duration = defaultMoveDuration;

        StartMove(position, Quaternion.Euler(eulerRotation), duration, OnComplete);
    }

    void StartMove(Vector3 targetPosition, Quaternion targetRotation, float duration, Action OnComplete)
    {
        StopCameraMovement();

        moveTween = transform.DOMove(targetPosition, duration)
            .SetEase(easeType);

        rotateTween = transform.DORotateQuaternion(targetRotation, duration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                OnComplete?.Invoke();
            });
    }

    public void StopCameraMovement()
    {
        moveTween?.Kill();
        rotateTween?.Kill();
    }
}