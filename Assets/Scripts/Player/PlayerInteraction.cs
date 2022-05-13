using UnityEngine;

/// <summary>
/// Component responsible for handling the player interacting with objects.
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform carryHoldTransform;

    [SerializeField] private Carryable currentlyHeldObject;
    [SerializeField] private float interactDistance = 1.5f;

    [SerializeField] private LayerMask interactLayerMask;

    private SwitchableController switchController;

    private void Start()
    {
        switchController = GetComponent<SwitchableController>();
    }

    private void Update()
    {
        // if E has been pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // check if an object is held
            if (currentlyHeldObject == null)
            {
                // check for something to interact with
                CheckForInteractions();
                return;
            }

            // check for deposit spot
            if (CheckForObject(interactDistance, out RaycastHit hitInfo))
            {
                DepositSpot depositSpot = hitInfo.collider.GetComponentInParent<DepositSpot>();
                if (depositSpot != null)
                {
                    depositSpot.Deposit(currentlyHeldObject.Resource);
                    Destroy(currentlyHeldObject.gameObject);
                    return;
                }
            }

            // drop the currently held object
            currentlyHeldObject.Drop();
            currentlyHeldObject = null;
        }
    }

    private bool CheckForObject(float distance, out RaycastHit hitInfo)
    {
        // create ray from camera
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        // return result from cast
        return Physics.Raycast(ray, out hitInfo, distance, interactLayerMask.value);
    }

    private void CheckForInteractions()
    {
        // if an object was found
        if (CheckForObject(interactDistance, out RaycastHit hitInfo))
        {
            // Get SwitchableController in target
            SwitchableController target = hitInfo.collider.GetComponentInParent<SwitchableController>();
            if (target != null)
            {
                // if target was found then switch
                target.SwitchTo(switchController);
                return;
            }

            // Get Carryable in target
            Carryable carryable = hitInfo.collider.GetComponentInParent<Carryable>();

            // check if a Carryable object was found
            if (carryable != null)
            {
                // pick up carryable
                carryable.Pickup(carryHoldTransform);
                currentlyHeldObject = carryable;
            }
        }
    }
}