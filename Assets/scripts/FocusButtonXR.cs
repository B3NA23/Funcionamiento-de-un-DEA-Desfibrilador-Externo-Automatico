using UnityEngine;

public class FocusButtonXR : MonoBehaviour
{
    public ModelFocusController focusController;
    public int focusIndex;

    public void OnFocus()
    {
        focusController.FocusOn(focusIndex);
    }
}
