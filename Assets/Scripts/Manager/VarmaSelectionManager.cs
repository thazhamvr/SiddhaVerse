using UnityEngine;
using System.Collections.Generic;
using TMPro;

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

    private int pendingPointIndex = -1;
    private int pendingLayerIndex = 1; // 1 = Skin, 2 = Muscle/Nerve, 3 = Skeleton

    // Triggered by each individual Varma Point button in the ScrollView (Pass 0 for Point 1, 1 for Point 2, etc.)
    public void StagePoint(int buttonIndex)
    {
        if (buttonIndex >= 0 && buttonIndex < patuPoints.Count)
        {
            pendingPointIndex = buttonIndex;

            if (patuPoints[buttonIndex] != null && patuPoints[buttonIndex].myDataCard != null)
            {
                if (varmamNameText != null)
                {
                    varmamNameText.text = patuPoints[buttonIndex].myDataCard.pointName;
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

    // Direct methods for the 3 Layer Buttons (stages selection only; waits for View button)
    public void StageSkinLayer()
    {
        pendingLayerIndex = 1;
    }

    public void StageMuscleNerveLayer()
    {
        pendingLayerIndex = 2;
    }

    public void StageSkeletonLayer()
    {
        pendingLayerIndex = 3;
    }

    // Triggered by 3D VIEW / VIEW DETAIL Button
    public void CommitView()
    {
        // 1. Set Anatomy Layer visibility based on the staged layer
        if (layerController != null)
        {
            if (pendingLayerIndex == 1) layerController.SetSkinLayer();
            else if (pendingLayerIndex == 2) layerController.SetMuscleLayer();
            else if (pendingLayerIndex == 3) layerController.SetBoneLayer();
        }

        // Diagnostic check to verify list count and selected index
        Debug.Log("CommitView -> patuPoints count: " + (patuPoints != null ? patuPoints.Count : 0) + " | pendingPointIndex: " + pendingPointIndex);

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
}