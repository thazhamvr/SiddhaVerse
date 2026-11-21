using UnityEngine;

public class UITriggerZoneController : MonoBehaviour
{
    [Header("3 Side WorldSpace Canvases (Do NOT include Top_Bar_Canvas)")]
    [Tooltip("Drag Canvas_FarLeft_Toggles, Canvas_Left_Anatomy, and Canvas_Right_Data here")]
    public GameObject[] sideUICanvases;

    [Header("Player Detection")]
    [Tooltip("Drag your XR Origin (XR Rig) here")]
    public Transform playerHeadOrRig;

    private bool isVarmamModeActive = false;
    private bool isPlayerInsideZone = false;

    void Start()
    {
        RefreshUIVisibility();
    }

    // Hook this to the "VARMAM" button on Top_Bar_Canvas -> OnClick()
    public void ToggleVarmamMode()
    {
        isVarmamModeActive = !isVarmamModeActive;
        RefreshUIVisibility();
    }

    // Explicit setter used by TabletNavigationManager or TopBarUIManager
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

        return other.transform == playerHeadOrRig || other.transform.IsChildOf(playerHeadOrRig);
    }

    // Shows the 3 side canvases ONLY when Varmam is toggled ON AND Player is in front of the TV
    private void RefreshUIVisibility()
    {
        bool shouldShow = isVarmamModeActive && isPlayerInsideZone;

        if (sideUICanvases == null) return;

        foreach (GameObject panel in sideUICanvases)
        {
            if (panel != null)
            {
                panel.SetActive(shouldShow);
            }
        }
    }
}