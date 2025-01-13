using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PhotoInventoryManager : MonoBehaviour
{
    [Header("---Planet Images---")]
    //[SerializeField] private Image planetImage; //Reference to the UI Image component where the photo will be displayed
    [SerializeField] private Image sunImage;
    [SerializeField] private Image earthImage;
    [SerializeField] private Image moonImage;
    [SerializeField] private Image mercuryImage;
    [SerializeField] private Image VenusImage;
    [SerializeField] private Image marsImage;
    [SerializeField] private Image jupiterImage;
    [SerializeField] private Image saturnImage;
    [SerializeField] private Image uranusImage;
    [SerializeField] private Image neptuneImage;
    [SerializeField] private Image plutoImage;
    //[SerializeField] private TextMeshProUGUI planetNameText; //Reference to the UI Text component where the planet's name will be displayed

    [Header("---Planet UI Game Objects---")]
    // References to the planet UI GameObjects
    [SerializeField] private GameObject sunObject;
    [SerializeField] private GameObject earthObject;
    [SerializeField] private GameObject moonObject;
    [SerializeField] private GameObject mercuryObject;
    [SerializeField] private GameObject venusObject;
    [SerializeField] private GameObject marsObject;
    [SerializeField] private GameObject jupiterObject;
    [SerializeField] private GameObject saturnObject;
    [SerializeField] private GameObject uranusObject;
    [SerializeField] private GameObject neptuneObject;
    [SerializeField] private GameObject plutoObject;

    //Dictionary to save planet names
    private Dictionary<string, Image> planetImageMap;
    private Dictionary<string, GameObject> planetObjectMap;

    private void Start()
    {
        //Initializes the dictionary with planet names and their Image components
        planetImageMap = new Dictionary<string, Image>
        {
            { "Earth", earthImage },
            { "Moon", moonImage },
            { "Mars", marsImage },
            { "Sun", sunImage },
            { "Mercury", mercuryImage },
            { "Venus", VenusImage },
            { "Jupiter", jupiterImage },
            { "Saturn", saturnImage },
            { "Uranus", uranusImage },
            { "Neptune", neptuneImage },
            { "Pluto", plutoImage }
        };

        planetObjectMap = new Dictionary<string, GameObject>
        {
            { "Earth", earthObject },
            { "Moon", moonObject },
            { "Mars", marsObject },
            { "Sun", sunObject },
            { "Mercury", mercuryObject },
            { "Venus", venusObject },
            { "Jupiter", jupiterObject },
            { "Saturn", saturnObject },
            { "Uranus", uranusObject },
            { "Neptune", neptuneObject },
            { "Pluto", plutoObject }
        };

    }

    //Method to display the photo in the UI
    public void DisplayPhoto(Texture2D texture, string planetName)
    {
        if (texture != null)
        {
            if(planetImageMap.ContainsKey(planetName))
            {
                //Get the correct Image component based on the planet name
                Image targetImage = planetImageMap[planetName];

                //Set the sprite of the Image component to the captured texture
                targetImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                //Set the name of the planet in the UI text
                //planetNameText.text = planetName;
                // Activate the corresponding planet GameObject
            }

            if (planetObjectMap.ContainsKey(planetName))
            {
                GameObject targetPlanetObject = planetObjectMap[planetName];
                targetPlanetObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("No Image component found for planet: " + planetName);
        }
    }

    //Method to get all planet GameObjects
    public IEnumerable<GameObject> GetAllPlanetObjects()
    {
        return planetObjectMap.Values;
    }
}
