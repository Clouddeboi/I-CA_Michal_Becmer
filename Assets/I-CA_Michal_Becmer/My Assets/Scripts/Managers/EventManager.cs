using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private PhotoInventoryManager photoInventoryManager;//Reference to the PhotoInventoryManager
    [SerializeField] private bool allPhotosTaken = false;//Flag to track if all photos have been taken
    [SerializeField] private EndingsVoicelinesManager endingsVoicelinesManager;

    private void Update()
    {
        //Check if all photos have been taken
        if (!allPhotosTaken && AreAllPhotosTaken())
        {
            //Set the bool to true to avoid repeating the event
            allPhotosTaken = true;
            //Trigger the event
            OnAllPhotosTaken();
        }
    }

    //Checks if all photos are taken
    private bool AreAllPhotosTaken()
    {
        foreach (var planetObject in photoInventoryManager.GetAllPlanetObjects())
        {
            if (!planetObject.activeSelf)
            {
                //If any planet object is not active, return false
                return false;
            }
        }
        //All planet objects are active
        return true;
    }

    //Event triggered when all photos are taken
    private void OnAllPhotosTaken()
    {
        //We will add an actual event later
        Debug.Log("All photos have been taken! Event triggered.");
        endingsVoicelinesManager.PlayEndGame();
    }
}
