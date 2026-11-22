using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class VarmaSelectionManager : MonoBehaviour
{
    [Header("The 12 Patu Varmam Points")]
    [Tooltip("Drag the 12 point senders here in exact order (Index 0 to 11)")]
    public List<VarmaPointSender> patuPoints;

    [Header("TV Monitor UI Text Reference")]
    [Tooltip("Drag the VARMA NAME Text component here")]
    public TextMeshProUGUI varmamNameText;

    [Header("External Layer Controller")]
    public AnatomyLayerController layerController;

    [Header("Sequential UI Flow Controller")]
    [Tooltip("Drag TV_UI_TriggerZone here so picking a point or layer advances the 3-panel flow")]
    public UITriggerZoneController uiFlowController;

    [Header("One-Time View Button")]
    [Tooltip("Drag the View button GameObject from Canvas_Right_Data here so it disables after first use")]
    public GameObject viewButtonObject;

    [Header("Persistent Selected Button Sprites (Optional)")]
    [Tooltip("Assign the 12 Varmam point button Images in order (0 to 11)")]
    public Image[] pointButtonImages;
    [Tooltip("Assign the 3 Layer button Images in order: 0 = Skin, 1 = Muscle/Nerve, 2 = Skeleton")]
    public Image[] layerButtonImages;
    public Sprite unselectedButtonSprite; // e.g., btn_action_190x70_normal
    public Sprite activeSelectedSprite;   // e.g., btn_action_primary_190x70_normal

    private int pendingPointIndex = -1;
    private int pendingLayerIndex = 1; // 1 = Skin, 2 = Muscle/Nerve, 3 = Skeleton
    private bool hasUsedInitialView = false;

    void Start()
    {
        hasUsedInitialView = false;
        if (viewButtonObject != null)
        {
            viewButtonObject.SetActive(true);
        }
    }

    // Triggered by each individual Varma Point button in the ScrollView (Pass 0 for Point 1, 1 for Point 2, etc.)
    public void StagePoint(int buttonIndex)
    {
        if (buttonIndex >= 0 && buttonIndex < patuPoints.Count)
        {
            pendingPointIndex = buttonIndex;
            HighlightActiveButton(pointButtonImages, buttonIndex);

            if (patuPoints[buttonIndex] != null && patuPoints[buttonIndex].myDataCard != null)
            {
                if (varmamNameText != null)
                {
                    varmamNameText.text = patuPoints[buttonIndex].myDataCard.pointName;
                }

                // Advance sequential UI flow: spawn Anatomy Layer panel
                if (uiFlowController != null)
                {
                    uiFlowController.AdvanceToStep2_LayerSelection();
                }

                // After the initial View button has been used once, update the 3D point & data immediately
                if (hasUsedInitialView)
                {
                    ApplyPointAndLayerSelection();
                }
            }
            else
            {
                Debug.LogError("Missing point sender or data card at index: " + buttonIndex);
            }
        }
        else
        {
            Debug.LogError("Button Index " + buttonIndex + " is out of range.");
        }
    }

    // Direct methods for the 3 Layer Buttons on Canvas_Left_Anatomy
    public void StageSkinLayer()
    {
        pendingLayerIndex = 1;
        HighlightActiveButton(layerButtonImages, 0);
        OnLayerSelected();
    }

    public void StageMuscleNerveLayer()
    {
        pendingLayerIndex = 2;
        HighlightActiveButton(layerButtonImages, 1);
        OnLayerSelected();
    }

    public void StageSkeletonLayer()
    {
        pendingLayerIndex = 3;
        HighlightActiveButton(layerButtonImages, 2);
        OnLayerSelected();
    }

    private void OnLayerSelected()
    {
        if (uiFlowController != null)
        {
            uiFlowController.AdvanceToStep3_DataAndView();
        }

        // After the View button has been used once, selecting a layer immediately updates the model & point
        if (hasUsedInitialView)
        {
            ApplyPointAndLayerSelection();
        }
    }

    // Triggered by the View button on Canvas_Right_Data (used ONCE on the first selection)
    public void CommitView()
    {
        hasUsedInitialView = true;

        // Disable the View button after its first use
        if (viewButtonObject != null)
        {
            viewButtonObject.SetActive(false);
        }

        ApplyPointAndLayerSelection();
    }

    private void ApplyPointAndLayerSelection()
    {
        // 1. Set Anatomy Layer visibility based on the selected layer
        if (layerController != null)
        {
            if (pendingLayerIndex == 1) layerController.SetSkinLayer();
            else if (pendingLayerIndex == 2) layerController.SetMuscleLayer();
            else if (pendingLayerIndex == 3) layerController.SetBoneLayer();
        }

        // 2. Activate selected Varma Point sphere and send data to Canvas_Right_Data
        if (patuPoints == null || patuPoints.Count == 0 || pendingPointIndex == -1) return;

        foreach (VarmaPointSender point in patuPoints)
        {
            if (point != null) point.SetVisible(false);
        }

        VarmaPointSender selectedPoint = patuPoints[pendingPointIndex];
        if (selectedPoint != null)
        {
            selectedPoint.SetVisible(true);
            selectedPoint.StartBlinking();
            selectedPoint.SendData();
        }
    }

    private void HighlightActiveButton(Image[] buttonImages, int activeIndex)
    {
        if (buttonImages == null || unselectedButtonSprite == null || activeSelectedSprite == null) return;

        for (int i = 0; i < buttonImages.Length; i++)
        {
            if (buttonImages[i] != null)
            {
                buttonImages[i].sprite = (i == activeIndex) ? activeSelectedSprite : unselectedButtonSprite;
            }
        }
    }
}