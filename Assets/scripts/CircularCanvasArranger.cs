using UnityEngine;

public class CircularCanvasArranger : MonoBehaviour
{
    [Header("Configuración")]
    public Transform playerCamera;
    public Canvas[] stepCanvases;
    public float radius = 3f;
    public float heightOffset = 0f;
    public float rotationOffset = 0f; // ← nuevo campo para rotar el círculo

    void Start()
    {
        if (playerCamera == null || stepCanvases.Length == 0) return;

        float angleStep = 360f / stepCanvases.Length;

        for (int i = 0; i < stepCanvases.Length; i++)
        {
            // Aplica el offset de rotación
            float angle = (i * angleStep + rotationOffset) * Mathf.Deg2Rad;

            Vector3 pos = playerCamera.position + new Vector3(Mathf.Sin(angle), heightOffset, Mathf.Cos(angle)) * radius;
            stepCanvases[i].transform.position = pos;

            stepCanvases[i].transform.LookAt(playerCamera);
            stepCanvases[i].transform.Rotate(0, 180, 0);
        }
    }
}
