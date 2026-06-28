using UnityEngine;
using UnityEngine.InputSystem;

public class TabletToggleManager : MonoBehaviour
{
    [Header("The 3D Tablet Object")]
    public GameObject tabletObject;

    [Header("X Button Input Action")]
    public InputActionReference toggleButtonAction;

    private void OnEnable()
    {
        // Start listening for the button press when the script is active
        if (toggleButtonAction != null)
        {
            toggleButtonAction.action.started += ToggleTablet;
        }
    }

    private void OnDisable()
    {
        // Stop listening when the script is disabled to prevent memory leaks
        if (toggleButtonAction != null)
        {
            toggleButtonAction.action.started -= ToggleTablet;
        }
    }

    void Start()
    {
        // Ensure the tablet starts hidden when the scene loads
        if (tabletObject != null)
        {
            tabletObject.SetActive(false);
        }
    }

    private void ToggleTablet(InputAction.CallbackContext context)
    {
        // Flip the active state: if it's on, turn it off. If it's off, turn it on.
        if (tabletObject != null)
        {
            tabletObject.SetActive(!tabletObject.activeSelf);
        }
    }
}