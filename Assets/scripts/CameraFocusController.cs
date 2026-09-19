using UnityEngine;
using UnityEngine.UI;

public class CameraFocusController : MonoBehaviour
{
    [Header("Puntos de enfoque")]
    public Transform[] focusTargets;
    public GameObject[] backButtons; // 👈 Un botón por cada target
    public float moveSpeed = 2f;
    public float stopDistance = 0.05f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private bool focusing = false;
    private int currentTargetIndex = -1;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        targetPosition = initialPosition;
        targetRotation = initialRotation;

        // Oculta todos los botones de volver al inicio
        foreach (var btn in backButtons)
        {
            if (btn != null)
                btn.SetActive(false);
        }
    }

    void Update()
    {
        if (focusing)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.position, targetPosition) < stopDistance)
            {
                focusing = false;

                // Muestra el botón de volver correspondiente al target actual
                if (currentTargetIndex >= 0 && currentTargetIndex < backButtons.Length && backButtons[currentTargetIndex] != null)
                    backButtons[currentTargetIndex].SetActive(true);
            }
        }
    }

    public void FocusOn(int index)
    {
        if (index >= 0 && index < focusTargets.Length && focusTargets[index] != null)
        {
            // Oculta todos los botones antes de cambiar
            foreach (var btn in backButtons)
            {
                if (btn != null) btn.SetActive(false);
            }

            Vector3 direction = (focusTargets[index].position - transform.position).normalized;
            float zoomDistance = 0.5f;

            targetPosition = focusTargets[index].position - direction * zoomDistance;
            targetRotation = Quaternion.LookRotation(focusTargets[index].position - targetPosition);

            focusing = true;
            currentTargetIndex = index;
        }
    }

    public void ResetFocus()
    {
        targetPosition = initialPosition;
        targetRotation = initialRotation;
        focusing = true;

        // Oculta todos los botones
        foreach (var btn in backButtons)
        {
            if (btn != null) btn.SetActive(false);
        }

        currentTargetIndex = -1;
    }
}
