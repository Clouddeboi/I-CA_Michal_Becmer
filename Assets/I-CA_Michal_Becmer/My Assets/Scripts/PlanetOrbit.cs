using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    public Transform target; //Target object to rotate around
    public float speed = 5f; // Set a fixed speed of the movement
    public float radius = 3f; //radius of the path
    public float angle = 0f; //current angle of the object

    private float targetRadius; // Target radius for smooth transition
    private float radiusVelocity = 0f; // Time to transition to new radius

    private float smoothTime = 4f;

    void Start()
    {
        //Initialize target radius to the current radius
        targetRadius = radius;
    }

    //Update is called once per frame
    void Update()
    {
        //Smoothly transition to the new radius using SmoothDamp
        radius = Mathf.SmoothDamp(radius, targetRadius, ref radiusVelocity, smoothTime);

        float x = target.position.x + Mathf.Cos(angle) * radius;
        float y = target.position.y;
        float z = target.position.z + Mathf.Sin(angle) * radius;

        transform.position = new Vector3(x, y, z);

        angle += speed * Time.deltaTime; // Keep the speed constant
    }
}
