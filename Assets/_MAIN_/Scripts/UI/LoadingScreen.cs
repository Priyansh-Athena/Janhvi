using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [Header("References")]
    public CanvasGroup panelGroup;
    public Image loadingImage;
    public TMP_Text waitText;
    public TMP_Text factText;

    [Header("Settings")]
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;
    public float imagePulseScale = 1.05f;
    public float imagePulseDuration = 1.5f;
    public float factChangeInterval = 6f;
    public float waitTextInterval = 0.5f; // time between dot updates

    private bool isAnimating = false;
    private Coroutine factRoutine;
    private Coroutine waitTextRoutine;
    private List<string> forensicFacts = new List<string>();

    void Start()
    {
        panelGroup.alpha = 0f;
        waitText.alpha = 0f;
        factText.alpha = 0f;
        loadingImage.transform.localScale = Vector3.one;
        LoadSpaceFacts();
    }

    public void StartLoading(string sceneName)
    {
        StartCoroutine(LoadingSequence(sceneName));
    }

    private IEnumerator LoadingSequence(string sceneName)
    {
        ShowLoadingScreen();

        yield return new WaitForSeconds(5f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        asyncLoad.allowSceneActivation = true;
        HideLoadingScreen();
        yield return new WaitForSeconds(fadeOutDuration);
    }

    public void ShowLoadingScreen()
    {
        panelGroup.blocksRaycasts = true;
        panelGroup.DOFade(1f, fadeInDuration);

        waitText.text = "Please wait";
        waitText.DOFade(1f, fadeInDuration);
        factText.DOFade(1f, fadeInDuration);

        // Animate loading image — rotate clockwise continuously
        loadingImage.transform.DORotate(new Vector3(0, 0, -360f), 3f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        // Pulse the image for a bit of life
        loadingImage.transform.DOScale(imagePulseScale, imagePulseDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

        if (!isAnimating)
        {
            factRoutine = StartCoroutine(ChangeFactRoutine());
            waitTextRoutine = StartCoroutine(AnimateWaitText());
        }
    }

    public void HideLoadingScreen()
    {
        if (factRoutine != null) StopCoroutine(factRoutine);
        if (waitTextRoutine != null) StopCoroutine(waitTextRoutine);
        isAnimating = false;

        panelGroup.DOFade(0f, fadeOutDuration).OnComplete(() =>
        {
            panelGroup.blocksRaycasts = false;
            loadingImage.transform.DOKill();
        });
    }

    private IEnumerator AnimateWaitText()
    {
        isAnimating = true;
        string baseText = "Please wait";
        int dotCount = 0;

        while (isAnimating)
        {
            waitText.text = baseText + new string('.', dotCount);
            dotCount = (dotCount + 1) % 4; // cycles 0–3 dots
            yield return new WaitForSeconds(waitTextInterval);
        }
    }

    public void ChangeRandomFact()
    {
        if (forensicFacts.Count == 0) return;

        string newFact = forensicFacts[Random.Range(0, forensicFacts.Count)];

        factText.DOFade(0f, 0.4f).OnComplete(() =>
        {
            factText.text = newFact;
            factText.DOFade(1f, 0.4f);
        });
    }

    private IEnumerator ChangeFactRoutine()
    {
        isAnimating = true;
        ChangeRandomFact();

        while (isAnimating)
        {
            yield return new WaitForSeconds(factChangeInterval);
            ChangeRandomFact();
        }
    }

    private void LoadSpaceFacts()
    {
        // 100+ astrophysics and space facts
        forensicFacts.AddRange(new string[]
{
    "Fingerprints are unique — no two people have identical prints, not even identical twins.",
    "Forensic science uses biology, chemistry, and physics to solve crimes.",
    "Even a tiny fiber can link a suspect to a crime scene.",
    "Latent fingerprints are invisible and require special techniques to reveal.",
    "DNA evidence can identify a person with extremely high accuracy.",
    "Blood spatter patterns can reveal how a crime occurred.",
    "Forensic investigators must document the scene before collecting evidence.",
    "Cross-contamination of evidence can ruin an investigation.",
    "Tool marks can be matched to specific tools used in a crime.",
    "Trace evidence includes hair, fibers, dust, and glass fragments.",
    "The chain of custody ensures evidence is handled properly.",
    "Footprints can reveal a suspect’s movement and weight.",
    "Forensic toxicology helps detect poisons and drugs in the body.",
    "Even a single hair can contain DNA evidence.",
    "Fingerprints are formed before birth and remain unchanged for life.",
    "Crime scene photography preserves evidence for later analysis.",
    "Forensic analysis helps reconstruct events step by step.",
    "Gunshot residue can indicate if someone fired a weapon.",
    "Blood type can narrow down suspects.",
    "Digital forensics investigates data from phones and computers.",
    "Forensic entomology uses insects to estimate time of death.",
    "Glass fragments can show the direction of impact.",
    "Every contact leaves a trace — known as Locard’s Exchange Principle.",
    "Investigators must wear gloves to avoid contaminating evidence.",
    "Different surfaces require different techniques to collect evidence.",
    "Fingerprints are often found on smooth surfaces like glass and metal.",
    "Fibers can transfer between people through simple contact.",
    "Forensic labs analyze evidence collected from crime scenes.",
    "DNA profiling compares genetic material between samples.",
    "Even small mistakes at a crime scene can lead to wrong conclusions.",
    "Forensic science plays a key role in modern justice systems.",
    "Evidence must be carefully labeled and stored.",
    "Crime scene sketches help document spatial relationships.",
    "Forensic experts often testify in court.",
    "Time of death can be estimated using body temperature and rigor mortis.",
    "Forensic anthropology studies human bones to identify victims.",
    "Forensic evidence must be collected in a specific order.",
    "Investigators use UV light to detect hidden evidence.",
    "Some chemicals can reveal blood stains invisible to the eye.",
    "Forensic science combines observation, logic, and scientific methods.",
    "The smallest clue can solve the biggest mystery."
});
    }
}
