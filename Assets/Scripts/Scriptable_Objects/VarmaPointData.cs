using UnityEngine;

[CreateAssetMenu(fileName = "New Varma Point", menuName = "Siddha/Varma Point Data")]
public class VarmaPointData : ScriptableObject
{
    public string pointName;
    public string location;
    [TextArea(3, 10)]
    public string description;
    public Sprite anatomyImage;
}