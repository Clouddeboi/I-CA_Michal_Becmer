using UnityEngine;

namespace GD.Selection
{
    public class HighlightSelectionResponse : MonoBehaviour, ISelectionResponse
    {
        [SerializeField] private Camera mainCamera; //Reference to the main camera

        private Transform currentHoveredObject; //To track the current hovered object

        private void Update()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); //Ray from the camera to the mouse position
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //If we hit an object, display its info in the debug console
                DisplayObjectInfo(hit.transform);
            }
            else
            {
                //If no object is hit, clear the displayed info
                //We will always hit an object as even the empty space has a layer
                if (currentHoveredObject != null)
                {
                    Debug.Log("No object hovered");
                    currentHoveredObject = null;
                }
            }
        }

        private void DisplayObjectInfo(Transform obj)
        {
            //Get the object name and layer
            string objectName = obj.name;
            string objectLayer = LayerMask.LayerToName(obj.gameObject.layer);

            //Log the object's name and layer in the console
            //Will change this to also display in the ui
            Debug.Log("Object: " + objectName + ", Layer: " + objectLayer);

            //Track the hovered object
            currentHoveredObject = obj;
        }

        public void OnSelect(Transform currentTransform)
        {
            //Additional code will be implemented later
        }

        public void OnDeselect(Transform currentTransform)
        {
            //Additional code will be implemented later
        }
    }
}
