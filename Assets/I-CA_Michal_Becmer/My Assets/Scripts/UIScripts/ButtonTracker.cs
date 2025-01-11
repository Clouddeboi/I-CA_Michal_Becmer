using UnityEngine;
using UnityEngine.UI;

public class UIPlanetButton : MonoBehaviour
{
    [SerializeField] private PlanetTracker planetTracker;//Reference to the PlanetTracker
    [SerializeField] private string planetTag;//Tag of the planet to track
    [SerializeField] private Button trackButton;//Reference to the button

    private void Start()
    {
        //Add listener to the button
        if (trackButton != null)
        {
            trackButton.onClick.AddListener(() => planetTracker.ToggleTrackingPlanetByTag(planetTag));
        }
    }
}
