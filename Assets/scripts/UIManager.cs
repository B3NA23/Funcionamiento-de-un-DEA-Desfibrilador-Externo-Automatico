using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panelInicio;
    public GameObject panelMenu;
    public GameObject[] otherScreens; // 0..4 -> PanelScreen1..5

    void Start()
    {
        // Asegura estado inicial
        ShowInicio();
    }

    public void ShowInicio()
    {
        panelInicio.SetActive(true);
        panelMenu.SetActive(false);
        SetAllScreensActive(false);
    }

    public void ShowMenu()
    {
        panelInicio.SetActive(false);
        panelMenu.SetActive(true);
        SetAllScreensActive(false);
    }

    public void ShowScreen(int index)
    {
        panelInicio.SetActive(false);
        panelMenu.SetActive(false);
        SetAllScreensActive(false);
        if (index >= 0 && index < otherScreens.Length)
            otherScreens[index].SetActive(true);
    }

    private void SetAllScreensActive(bool value)
    {
        foreach (var p in otherScreens)
            if (p != null)
                p.SetActive(value);
    }
}
