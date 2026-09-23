using UnityEngine;

public class TabletNavigationManager : MonoBehaviour
{
    [Header("TV Monitor Panels")]
    [Tooltip("Drag Start_Panel here")]
    public GameObject startPanel;

    [Tooltip("Drag Home_Panel here")]
    public GameObject homePanel;

    [Tooltip("Drag Patu_Panel here")]
    public GameObject patuPanel;

    [Tooltip("Drag ViewDetail_Panel here")]
    public GameObject viewDetailPanel;

    [Header("External Side Canvases")]
    [Tooltip("Drag Canvas_Left_Anatomy or Canvas_Right_Data here")]
    public GameObject rightDataCanvas;

    void Start()
    {
        ShowStartPanel();
    }

    // Called by START Button on Start_Panel
    public void ShowStartPanel()
    {
        HideAllPanels();
        if (startPanel != null) startPanel.SetActive(true);
        if (rightDataCanvas != null) rightDataCanvas.SetActive(false);
    }

    // Called by BACK Button or Home Button
    public void ShowHomePanel()
    {
        HideAllPanels();
        if (homePanel != null) homePanel.SetActive(true);
        if (rightDataCanvas != null) rightDataCanvas.SetActive(false);
    }

    // Called by PATU VARMAM Button on Home_Panel
    public void ShowPatuVarmamPanel()
    {
        HideAllPanels();
        if (patuPanel != null) patuPanel.SetActive(true);
        if (rightDataCanvas != null) rightDataCanvas.SetActive(true);
    }

    // Called by VIEW DETAIL Button on Patu_Panel
    public void ShowViewDetailPanel()
    {
        HideAllPanels();
        if (viewDetailPanel != null) viewDetailPanel.SetActive(true);
        if (rightDataCanvas != null) rightDataCanvas.SetActive(true);
    }

    private void HideAllPanels()
    {
        if (startPanel != null) startPanel.SetActive(false);
        if (homePanel != null) homePanel.SetActive(false);
        if (patuPanel != null) patuPanel.SetActive(false);
        if (viewDetailPanel != null) viewDetailPanel.SetActive(false);
    }

    public void QuitApplication()
    {
        Debug.Log("Quitting Application...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}