using UnityEngine;

[CreateAssetMenu(fileName = "New Varma Point", menuName = "Siddha/Varma Point Data")]
public class VarmaPointData : ScriptableObject
{
    public string pointName;

    [TextArea(3, 10)]
    public string description;

    public Sprite anatomyImage;

    // --- ADD THESE TWO LINES FOR THE TABLET FILTERING ---
    public enum BodyRegion { Head, RightArm, LeftArm, Torso, LeftLeg, RightLeg }
    public BodyRegion region;
}