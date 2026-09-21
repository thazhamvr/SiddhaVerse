using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class VarmaSelectionManager : MonoBehaviour
{
    [Header("The 12 Patu Varmam Points")]
    [Tooltip("Drag the 12 dummy parent spheres here IN EXACT ORDER (1 to 12)")]
    public List<VarmaPointSender> patuPoints;

    [Header("Tablet UI Text References")]
    [Tooltip("Drag the Text element from the tablet that shows the preview name")]
    public TextMeshProUGUI tabletPointNameText;

    [Header("External Managers")]
    [Tooltip("Drag the object holding your AnatomyLayerController script here")]
    public AnatomyLayerController layerController;

    // Internal memory to hold the user's choices before they press VIEW
    private int pendingPointIndex = -1;
    private int pendingLayerIndex = 1; // Defaults to Skin layer (1)

    // 1. Triggered by the Numbered Buttons (1-12)
    public void StagePoint(int buttonIndex)
    {
        Debug.Log("Button pressed! Staging Index: " + buttonIndex); // Tells us if the button works

        if (buttonIndex >= 0 && buttonIndex < patuPoints.Count)
        {
            pendingPointIndex = buttonIndex;

            if (patuPoints[buttonIndex] == null)
            {
                Debug.LogError("The sphere at index " + buttonIndex + " is missing from the Patu Points list!");
                return;
            }

            if (tabletPointNameText != null)
            {
                // This logs the name it's TRYING to display
                Debug.Log("Trying to display name: " + patuPoints[buttonIndex].myDataCard.pointName);
                tabletPointNameText.text = patuPoints[buttonIndex].myDataCard.pointName;
            }
            else
            {
                Debug.LogError("Tablet Point Name Text is missing in the Inspector!");
            }
        }
        else
        {
            Debug.LogError("Button Index " + buttonIndex + " is out of range. Check your Patu Points list size.");
        }
    }

    // 2. Triggered by the Skin/Muscle/Bone buttons on the Tablet
    public void StageLayer(int layerType)
    {
        // 1 = Skin, 2 = Muscle, 3 = Bone
        pendingLayerIndex = layerType;
    }

    // 3. Triggered by the big "VIEW" Button
    public void CommitView()
    {
        // A. Apply the memorized Anatomy Layer to the Dummy
        if (layerController != null)
        {
            if (pendingLayerIndex == 1) layerController.SetSkinTransparent();
            else if (pendingLayerIndex == 2) layerController.SetMuscleTransparent();
            else if (pendingLayerIndex == 3) layerController.SetBoneTransparent();
        }

        // B. Apply the memorized Varma Point to the Dummy
        if (patuPoints == null || patuPoints.Count == 0 || pendingPointIndex == -1) return;

        // Hide everything first
        foreach (VarmaPointSender point in patuPoints)
        {
            if (point != null) point.SetVisible(false);
        }

        // Turn on the exact point they confirmed
        VarmaPointSender selectedPoint = patuPoints[pendingPointIndex];
        if (selectedPoint != null)
        {
            selectedPoint.SetVisible(true);
            selectedPoint.StartBlinking();
            selectedPoint.SendData(); // Triggers AnatomyDisplayManager to show description/location on Right Canvas
        }
    }
}