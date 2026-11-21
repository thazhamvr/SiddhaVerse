using UnityEngine;

public class UITriggerZoneController : MonoBehaviour
{
    [Header("3 Side WorldSpace Canvases (Do NOT include Top_Bar_Canvas)")]
    public GameObject farLeftPointsCanvas;   // Canvas_FarLeft_Toggles
    public GameObject leftAnatomyCanvas;     // Canvas_Left_Anatomy
    public GameObject rightDataCanvas;       // Canvas_Right_Data

    [Header("Player Detection")]
    [Tooltip("Drag your XR Origin (XR Rig) here")]
    public Transform playerHeadOrRig;

    private bool isVarmamModeActive = false;
    private bool isPlayerInsideZone = false;

    // 0 = Not started, 1 = Point selection visible, 2 = Layer selection visible, 3 = All 3 visible
    private int currentFlowStep = 0;

    void Start()
    {
        RefreshUIVisibility();
    }

    // Hook this to the "VARMAM" button on Top_Bar_Canvas -> OnClick()
    public void ToggleVarmamMode()
    {
        SetVarmamMode(!isVarmamModeActive);
    }

    // Called by TabletNavigationManager or Top_Bar_Canvas
    public void SetVarmamMode(bool active)
    {
        isVarmamModeActive = active;

        if (isVarmamModeActive && currentFlowStep == 0)
        {
            StartStep1_PointSelection();
        }
        else if (!isVarmamModeActive)
        {
            currentFlowStep = 0;
        }

        RefreshUIVisibility();
    }

    // STEP 1: Reveal Canvas_FarLeft_Toggles in its exact original place
    public void StartStep1_PointSelection()
    {
        currentFlowStep = 1;
        RefreshUIVisibility();
    }

    // STEP 2: Reveal Canvas_Left_Anatomy when a Varmam point is selected
    public void AdvanceToStep2_LayerSelection()
    {
        if (!isVarmamModeActive) return;

        if (currentFlowStep == 1)
        {
            currentFlowStep = 2;
            RefreshUIVisibility();
        }
    }

    // STEP 3: Reveal Canvas_Right_Data when an Anatomy Layer is selected
    public void AdvanceToStep3_DataAndView()
    {
        if (!isVarmamModeActive) return;

        if (currentFlowStep == 2)
        {
            currentFlowStep = 3;
            RefreshUIVisibility();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayerCollider(other))
        {
            isPlayerInsideZone = true;
            RefreshUIVisibility();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayerCollider(other))
        {
            isPlayerInsideZone = false;
            RefreshUIVisibility();
        }
    }

    private bool IsPlayerCollider(Collider other)
    {
        if (playerHeadOrRig == null)
            return other.CompareTag("Player") || other.CompareTag("MainCamera");

        return other.transform == playerHeadOrRig || other.transform.IsChildOf(playerHeadOrRig);
    }

    private void RefreshUIVisibility()
    {
        bool canShowAny = isVarmamModeActive && isPlayerInsideZone && currentFlowStep > 0;

        if (farLeftPointsCanvas != null)
            farLeftPointsCanvas.SetActive(canShowAny && currentFlowStep >= 1);

        if (leftAnatomyCanvas != null)
            leftAnatomyCanvas.SetActive(canShowAny && currentFlowStep >= 2);

        if (rightDataCanvas != null)
            rightDataCanvas.SetActive(canShowAny && currentFlowStep >= 3);
    }
}