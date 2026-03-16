using System.Collections.Generic;
using UnityEngine;

public class PhotoCamera : MonoBehaviour
{
    public Camera playerCamera;
    public PhotoGalleryUI gallery;

    public int photoWidth = 512;
    public int photoHeight = 512;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            TakePhoto();
        }
    }

    public void TakePhoto()
    {
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
    }
}