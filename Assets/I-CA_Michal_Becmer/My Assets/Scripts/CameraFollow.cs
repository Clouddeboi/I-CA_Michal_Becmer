using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //The Player to follow
    public Vector3 offset = new Vector3(0f, 0.3f, 0f); //Offset position from the target
    public float followSpeed = 5f; //Speed of the camera following the target
    public bool lockYAxis = true; //Bool to lock rotation along the Y-axis

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
                //This is since the game will be top down there is no need to rotate it
                //any other way for now (this will change for photo mode when we go into first person)
                Vector3 lookDirection = target.position - transform.position;
                lookDirection.y = 0; //Lock the Y-axis
                if (lookDirection.magnitude > 0.1f) //Avoid zero-length vectors
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), followSpeed * Time.deltaTime);
                }
            }
        }
    }
}
