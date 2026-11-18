using UnityEngine;

public class TopBarUIManager : MonoBehaviour
{
    [Header("Canvases")]
    public GameObject topBarCanvas;
    public GameObject[] otherCanvases; // Drag your other 3 canvases here

    void Start()
    {
        // Ensure only the top bar canvas is active on application startup
        if (topBarCanvas != null) topBarCanvas.SetActive(true);

        foreach (GameObject canvas in otherCanvases)
        {
            if (canvas != null) canvas.SetActive(false);
        }
    }

    // Called by the Varmam button on the Top Bar Canvas via raycast click
    public void ToggleVarmamCanvases(bool state)
    {
        foreach (GameObject canvas in otherCanvases)
        {
            if (canvas != null) canvas.SetActive(state);
        }
    }

    // Alternatively, if it acts as a toggle switch:
    public void ToggleVarmamCanvases()
    {
        foreach (GameObject canvas in otherCanvases)
        {
            if (canvas != null) canvas.SetActive(!canvas.activeSelf);
        }
    }
}