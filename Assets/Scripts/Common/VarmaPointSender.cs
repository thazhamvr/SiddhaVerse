using UnityEngine;

public class VarmaPointSender : MonoBehaviour
{
    public VarmaPointData myDataCard;
    private AnatomyDisplayManager uiManager;

    void Start()
    {
        // Automatically finds the UI Manager when the game starts
        uiManager = FindObjectOfType<AnatomyDisplayManager>();
    }

    // The VR Interactor will trigger this
    public void SendData()
    {
        if (uiManager != null && myDataCard != null)
        {
            uiManager.DisplayVarmaData(myDataCard);
        }
    }
}