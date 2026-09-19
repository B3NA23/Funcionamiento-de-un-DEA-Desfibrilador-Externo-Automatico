using UnityEngine;
using UnityEngine.UI;

public class ImageSlider : MonoBehaviour
{
    [Header("Componentes UI")]
    public Image imageDisplay;
    public Sprite[] images;

    private int currentIndex = 0;

    void Start()
    {
        if (images.Length > 0 && imageDisplay != null)
        {
            imageDisplay.sprite = images[0];
        }
    }

    public void NextImage()
    {
        if (images.Length == 0) return;
        currentIndex = (currentIndex + 1) % images.Length;
        imageDisplay.sprite = images[currentIndex];
    }

    public void PrevImage()
    {
        if (images.Length == 0) return;
        currentIndex = (currentIndex - 1 + images.Length) % images.Length;
        imageDisplay.sprite = images[currentIndex];
    }
}

