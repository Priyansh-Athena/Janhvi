using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoGalleryUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Transform galleryParent;
    [SerializeField] GameObject photoPrefab;
    [SerializeField] CanvasGroup displayImageCg, gallery;
    [SerializeField] Image displayImage;

    List<Sprite> photos = new List<Sprite>();

    private void Start()
    {
        DisplaySavedPhotos();
    }

    void DisplaySavedPhotos()
    {
        if (Persisting.Instance == null) return;

        List<Texture2D> savedPhotos = Persisting.Instance.capturedPhotos;

        foreach (Texture2D photoTexture in savedPhotos)
        {
            AddPhoto(photoTexture);
        }
    }

    public void AddPhoto(Texture2D photoTexture)
    {
        Sprite sprite = Sprite.Create(
            photoTexture,
            new Rect(0, 0, photoTexture.width, photoTexture.height),
            new Vector2(0.5f, 0.5f)
        );

        photos.Add(sprite);

        GameObject photoObj = Instantiate(photoPrefab, galleryParent);

        Image img = photoObj.GetComponent<Image>();
        Button btn = photoObj.GetComponent<Button>();

        img.sprite = sprite;
        btn.onClick.AddListener(() => {
            Sprite tempSprite = sprite;
            displayImage.sprite = tempSprite;

            displayImageCg.blocksRaycasts = true;
            displayImageCg.DOFade(1f, 0.5f);
        });
    }

    public void CloseDisplayImage()
    {
        displayImageCg.DOFade(0f, 0.5f).OnComplete(() =>
        {
            displayImageCg.blocksRaycasts = false;
        });
    }

    public void ToggleGallery(bool toggle)
    {
        gallery.DOFade((toggle) ? 1f : 0f, 0.5f).OnComplete(() =>
        {
            gallery.blocksRaycasts = toggle;
        });
    }
}