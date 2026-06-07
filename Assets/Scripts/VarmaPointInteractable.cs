using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSimpleInteractable))]
public class VarmaPointInteractable : MonoBehaviour
{
    public VarmaPointData myData;
    private PhaseTwoManager gameManager;
    private XRSimpleInteractable xrInteractable;

    void Start()
    {
        gameManager = FindObjectOfType<PhaseTwoManager>();
        xrInteractable = GetComponent<XRSimpleInteractable>();

        // This tells the XR Toolkit: "When the raycast trigger is pulled on this object, run this code."
        xrInteractable.selectEntered.AddListener(TriggerPoint);
    }

    void TriggerPoint(SelectEnterEventArgs args)
    {
        if (gameManager != null && myData != null)
        {
            gameManager.OnPointClicked(myData);
        }
    }
}