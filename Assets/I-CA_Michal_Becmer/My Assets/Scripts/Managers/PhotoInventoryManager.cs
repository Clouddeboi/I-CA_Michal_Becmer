using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhotoInventoryManager : MonoBehaviour
{
    [SerializeField] private Image planetImage; //Reference to the UI Image component where the photo will be displayed
    [SerializeField] private TextMeshProUGUI planetNameText; //Reference to the UI Text component where the planet's name will be displayed

    private void Start()
    {
        //Initialize the UI components if not already assigned
        if (planetImage == null || planetNameText == null)
        {
            Debug.LogError("InventoryManager is missing references to UI");
        }
    }

    //Method to display the photo in the UI
    public void DisplayPhoto(Texture2D texture, string planetName)
    {
        if (texture != null)
        {
            //Set the sprite of the Image component to the captured texture
            planetImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            
            //Set the name of the planet in the UI text
            planetNameText.text = planetName;
        }
        else
        {
            Debug.LogWarning("Received texture is null");
        }
    }
}
