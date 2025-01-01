using UnityEngine;

namespace GD.Selection
{
    //implementing IRayProvider which uses a ray for selection purposes
    public class MouseScreenRayProvider : MonoBehaviour, IRayProvider
    {
        [SerializeField] private Camera mainCamera; //Reference to the main camera

        //Creates a ray based on the cursors position
        public Ray CreateRay()
        {
            //Get the cursor's position in the world
            Vector3 cursorPosition = Input.mousePosition;

            //Convert the cursor position to a world position
            Ray ray = mainCamera.ScreenPointToRay(cursorPosition);

            return ray;
        }
    }
}
