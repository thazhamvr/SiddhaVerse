using UnityEngine;

public enum BodyRegion { LeftHand, RightHand, Head, Chest, Legs }

[CreateAssetMenu(fileName = "NewVarmaPoint", menuName = "Varma System/Point Data")]
public class VarmaPointData : ScriptableObject
{
    public string pointName;

    [TextArea(3, 5)]
    public string description;

    [Header("UI Layout (Legacy)")]
    public Vector3 uiOffset = new Vector3(0.2f, 0f, 0f); // <-- This fixes the error!

    [Header("Sequence Logic")]
    public BodyRegion region;
    public int sequenceNumber; // e.g., 1, 2, 3...
}