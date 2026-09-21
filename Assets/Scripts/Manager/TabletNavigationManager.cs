using UnityEngine;

public class TabletNavigationManager : MonoBehaviour
{
    [Header("External UI")]
    [Tooltip("Drag your Canvas_Right_Data here")]
    public GameObject rightDataCanvas;

    [Header("Tablet Screens (Canvases)")]
    public GameObject mainScreenCanvas;   // Screen 1: The starting menu
    public GameObject patuVarmamCanvas;   // Screen 2: The 12 Patu points
    public GameObject totuVarmamCanvas;   // Screen 3: Work in progress

    void Start()
    {
        ShowMainScreen();
    }

    public void ShowMainScreen()
    {
        HideAllScreens();
        if (mainScreenCanvas != null) mainScreenCanvas.SetActive(true);
        if (rightDataCanvas != null) rightDataCanvas.SetActive(false); // Hide right data on home
    }

    public void ShowPatuVarmamScreen()
    {
        HideAllScreens();
        if (patuVarmamCanvas != null) patuVarmamCanvas.SetActive(true);
        if (rightDataCanvas != null) rightDataCanvas.SetActive(true); // Show right data
    }

    public void ShowTotuVarmamScreen()
    {
        HideAllScreens();
        if (totuVarmamCanvas != null) totuVarmamCanvas.SetActive(true);
        if (rightDataCanvas != null) rightDataCanvas.SetActive(true);
    }

    private void HideAllScreens()
    {
        if (mainScreenCanvas != null) mainScreenCanvas.SetActive(false);
        if (patuVarmamCanvas != null) patuVarmamCanvas.SetActive(false);
        if (totuVarmamCanvas != null) totuVarmamCanvas.SetActive(false);
    }
    public void QuitApplication()
    {
        Debug.Log("Quitting App...");

        // This closes the app on your Android/XR headset
        Application.Quit();

        // This forces the Unity Editor's "Play" button to stop so you can test it!
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}