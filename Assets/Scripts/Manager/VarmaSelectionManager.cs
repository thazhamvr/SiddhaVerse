using UnityEngine;
using System.Collections.Generic;

public class VarmaSelectionManager : MonoBehaviour
{
    [Header("Drag your 3D Dummy Spheres Here")]
    public List<VarmaPointSender> allLeftHandPoints;

    public void OnVarmaButtonPressed(VarmaPointSender clickedSphere)
    {
        // 1. Hide ALL spheres so the dummy is completely clear
        foreach (VarmaPointSender sphere in allLeftHandPoints)
        {
            if (sphere != null)
            {
                sphere.SetVisible(false);
            }
        }

        // 2. Activate ONLY the exact sphere you poked
        if (clickedSphere != null)
        {
            clickedSphere.SetVisible(true);  // Make it appear
            clickedSphere.StartBlinking();   // Make it blink
            clickedSphere.SendData();        // Send data to the UI canvas
        }
    }
}