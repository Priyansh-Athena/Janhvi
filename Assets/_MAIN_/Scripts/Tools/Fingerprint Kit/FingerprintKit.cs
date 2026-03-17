using UnityEngine;
using DG.Tweening;

public class FingerprintKit : MonoBehaviour
{
    public bool isSelected = false, canRun = false;
    
    [SerializeField] string tagToLookFor;
    [SerializeField] string toolName; 
    [SerializeField] CanvasGroup cg;
    [SerializeField] PhotoCamera photoCamera;


    GameObject triggeredObj;


    private void Awake()
    {
        photoCamera = GetComponent<PhotoCamera>();
    }

    private void Update()
    {
        if(isSelected && canRun && Input.GetKeyDown(KeyCode.Q))
        {
            triggeredObj?.GetComponent<Fingerprint>()?.Run();

            DOVirtual.DelayedCall(0.25f, () =>
            {
                photoCamera.TakePhoto();
            });
        }
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
        if(col.CompareTag(tagToLookFor))
        {
            triggeredObj = to;
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
    }
}
