using UnityEngine;

public class RaycastSystem : MonoBehaviour
{
    // Reference to the audio manager (for future functionality if needed)
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();
    }

    //Layer mask to specify which layers the detection will interact with
    [SerializeField] private LayerMask layerMask; 

    //Size of the detection box (as a 3D box shape in front of the player)
    [SerializeField] private Vector3 boxSize = new Vector3(0.5f, 0.5f, 0.5f);

    //Offset of the detection box relative to the player (in front of the player)
    [SerializeField] private Vector3 boxOffset = new Vector3(0f, 0f, 0.25f);

    //Boolean to check if the player is facing an object
    public bool isFacingObject { get; private set; }
    public GameObject currentObject = null;

    //Method to set the detection box size dynamically
    public void SetDetectionRange(Vector3 newBoxSize)
    {
        //Update the detection box size when called
        boxSize = newBoxSize;
    }

    //Method to configure LayerMask dynamically
    public void SetLayerMask(LayerMask newLayerMask)
    {
        layerMask = newLayerMask;
    }

    //Method to check for objects in the detection area
    public void CheckForObject()
    {
        //Reset the value for isFacingObject every time we check
        isFacingObject = false;

        //Calculate the center of the detection box
        Vector3 boxCenter = transform.position + transform.TransformDirection(boxOffset);
        
        //Perform a box overlap check to detect objects within the detection box
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, boxSize / 2, transform.rotation, layerMask);

        //If we detect an object, debug the info in the terminal
        if (hitColliders.Length > 0)
        {
            foreach (Collider hitCollider in hitColliders)
            {
                //Log the name of the object detected
                Debug.Log("Object detected: " + hitCollider.gameObject.name);
                isFacingObject = true;
                //Set the currentObject to the object being detected
                currentObject = hitCollider.gameObject;

                //Draw a debug line from the player to the detected object
                Debug.DrawLine(transform.position, hitCollider.transform.position, Color.green);
                break; //Stop after finding the first interactable object
            }
        }
        else
        {
            currentObject = null;
            //If no objects are detected
            Debug.Log("No objects detected in the box.");
        }
    }

    //Getter method for box size (so other scripts can access it)
    public Vector3 GetBoxSize()
    {
        return boxSize;
    }

    //Getter method for box offset (so other scripts can access it)
    public Vector3 GetBoxOffset()
    {
        return boxOffset;
    }

    //Draw the detection box for debugging in the scene view
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.yellow;
        Vector3 boxCenter = transform.position + transform.TransformDirection(boxOffset);
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
}
