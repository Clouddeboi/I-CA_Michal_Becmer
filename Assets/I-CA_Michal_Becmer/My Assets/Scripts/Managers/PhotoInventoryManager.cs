using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PhotoInventoryManager : MonoBehaviour
{
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

    //Dictionary to save planet names
    private Dictionary<string, Image> planetImageMap;

    private void Start()
    {
        //Initializes the dictionary with planet names and their Image components
        planetImageMap = new Dictionary<string, Image>
        {
            { "Earth", earthImage },
            { "Moon", moonImage },
            { "Mars", marsImage },
            { "Sun", sunImage },
            { "Mars", mercuryImage },
            { "Venus", VenusImage },
            { "Jupiter", jupiterImage },
            { "Saturn", saturnImage },
            { "Uranus", uranusImage },
            { "Neptune", neptuneImage },
            { "Pluto", plutoImage }
        };

        //Ensure all references are set up
        if (earthImage == null || marsImage == null)
        {
            Debug.LogError("Missing references to UI components");
        }
    }

    //Method to display the photo in the UI
    public void DisplayPhoto(Texture2D texture, string planetName)
    {
        if (texture != null && planetImageMap.ContainsKey(planetName))
        {
            //Get the correct Image component based on the planet name
            Image targetImage = planetImageMap[planetName];

            //Set the sprite of the Image component to the captured texture
            targetImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            //Set the name of the planet in the UI text
            //planetNameText.text = planetName;
        }
        else
        {
            Debug.LogWarning("No Image component found for planet: " + planetName);
        }
    }
}
