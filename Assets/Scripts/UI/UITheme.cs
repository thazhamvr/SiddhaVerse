using UnityEngine;

[CreateAssetMenu(fileName = "UITheme", menuName = "SiddhaVerse/UI Theme")]
public class UITheme : ScriptableObject
{
    [Header("Surfaces")]
    public Color primaryBackground = new Color32(0x07, 0x11, 0x1F, 0xFF); // #07111F
    public Color secondarySurface = new Color32(0x11, 0x1D, 0x2B, 0xFF); // #111D2B
    public Color glassSurface = new Color32(0x16, 0x26, 0x38, 0xD9); // #162638 (~85% alpha)

    [Header("Accents")]
    public Color xrAccentCyan = new Color32(0x4F, 0xD8, 0xFF, 0xFF); // #4FD8FF (Active/Focus)
    public Color xrAccentTeal = new Color32(0x21, 0xC7, 0xA8, 0xFF); // #21C7A8 (Completed)
    public Color heritageGold = new Color32(0xD7, 0xA8, 0x4B, 0xFF); // #D7A84B (Siddha Only)

    [Header("Typography")]
    public Color primaryText = new Color32(0xF5, 0xF7, 0xFA, 0xFF); // #F5F7FA
    public Color secondaryText = new Color32(0xAA, 0xB7, 0xC5, 0xFF); // #AAB7C5
}