using UnityEngine;

public class UITriggerZoneController : MonoBehaviour
{
    [Header("WorldSpace UI Panels to Toggle")]
    [Tooltip("Drag Canvas_FarLeft_Toggles, Canvas_Left_Anatomy, Canvas_Right_Data, etc. here")]
    public GameObject[] worldSpaceUIPanels;

    [Header("Player Detection")]
    [Tooltip("Drag your XR Origin (XR Rig) or Main Camera here so the trigger detects the player")]
    public Transform playerHeadOrRig;

    [Header("Initial States")]
    [Tooltip("Set true if Varmam mode starts active, or false if the user must press the Varmam button first")]
    public bool isVarmamModeActive = false;

    private bool isPlayerInsideZone = false;

    void Start()
    {
        RefreshUIVisibility();
    }

    // Hook this to your Top_Bar_Canvas "VARMAM" button OnClick() (toggles ON/OFF)
    public void ToggleVarmamMode()
    {
        isVarmamModeActive = !isVarmamModeActive;
        RefreshUIVisibility();
    }

    // Optional: Use these if your TopBarUIManager explicitly opens/closes Varmam mode
    public void SetVarmamMode(bool active)
    {
        isVarmamModeActive = active;
        RefreshUIVisibility();
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

        // Matches if the collider belongs to XR Origin, Camera Offset, or Main Camera
        return other.transform == playerHeadOrRig || other.transform.IsChildOf(playerHeadOrRig);
    }

    // Master Rule: UI is ONLY visible when Varmam Mode is ON AND Player is inside the TV zone
    private void RefreshUIVisibility()
    {
        bool shouldShow = isVarmamModeActive && isPlayerInsideZone;

        if (worldSpaceUIPanels == null) return;

        foreach (GameObject panel in worldSpaceUIPanels)
        {
            if (panel != null)
            {
                panel.SetActive(shouldShow);
            }
        }
    }
}