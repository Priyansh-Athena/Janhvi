using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Tweezer : MonoBehaviour
{
    public bool isSelected = false, canRun = false;
    
    [SerializeField] string tagToLookFor;
    [SerializeField] string toolName;
    [SerializeField] CanvasGroup cg;
    [SerializeField] PhotoCamera photoCamera;
    [SerializeField] Transform holdPoint;


    GameObject triggeredObj;

    public static UnityAction OnTweezerUsed;


    private void Awake()
    {
        photoCamera = GetComponent<PhotoCamera>();
    }

    private void Update()
    {
        if (isSelected && canRun && Input.GetKeyDown(KeyCode.Q))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        triggeredObj.transform.SetParent(holdPoint);
        triggeredObj.transform.DOLocalMove(Vector3.zero, 1f).OnComplete(() =>
        {
            photoCamera.TakePhoto();
            DOVirtual.DelayedCall(0.25f, () =>
            {
                OnTweezerUsed?.Invoke();
                Destroy(triggeredObj);
            });
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
        if(col.CompareTag(tagToLookFor))
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
    }
}
