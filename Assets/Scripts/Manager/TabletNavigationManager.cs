using UnityEngine;

public class TabletNavigationManager : MonoBehaviour
{
    [Header("External UI")]
    [Tooltip("Drag your Canvas_Right_Data here")]
    public GameObject rightDataCanvas;

    [Header("Tablet Screens (Canvases)")]
    public GameObject homeScreenCanvas;   // The screen with View Full Body / View Parts
    public GameObject fullBodyCanvas;     // The screen for Full Body view
    public GameObject partsMenuCanvas;    // The screen listing Left Hand, Right Leg, etc.
    public GameObject leftHandCanvas;     // The specific Left Hand screen with 12 points

    void Start()
    {
        // When the game starts, force the tablet to the Home Screen
        ShowHomeScreen();
    }

    // --- BUTTON FUNCTIONS ---

    public void ShowHomeScreen()
    {
        HideAllScreens();
        if (homeScreenCanvas != null) homeScreenCanvas.SetActive(true);
        if (rightDataCanvas != null) rightDataCanvas.SetActive(false); // Hide right data on home
    }

    public void ShowFullBodyView()
    {
        HideAllScreens();
        if (fullBodyCanvas != null) fullBodyCanvas.SetActive(true);

        // As requested: Right Data Canvas must be OFF for Full Body
        if (rightDataCanvas != null) rightDataCanvas.SetActive(false);
    }

    public void ShowPartsMenu()
    {
        HideAllScreens();
        if (partsMenuCanvas != null) partsMenuCanvas.SetActive(true);

        // As requested: Right Data Canvas must be ON for Parts/Varma interaction
        if (rightDataCanvas != null) rightDataCanvas.SetActive(true);
    }

    public void ShowLeftHandView()
    {
        HideAllScreens();
        if (leftHandCanvas != null) leftHandCanvas.SetActive(true);

        // Right Data Canvas stays ON when viewing specific body parts
        if (rightDataCanvas != null) rightDataCanvas.SetActive(true);
    }

    // --- INTERNAL CLEANUP ---
    private void HideAllScreens()
    {
        if (homeScreenCanvas != null) homeScreenCanvas.SetActive(false);
        if (fullBodyCanvas != null) fullBodyCanvas.SetActive(false);
        if (partsMenuCanvas != null) partsMenuCanvas.SetActive(false);
        if (leftHandCanvas != null) leftHandCanvas.SetActive(false);
    }
}