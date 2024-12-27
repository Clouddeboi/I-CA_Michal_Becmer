using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //The Player to follow
    public Vector3 offset = new Vector3(0f, 0.3f, -5f); //Offset position from the target
    public float followSpeed = 5f; //Speed of the camera following the target
    public bool lockYAxis = true; //Bool to lock rotation along the Y-axis

    public float zoomInSpeed = 0.5f; //Speed of zoom
    public float zoomOutSpeed = 0.5f; //Speed of zoom
    public float minZoom = -10f; //Minimum zoom (closer to the target)
    public float maxZoom = -2f; //Maximum zoom (farther from the target)

    [SerializeField] private InputAction zoomInAction; //InputAction for zooming in
    [SerializeField] private InputAction zoomOutAction; //InputAction for zooming out

    private float currentZoom; //Track current zoom level
    private float targetZoom; //Target zoom level when key is released
    private bool isZoomingOut = false; //Track if zooming in
    private bool isZoomingIn = false; //Track if zooming out

    void Start()
    {
        currentZoom = offset.y; //Initialize current zoom level
        targetZoom = currentZoom; //Initialize target zoom level
    }

    //Subscribe to input events when the script is active and unsubscribe when disabled
    //to manage event handlers and prevent errors
    private void OnEnable()
    {
        zoomInAction.Enable(); //Enable the ZoomIn action
        zoomOutAction.Enable(); //Enable the ZoomOut action
        zoomInAction.performed += OnZoomInPressed; //Subscribe to zoom in key press
        zoomOutAction.performed += OnZoomOutPressed; //Subscribe to zoom out key press
        zoomInAction.canceled += OnZoomInReleased; //Subscribe to zoom in key release
        zoomOutAction.canceled += OnZoomOutReleased; //Subscribe to zoom out key release
    }

    private void OnDisable()
    {
        zoomInAction.performed -= OnZoomInPressed; //Unsubscribe from zoom in key press
        zoomOutAction.performed -= OnZoomOutPressed; //Unsubscribe from zoom out key press
        zoomInAction.canceled -= OnZoomInReleased; //Unsubscribe from zoom in key release
        zoomOutAction.canceled -= OnZoomOutReleased; //Unsubscribe from zoom out key release
        zoomInAction.Disable(); //Disable the ZoomIn action
        zoomOutAction.Disable(); //Disable the ZoomOut action
    }

    void LateUpdate()
    {
        if (target != null)
        {
            //Calculate the desired position with offset
            Vector3 desiredPosition = target.position + offset;

            //Smoothly move the camera to the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

            //Adjust rotation
            if (lockYAxis)
            {
                //Rotate only on the horizontal plane
                Vector3 lookDirection = target.position - transform.position;
                lookDirection.y = 0; //Lock the Y-axis
                if (lookDirection.magnitude > 0.1f) //Avoid zero-length vectors
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), followSpeed * Time.deltaTime);
                }
            }
        }

        //Update the zoom level with the correct speed depending on whether zooming in or out
        float zoomSpeed = isZoomingIn ? zoomInSpeed : zoomOutSpeed;
        currentZoom = Mathf.Lerp(currentZoom, targetZoom, zoomSpeed * Time.deltaTime);
        offset.y = currentZoom; //Update the offset with the new zoom level
    }

    private void OnZoomInPressed(InputAction.CallbackContext context)
    {
        //Start zooming out when the key is pressed
        isZoomingOut = true;
        targetZoom = Mathf.Max(targetZoom - zoomOutSpeed * Time.deltaTime, minZoom); //Set target zoom but clamp to minZoom
    }

    private void OnZoomInReleased(InputAction.CallbackContext context)
    {
        //Stop zooming out when the key is released, freeze the target zoom level
        isZoomingOut = false;
        targetZoom = currentZoom; //Stop zooming and hold the zoom at the current position
    }

    private void OnZoomOutPressed(InputAction.CallbackContext context)
    {
        //Start zooming In when the key is pressed
        isZoomingIn = true;
        targetZoom = Mathf.Min(targetZoom + zoomInSpeed * Time.deltaTime, maxZoom); //Set target zoom but clamp to maxZoom
    }

    private void OnZoomOutReleased(InputAction.CallbackContext context)
    {
        //Stop zooming In when the key is released, freeze the target zoom level
        isZoomingIn = false;
        targetZoom = currentZoom; //Stop zooming and hold the zoom at the current position
    }
}
