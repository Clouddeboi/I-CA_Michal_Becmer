using UnityEngine;
using UnityEngine.UI;  //For UI elements
using TMPro;

namespace GD.Selection
{
    public class HighlightSelectionResponse : MonoBehaviour, ISelectionResponse
    {
        [SerializeField] private Camera mainCamera; //Reference to the main camera
        [SerializeField] private TMP_Text uiText; //Reference to the UI Text element to display object info

        private Transform currentHoveredObject; //To track the current hovered object

        private void Update()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); //Ray from the camera to the mouse position
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //If we hit an object, display its info in the UI
                DisplayObjectInfo(hit.transform);
            }
            else
            {
                //If no object is hit, clear the displayed info
                if (currentHoveredObject != null)
                {
                    uiText.text = "No object hovered"; //Show message in UI if nothing is hovered
                    currentHoveredObject = null;
                }
            }
        }

        private void DisplayObjectInfo(Transform obj)
        {
            //Get the object name and layer
            string objectName = obj.name;
            string objectLayer = LayerMask.LayerToName(obj.gameObject.layer);

            //Update the UI Text element with the object's name and layer
            uiText.text = "Object: " + objectName + "\nType: " + objectLayer;

            //Track the hovered object
            currentHoveredObject = obj;
        }

        public void OnSelect(Transform currentTransform)
        {
            //Additional code can be implemented later
        }

        public void OnDeselect(Transform currentTransform)
        {
            //Additional code can be implemented later
        }
    }
}
