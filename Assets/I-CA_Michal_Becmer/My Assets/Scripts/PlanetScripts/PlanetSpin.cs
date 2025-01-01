using UnityEngine;

public class PlanetSpin : MonoBehaviour
{
    public GameObject targetObject; //The Planet to spin
    public Vector3 rotationSpeed = new Vector3(0f, 100f, 0f); //Rotation speed in degrees per second

    void Update()
    {
        //Check if a target object is assigned
        if (targetObject != null)
        {
            //Rotate the target object based on the rotation speed
            targetObject.transform.Rotate(rotationSpeed * Time.deltaTime);
        }
    }
}

