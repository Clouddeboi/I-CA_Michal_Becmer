using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; //Reference to the main camera
    [SerializeField] private Camera photoModeCamera; //Reference to the photo mode camera
    [SerializeField] private MonoBehaviour playerMovementScript; //Reference to the player's movement script
    [SerializeField] private TakePhoto takePhotoScript;//Reference to the TakePhoto script

    [SerializeField] private InputAction togglePhotoModeAction; //Input action for toggling photo mode (right mouse click)

    private bool isPhotoModeActive = false; //Tracks whether photo mode is active

    private void Start()
    {
        //Ensure the main camera starts active and the photo mode camera is inactive
        mainCamera.gameObject.SetActive(true);
        photoModeCamera.gameObject.SetActive(false);
        
        if (takePhotoScript != null)
        {
            takePhotoScript.enabled = false;
        }
    }

    private void OnEnable()
    {
        togglePhotoModeAction.Enable(); //Enable the input action
        togglePhotoModeAction.performed += TogglePhotoMode; //Subscribe to the input action
    }

    private void OnDisable()
    {
        togglePhotoModeAction.performed -= TogglePhotoMode; //Unsubscribe from the input action
        togglePhotoModeAction.Disable(); //Disable the input action
    }

    private void TogglePhotoMode(InputAction.CallbackContext context)
    {
        isPhotoModeActive = !isPhotoModeActive; //Toggle the photo mode state

        if (isPhotoModeActive)
        {
            //Enables photo mode
            EnablePhotoMode();
        }
        else
        {
            //Disables photo mode
            DisablePhotoMode();
        }
    }

    private void EnablePhotoMode()
    {
        //Switch to the photo mode camera
        mainCamera.gameObject.SetActive(false);
        photoModeCamera.gameObject.SetActive(true);

        //Disable the player movement script
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
        }

        if (takePhotoScript != null)
        {
            takePhotoScript.enabled = true;
        }
    }

    private void DisablePhotoMode()
    {
        //Switch back to the main camera
        photoModeCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);

        //Re-enable the player movement script
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }

        if (takePhotoScript != null)
        {
            takePhotoScript.enabled = false;
        }
    }
}
