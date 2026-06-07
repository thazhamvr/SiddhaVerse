using UnityEngine;
using TMPro;

public class PhaseTwoManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI feedbackText;

    [Header("Visual Feedback")]
    public Transform targetPinpoint; // The floating arrow/ring

    [Header("Game State")]
    public BodyRegion activeRegion = BodyRegion.LeftHand;
    public int currentSequenceTarget = 1;

    private VarmaPointInteractable[] allPoints;

    void Start()
    {
        feedbackText.text = "Find Point 1 on the " + activeRegion.ToString();
        titleText.text = "???";
        descriptionText.text = "";

        // Find all spheres in the scene
        allPoints = FindObjectsOfType<VarmaPointInteractable>();
        UpdatePinpointPosition();
    }

    public void OnPointClicked(VarmaPointData clickedData)
    {
        if (clickedData.region != activeRegion)
        {
            feedbackText.text = "Focus on the " + activeRegion.ToString() + "!";
            return;
        }

        if (clickedData.sequenceNumber == currentSequenceTarget)
        {
            titleText.text = clickedData.pointName;
            descriptionText.text = clickedData.description;
            feedbackText.text = "Correct! Now find point " + (currentSequenceTarget + 1);

            currentSequenceTarget++;
            UpdatePinpointPosition(); // Move the marker to the next point!
        }
        else if (clickedData.sequenceNumber > currentSequenceTarget)
        {
            feedbackText.text = "Locked! You must find point " + currentSequenceTarget + " first.";
        }
        else
        {
            feedbackText.text = "Already unlocked. Keep going!";
        }
    }

    // This function finds the next sphere and moves the marker above it
    void UpdatePinpointPosition()
    {
        if (targetPinpoint == null) return;

        foreach (VarmaPointInteractable point in allPoints)
        {
            if (point.myData != null && point.myData.region == activeRegion && point.myData.sequenceNumber == currentSequenceTarget)
            {
                // Move the pinpoint slightly above the target sphere
                targetPinpoint.position = point.transform.position + new Vector3(0, 0.1f, 0);
                targetPinpoint.gameObject.SetActive(true);
                return;
            }
        }

        // If no more points are found, the sequence is complete!
        targetPinpoint.gameObject.SetActive(false);
        feedbackText.text = "Region Complete!";
    }
}