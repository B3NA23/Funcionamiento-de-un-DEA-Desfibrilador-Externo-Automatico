using UnityEngine;

public class ModelFocusController : MonoBehaviour
{
    [Header("Puntos de enfoque")]
    public Transform[] focusTargets; // Puntos a los que el modelo puede mirar
    public float rotationSpeed = 2f;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool focusing = false;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (focusing)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * rotationSpeed
            );
        }
    }

    // Llamado desde los botones
    public void FocusOn(int index)
    {
        if (index >= 0 && index < focusTargets.Length && focusTargets[index] != null)
        {
            Vector3 direction = focusTargets[index].position - transform.position;
            targetRotation = Quaternion.LookRotation(direction);
            focusing = true;
        }
        else
        {
            Debug.LogWarning("Índice de target inválido o sin referencia asignada.");
        }
    }

    // Llamado desde el botón de volver
    public void ResetFocus()
    {
        targetRotation = initialRotation;
        focusing = true;
    }
}
