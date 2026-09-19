using UnityEngine;

public class ModoComoFunciona : MonoBehaviour
{
    public GameObject escenario3D;   // El parent del escenario
    public GameObject panelComoFunciona;  // El panel UI de los pasos

    public void ActivarModo()
    {
        escenario3D.SetActive(false);      // Oculta TODO el escenario
        panelComoFunciona.SetActive(true); // Muestra los pasos
    }

    public void DesactivarModo()
    {
        escenario3D.SetActive(true);       // Vuelve a mostrar el escenario
        panelComoFunciona.SetActive(false);
    }
}
