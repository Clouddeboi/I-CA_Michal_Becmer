using UnityEngine;

public class PlanetTracker : MonoBehaviour
{
    [SerializeField] private RectTransform directionIndicator;//Arrow on the canvas
    [SerializeField] private Transform player;//Reference to the player's Transform
    [SerializeField] private GameObject indicator;//Reference to the indicator UI

    private Transform trackedPlanet;//Current planet being tracked
    private bool isTracking = false;//Whether tracking is active

    private void Update()
    {
        if (isTracking && trackedPlanet != null)
        {
            indicator.SetActive(true);
            //Calculate direction from player to planet on the X-Z plane
            Vector3 directionToPlanet = trackedPlanet.position - player.position;
            //Ignore vertical difference in 3D space
            directionToPlanet.y = 0;

            //Convert the direction vector (x-z) to (x-y)
            //This is because the game is on the x-z axis
            //And the canvas is on the x-y axis
            Vector2 direction2D = new Vector2(directionToPlanet.x, directionToPlanet.z);

            //Avoid jitter when very close
            if (direction2D.sqrMagnitude > 0.01f)
            {
                //Get the angle in the X-Y plane for the canvas
                float angle = Mathf.Atan2(direction2D.y, direction2D.x) * Mathf.Rad2Deg;

                //Rotate the UI indicator to point in the direction
                directionIndicator.localRotation = Quaternion.Euler(0, 0, angle);
            }
        }
        else
        {
            indicator.SetActive(false);
        }
    }

    public void ToggleTrackingPlanetByTag(string planetTag)
    {
        //Check if the same planet is already being tracked
        if (isTracking && trackedPlanet.CompareTag(planetTag))
        {
            //If the same planet is clicked, stop tracking
            StopTracking();
        }
        else
        {
            //If it's a new planet, start tracking
            TrackPlanetByTag(planetTag);
        }
    }

    //Method to start tracking a planet by its tag
    public void TrackPlanetByTag(string planetTag)
    {
        GameObject planet = GameObject.FindGameObjectWithTag(planetTag);

        if (planet != null)
        {
            trackedPlanet = planet.transform;
            isTracking = true;
            directionIndicator.gameObject.SetActive(true);
            Debug.Log($"Now tracking planet: {planet.name}");
        }
        else
        {
            Debug.LogWarning($"No planet found with tag: {planetTag}");
        }
    }

    //Method to stop tracking
    //Will Implement this later
    public void StopTracking()
    {
        isTracking = false;
        directionIndicator.gameObject.SetActive(false);
        Debug.Log("Tracking stopped.");
    }
}
