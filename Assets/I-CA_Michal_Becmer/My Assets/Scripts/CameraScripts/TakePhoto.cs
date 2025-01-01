using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

public class TakePhoto : MonoBehaviour
{
    [SerializeField] private RaycastSystem raycastSystem; //Reference to the RaycastSystem script
    [SerializeField] private InputAction checkObjectAction; //Input Action for checking objects

    private string photoFolderPath; //Path where photos will be saved

    private void Awake()
    {
        //Automatically find RaycastSystem on the same GameObject if not assigned
        if (raycastSystem == null)
        {
            raycastSystem = GetComponent<RaycastSystem>();
        }

        //Define the main "Photos" folder inside the Unity project
        photoFolderPath = Path.Combine(Application.dataPath, "Photos");
        
        //Create the main "Photos" folder if it doesn't exist
        if (!Directory.Exists(photoFolderPath))
        {
            Directory.CreateDirectory(photoFolderPath);
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
            //Unsubscribe from the action's performed event
            checkObjectAction.performed -= OnCheckObject;
            //Disable the Input Action
            checkObjectAction.Disable();
        }
    }

    private void OnCheckObject(InputAction.CallbackContext context)
    {
        if (raycastSystem != null)
        {
            //Call the CheckForObject method in the RaycastSystem
            raycastSystem.CheckForObject();

            //Check if a valid object is detected before trying to take a screenshot
            GameObject detectedObject = raycastSystem.currentObject; // Use the public property

            if (detectedObject != null)
            {
                //Take the screenshot only if currentObject is valid
                TakeScreenshot(detectedObject);
            }
            else
            {
                Debug.LogWarning("No valid object detected to take a photo");
            }
        }
    }

    private void TakeScreenshot(GameObject objectToTakePhotoOf)
    {
        if (objectToTakePhotoOf != null)
        {
            string planetName = objectToTakePhotoOf.name;

            //Define the subfolder path for each planet
            string planetFolderPath = Path.Combine(photoFolderPath, planetName);
            
            //Create the folder for the planet if it doesn't exist
            if (!Directory.Exists(planetFolderPath))
            {
                Directory.CreateDirectory(planetFolderPath);
            }

            //Create a unique file name based on the object name and timestamp
            string screenshotName = planetName + "_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
            string screenshotPath = Path.Combine(planetFolderPath, screenshotName);

            //Capture the screenshot
            ScreenCapture.CaptureScreenshot(screenshotPath);
            Debug.Log($"Screenshot saved to: {screenshotPath}");
        }
    }
}
