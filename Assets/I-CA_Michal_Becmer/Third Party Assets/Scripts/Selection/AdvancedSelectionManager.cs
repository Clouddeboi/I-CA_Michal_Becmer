using UnityEngine;

namespace GD.Selection
{
    public class AdvancedSelectionManager : MonoBehaviour
    {
        [SerializeField] private IRayProvider rayProvider; //Ray provider (ray creation)
        [SerializeField] private ISelector selector; //Selector (selection logic)
        [SerializeField] private ISelectionResponse response; //Selection response (selection effects)

        private Transform currentSelection;

        private void Awake()
        {
            //Initialize components if they aren't already assigned in the inspector
            rayProvider = rayProvider ?? GetComponent<IRayProvider>();
            selector = selector ?? GetComponent<ISelector>();
            response = response ?? GetComponent<ISelectionResponse>();
        }

        private void Update()
        {
            //If there's a current selection, deselect it first
            if (currentSelection != null)
            {
                response.OnDeselect(currentSelection);
            }

            //Create a ray using the ray provider (mouse-based ray)
            Ray ray = rayProvider.CreateRay();

            //Check the selection using the selector and the created ray
            selector.Check(ray);

            //Get the current selection based on the ray hit
            currentSelection = selector.GetSelection();

            //If there's a valid selection, apply the selection response
            if (currentSelection != null)
            {
                response.OnSelect(currentSelection);
            }
        }
    }
}
