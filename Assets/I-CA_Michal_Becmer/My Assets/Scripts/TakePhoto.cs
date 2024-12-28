using UnityEngine;
using UnityEngine.InputSystem;

public class TakePhoto : MonoBehaviour
{
    [SerializeField] private RaycastSystem raycastSystem; // Reference to the RaycastSystem script

    [SerializeField] private InputAction checkObjectAction; // Input Action for checking objects

    private void Awake()
    {
        //Automatically find RaycastSystem on the same GameObject if not assigned
        if (raycastSystem == null)
        {
            raycastSystem = GetComponent<RaycastSystem>();
        }
    }

    private void OnEnable()
    {
        if (checkObjectAction != null)
        {
            //Enable the Input Action
            checkObjectAction.Enable();
            //Subscribe to the action's performed event
            checkObjectAction.performed += OnCheckObject;
        }
    }

    private void OnDisable()
    {
        if (checkObjectAction != null)
        {
            // Unsubscribe from the action's performed event
            checkObjectAction.performed -= OnCheckObject;
            // Disable the Input Action
            checkObjectAction.Disable();
        }
    }

    private void OnCheckObject(InputAction.CallbackContext context)
    {
        if (raycastSystem != null)
        {
            //Call the CheckForObject method in the RaycastSystem
            raycastSystem.CheckForObject();
        }
    }
}
