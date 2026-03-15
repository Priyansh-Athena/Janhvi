using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerInstruction : MonoBehaviour
{
    [SerializeField] CanvasGroup cg;
    [SerializeField] TMP_Text instructionTxt;

    Coroutine currentRoutine;

    public void Show(string instruction)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowCoroutine(instruction));
    }

    IEnumerator ShowCoroutine(string instruction)
    {
        instructionTxt.text = "";

        yield return StartCoroutine(
            DoTweenAnimations.FadeInCanvasGroup(cg, 0.5f)
        );

        yield return StartCoroutine(
            DoTweenAnimations.TypeWriterTMP(instructionTxt, instruction, 0.05f)
        );
    }

    public void Hide()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        StartCoroutine(
            DoTweenAnimations.FadeOutCanvasGroup(cg, 0.5f)
        );
    }
}