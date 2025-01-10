using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using State;

public class JournalManager : MonoBehaviour
{
    [SerializeField] private GameObject[] journalPages; //Array to store references to each page panel
    [SerializeField] private Button nextButton; //Next button
    [SerializeField] private Button previousButton; //Previous button
    [SerializeField] private GameObject journalUI; //Reference to the journal UI
    [SerializeField] private InputAction toggleJournalAction; //InputAction for toggling journal UI visibility
    [SerializeField] private StateManager stateManager;

    private int currentPageIndex = 0; //Keeps track of the current page index
    private bool isJournalVisible = false; //To track if the journal is currently visible

    //Start is called before the first frame update
    private void Start()
    {
        //Set up button click events
        previousButton.onClick.AddListener(OnPreviousPage);
        nextButton.onClick.AddListener(OnNextPage);

        //Display the first page (initial planet)
        DisplayPage(currentPageIndex);

        //Initialize journal UI visibility to be hidden
        journalUI.SetActive(isJournalVisible);

        //Enable the input action
        toggleJournalAction.Enable();
    }

    //Update is called once per frame
    private void Update()
    {
        //Check if the toggle journal action is triggered
        if (toggleJournalAction.triggered)
        {
            ToggleJournalVisibility(); //Toggle the visibility of the journal
        }
    }

    //Method to toggle journal UI visibility
    private void ToggleJournalVisibility()
    {
        isJournalVisible = !isJournalVisible; //Toggle the visibility state
        journalUI.SetActive(isJournalVisible); //Show or hide the journal UI

        if(isJournalVisible == true)
        {
            //Call StateManager to enter Photo state
            if (stateManager != null)
            {
                Debug.Log("Entered Journal State.");
                stateManager.SetState(State.PlayerStates.GameState.Menu);
            }
        }
        else
        {
            //Call StateManager to enter Photo state
            if (stateManager != null)
            {
                Debug.Log("Entered Default State.");
                stateManager.SetState(State.PlayerStates.GameState.Default);
            }
        }
    }

    //Method to display a page based on the current index
    private void DisplayPage(int pageIndex)
    {
        if (pageIndex >= 0 && pageIndex < journalPages.Length)
        {
            //Hide all panels
            foreach (var page in journalPages)
            {
                page.SetActive(false);
            }

            //Show the current page
            journalPages[pageIndex].SetActive(true);
        }
    }

    //Method to go to the previous page
    private void OnPreviousPage()
    {
        //Increment the page index and loop back to the last page if at the end
        currentPageIndex = (currentPageIndex + 1) % journalPages.Length; //This ensures looping
        DisplayPage(currentPageIndex); //Update the displayed page
    }

    //Method to go to the previous page
    private void OnNextPage()
    {
        //Decrement the page index and loop back to the first page if at the start
        currentPageIndex = (currentPageIndex - 1 + journalPages.Length) % journalPages.Length; //This ensures looping
        DisplayPage(currentPageIndex); //Update the displayed page
    }
}
