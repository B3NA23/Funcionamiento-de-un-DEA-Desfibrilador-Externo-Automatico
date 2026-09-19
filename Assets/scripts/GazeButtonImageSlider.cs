using UnityEngine;

public class GazeButtonImageSlider : MonoBehaviour
{
    public ImageSlider imageSlider;
    public bool next = true; // Si es true → avanza, si es false → retrocede

    // Se llama cuando el usuario mira y selecciona con Cardboard (ya lo tienes conectado)
    public void OnPointerClickXR()
    {
        if (imageSlider == null) return;

        if (next)
            imageSlider.NextImage();
        else
            imageSlider.PrevImage();
    }
}

