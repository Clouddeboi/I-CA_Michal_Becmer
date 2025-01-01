using UnityEngine;

namespace GD.Selection
{
    public class MouseSelector : MonoBehaviour, ISelector
    {
        private Transform currentSelection;//stores the current selection
        private RaycastHit hitInfo;//stores the information from the ray

        public void Check(Ray ray)
        {
            //Perform the raycast
            if (Physics.Raycast(ray, out hitInfo))
            {
                //if it hits stores the objects transform
                currentSelection = hitInfo.transform;
            }
            else
            {
                //if not clear the selection
                currentSelection = null;
            }
        }

        public Transform GetSelection()
        {
            //Return the current selection
            return currentSelection;
        }

        public RaycastHit GetHitInfo()
        {
            //Return the hit information for further processing
            return hitInfo;
        }
    }
}
