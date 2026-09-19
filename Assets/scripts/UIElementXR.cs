using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIelementXR : MonoBehaviour
{
    public UnityEvent OnXRPointerEnter;
    public UnityEvent OnXRPointerExit;
    public UnityEvent OnXRPointerClick; // 👈 AGREGA ESTA LÍNEA

    private Camera xRCamera;

    // Start is called before the first frame update
    void Start()
    {
        // Espera a que CameraPointerManager esté listo
        StartCoroutine(WaitForCameraPointerManager());
    }

    private IEnumerator WaitForCameraPointerManager()
    {
        // Espera hasta que el singleton exista
        while (CameraPointerManager.Instance == null)
            yield return null;

        // Ahora sí, asigna la cámara
        xRCamera = CameraPointerManager.Instance.GetComponent<Camera>();

        if (xRCamera == null)
        {
            Debug.LogWarning("⚠️ No se encontró cámara en CameraPointerManager, usando Camera.main");
            xRCamera = Camera.main;
        }

        Debug.Log("✅ Cámara XR inicializada correctamente en UIElementXR");
    }


    public void OnPointerClickXR()
    {
        OnXRPointerClick?.Invoke(); // 👈 AGREGA ESTA LÍNEA para que el evento se dispare

        PointerEventData pointerEvent = PlacePointer();
        ExecuteEvents.Execute(this.gameObject, pointerEvent, ExecuteEvents.pointerClickHandler);
    }

    public void OnPointerEnterXR()
    {
        GazeManager.Instance.SetUpGaze(1.5f);
        OnXRPointerEnter?.Invoke();

        PointerEventData pointerEvent = PlacePointer();
        ExecuteEvents.Execute(this.gameObject, pointerEvent, ExecuteEvents.pointerDownHandler);
    }

    public void OnPointerExitXR()
    {
        GazeManager.Instance.SetUpGaze(2.5f);
        OnXRPointerExit?.Invoke();

        PointerEventData pointerEvent = PlacePointer();
        ExecuteEvents.Execute(this.gameObject, pointerEvent, ExecuteEvents.pointerUpHandler);
    }

    private PointerEventData PlacePointer()
    {
        if (xRCamera == null)
        {
            // Intenta recuperar automáticamente la cámara
            if (CameraPointerManager.Instance != null)
                xRCamera = CameraPointerManager.Instance.GetComponent<Camera>();

            // Fallback: usa la cámara principal
            if (xRCamera == null)
                xRCamera = Camera.main;

            // Si sigue nula, muestra advertencia
            if (xRCamera == null)
            {
                Debug.LogError("❌ No se encontró cámara para UIElementXR. Asegúrate de asignar Camera.main o CameraPointerManager.");
                return null;
            }
        }

        // Si el Manager aún no tiene punto de impacto, evita null
        Vector3 hitPos = CameraPointerManager.Instance != null ?
            CameraPointerManager.Instance.hitPoint :
            xRCamera.transform.forward * 2f;

        Vector3 screenPos = xRCamera.WorldToScreenPoint(hitPos);
        var pointer = new PointerEventData(EventSystem.current);
        pointer.position = new Vector2(screenPos.x, screenPos.y);
        return pointer;
    }

}
