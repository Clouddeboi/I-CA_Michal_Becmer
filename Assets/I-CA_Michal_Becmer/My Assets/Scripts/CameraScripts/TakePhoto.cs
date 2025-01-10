using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using System.Collections;

public class TakePhoto : MonoBehaviour
{
    [SerializeField] private RaycastSystem raycastSystem; //Reference to the RaycastSystem script
    [SerializeField] private InputAction checkObjectAction; //Input Action for checking objects
    [SerializeField] private PhotoInventoryManager photoinventoryManager;

    AudioManager audioManager;

    private string photoFolderPath; //Path where photos will be saved

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

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
            GameObject detectedObject = raycastSystem.currentObject;

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

            //Capture the screenshot
            string screenshotPath = Path.Combine(photoFolderPath, planetName + "_photo.png");
            ScreenCapture.CaptureScreenshot(screenshotPath);
            Debug.Log($"Screenshot saved to: {screenshotPath}");

            audioManager.PlaySFX(audioManager.TakePhoto);

            //Create a texture from the saved screenshot
            StartCoroutine(LoadTexture(screenshotPath, planetName));
        }
    }

    private IEnumerator LoadTexture(string path, string planetName)
    {
        //Wait until the screenshot is saved
        yield return new WaitForEndOfFrame();

        //Load the texture from the file
        byte[] fileData = File.ReadAllBytes(path);

        //Creates a texture with the resolution of 2x2 
        //(this doesn't matter as it will be updated when we load the file)
        Texture2D texture = new Texture2D(2, 2);

        texture.LoadImage(fileData); //Load the image into the texture

        //Call the InventoryManager to add the photo to the UI
        photoinventoryManager.DisplayPhoto(texture, planetName);
    }
}