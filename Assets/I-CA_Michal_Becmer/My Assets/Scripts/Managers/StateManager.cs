using UnityEngine;

namespace State
{
    public class StateManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private PlayerController playerController;

        //Current state of the game
        private PlayerStates.GameState currentState = PlayerStates.GameState.Default;

        AudioManager audioManager;

        private void Awake()
        {
            audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }

        private void Start()
        {
            //Start with the default state
            SetState(PlayerStates.GameState.Default);
        }

        private void Update()
        {
            //Evaluate the current state and execute logic
            switch (currentState)
            {
                case PlayerStates.GameState.Default:
                    HandleDefaultState();
                    break;
                case PlayerStates.GameState.Photo:
                    HandlePhotoState();
                    break;
                case PlayerStates.GameState.Menu:
                    HandleMenuState();
                    break;
            }
        }

        public void SetState(PlayerStates.GameState newState)
        {
            currentState = newState;
            Debug.Log($"State changed to: {currentState}");
        }

        private void HandleDefaultState()
        {
            //Logic for Default state
            playerController.enabled = true;
            Debug.Log("In Default State.");
        }

        private void HandlePhotoState()
        {
            //Logic for Photo state
            playerController.enabled = false;
            Debug.Log("In Photo State.");
        }

        private void HandleMenuState()
        {
            //Logic for Menu state
            playerController.enabled = false;
            Debug.Log("In Menu State.");
        }
    }
}
