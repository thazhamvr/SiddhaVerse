using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSimpleInteractable))]
public class PlatformTurntable : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("1 = Matches your wrist perfectly. 2 = Spins twice as fast as your hand.")]
    public float rotationMultiplier = 2.0f;

    private XRSimpleInteractable interactable;
    private IXRSelectInteractor currentInteractor;
    private float previousControllerYaw;

    void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();

        interactable.selectEntered.AddListener(OnSelectEnter);
        interactable.selectExited.AddListener(OnSelectExit);
    }

    void OnSelectEnter(SelectEnterEventArgs args)
    {
        currentInteractor = args.interactorObject;

        // Record the starting horizontal rotation (Yaw) of the controller
        previousControllerYaw = currentInteractor.transform.eulerAngles.y;
    }

    void OnSelectExit(SelectExitEventArgs args)
    {
        currentInteractor = null;
    }

    void Update()
    {
        if (currentInteractor != null)
        {
            // Get the current wrist rotation
            float currentYaw = currentInteractor.transform.eulerAngles.y;

            // Mathf.DeltaAngle safely calculates the difference, even if the rotation wraps past 360 degrees
            float yawDifference = Mathf.DeltaAngle(previousControllerYaw, currentYaw);

            // Spin the platform around its center Y axis!
            transform.Rotate(0, yawDifference * rotationMultiplier, 0, Space.World);

            // Update for the next frame
            previousControllerYaw = currentYaw;
        }
    }
}