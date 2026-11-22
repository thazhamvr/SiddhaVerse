using UnityEngine;
using UnityEngine.UI;

public class TabletNavigationManager : MonoBehaviour
{
    [Header("TV Monitor Panels")]
    public GameObject startPanel;    // Start_Panel
    public GameObject uiActivePanel; // UI_Active_panel

    [Header("UI_Active_panel Buttons")]
    public Button paduVarmamButton;   // Padu_Btn
    public Button thoduVarmamButton;  // Thodu_Btn (1) - locked for now

    [Header("Top Bar Canvas (Spawns on Padu Varmam)")]
    [Tooltip("Drag ONLY Top_Bar_Canvas here")]
    public GameObject topBarCanvas;

    [Header("Trigger Zone Controller (Controls the 3 Side Canvases)")]
    [Tooltip("Drag TV_UI_TriggerZone here so the 3 side canvases stay hidden until Varmam is clicked")]
    public UITriggerZoneController triggerZoneController;

    void Start()
    {
        // 1. Hide Top_Bar_Canvas at startup
        if (topBarCanvas != null) topBarCanvas.SetActive(false);

        // 2. Ensure Varmam mode starts OFF so the 3 side canvases stay hidden
        if (triggerZoneController != null)
        {
            triggerZoneController.SetVarmamMode(false);
        }

        // 3. Show Start_Panel and hide UI_Active_panel initially
        if (startPanel != null) startPanel.SetActive(true);
        if (uiActivePanel != null) uiActivePanel.SetActive(false);

        // 4. Lock Thodu Varmam button and keep Padu Varmam unlocked
        if (thoduVarmamButton != null) thoduVarmamButton.interactable = false;
        if (paduVarmamButton != null) paduVarmamButton.interactable = true;
    }

    // Hook to START button inside Start_Panel -> OnClick()
    public void OnStartButtonPressed()
    {
        if (startPanel != null) startPanel.SetActive(false);
        if (uiActivePanel != null) uiActivePanel.SetActive(true);
    }

    // Hook to Padu_Btn inside UI_Active_panel -> OnClick()
    public void OnPaduVarmamPressed()
    {
        // Spawn ONLY Top_Bar_Canvas first; the 3 side panels wait for the Varmam button
        if (topBarCanvas != null)
        {
            topBarCanvas.SetActive(true);
        }
    }

    // Hook to the Back button inside UI_Active_panel -> OnClick()
    public void OnBackButtonPressed()
    {
        // Hide Top_Bar_Canvas and reset Varmam mode so all 3 side canvases close cleanly
        if (topBarCanvas != null) topBarCanvas.SetActive(false);

        if (triggerZoneController != null)
        {
            triggerZoneController.SetVarmamMode(false);
        }

        // Return to Start_Panel
        if (uiActivePanel != null) uiActivePanel.SetActive(false);
        if (startPanel != null) startPanel.SetActive(true);
    }

    // Hook to Exit_Btn (QUIT) inside Start_Panel -> OnClick()
    public void OnQuitButtonPressed()
    {
        Debug.Log("Quitting SiddhaVerse application...");

#if UNITY_EDITOR
        // Stops Play Mode when testing inside the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Exits cleanly on both Meta Quest standalone (.apk) and Windows PC VR (.exe)
        Application.Quit();
#endif
    }
}