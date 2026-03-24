using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PhotoCamera : MonoBehaviour
{
    public bool isSelected = false;

    [Space(10)]
    public string toolName;
    public Camera playerCamera;
    public PhotoGalleryUI gallery;
    [SerializeField] CanvasGroup cameraHoldingCg, capture;

    public int photoWidth = 512;
    public int photoHeight = 512;

    public static UnityAction OnPictureClicked;


    private void Update()
    {
        if(isSelected && Input.GetKeyDown(KeyCode.Q))
        {
            TakePhoto();
        }
    }

    public void TakePhoto()
    {
        Persisting.Instance.PlayCameraShutter();
        capture.DOFade(1f, 0.1f).OnComplete(() =>
        {
            capture.DOFade(0f, 0.1f);
        });

        RenderTexture rt = new RenderTexture(photoWidth, photoHeight, 24);

        playerCamera.targetTexture = rt;

        Texture2D photo = new Texture2D(photoWidth, photoHeight, TextureFormat.RGB24, false);

        playerCamera.Render();

        RenderTexture.active = rt;
        photo.ReadPixels(new Rect(0, 0, photoWidth, photoHeight), 0, 0);
        photo.Apply();

        playerCamera.targetTexture = null;
        RenderTexture.active = null;

        Destroy(rt);
        gallery.AddPhoto(photo);
        Persisting.Instance.capturedPhotos.Add(photo);

        OnPictureClicked?.Invoke();
    }

    public void OnToolSelected(ToolItem tool)
    {
        if(tool == null)
        {
            isSelected = false;
            cameraHoldingCg.DOFade(0f, 0.5f);
            return;
        }

        if(tool.Name == toolName)
        {
            isSelected = true;
            cameraHoldingCg.DOFade(1f, 0.5f);
        }
        else
        {
            isSelected = false;
            cameraHoldingCg.DOFade(0f, 0.5f);
        }
    }
}