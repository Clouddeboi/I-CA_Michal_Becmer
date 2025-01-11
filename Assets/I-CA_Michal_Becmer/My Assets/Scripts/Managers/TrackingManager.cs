using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using State;

public class TrackingManager : MonoBehaviour
{
    [SerializeField] private GameObject trackingUI;//Reference to the tracking UI
    [SerializeField] private InputAction toggleTrackingAction;//InputAction for toggling UI visibility
    [SerializeField] private StateManager stateManager;//Reference to the StateManager

    private bool isTrackingUIVisible = false;//To track if the tracking UI is currently visible

    private void Start()
    {
        //Initialize visibility to be hidden
        trackingUI.SetActive(isTrackingUIVisible);

        //Enable the input action
        toggleTrackingAction.Enable();
    }

    private void Update()
    {
        //Check if the toggle tracking action is triggered
        if (toggleTrackingAction.triggered)
        {
            //Toggle the visibility of the tracking UI
            ToggleTrackingUI();
        }
    }

    //Method to toggle UI visibility
    private void ToggleTrackingUI()
    {
        //Toggle the visibility state
        isTrackingUIVisible = !isTrackingUIVisible;
        //Show or hide the tracking UI
        trackingUI.SetActive(isTrackingUIVisible);

        if (isTrackingUIVisible)
        {
            //Call StateManager to enter Menu state
            if (stateManager != null)
            {
                Debug.Log("Entered Tracking State.");
                stateManager.SetState(State.PlayerStates.GameState.Menu);
            }
        }
        else
        {
            //Call StateManager to enter Default state
            if (stateManager != null)
            {
                Debug.Log("Entered Default State.");
                stateManager.SetState(State.PlayerStates.GameState.Default);
            }
        }
    }

    private void OnEnable()
    {
        //Enable the input action
        toggleTrackingAction.Enable();
    }

    private void OnDisable()
    {
        //Disable the input action
        toggleTrackingAction.Disable();
    }
}
