using UnityEngine;

namespace State
{
    public class StateManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera photoModeCamera;
        [SerializeField] private TakePhoto takePhoto;
        [SerializeField] private GameObject playerUI;

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

        public PlayerStates.GameState GetCurrentState()
        {
            return currentState;
        }

        private void HandleDefaultState()
        {
            //Enable player movement in Default state
            if (playerController != null)
            {
                playerController.enabled = true;
            }
            playerUI.SetActive(true);
        }

        private void HandlePhotoState()
        {
            //Disable player movement in Photo state
            if (playerController != null)
            {
                playerController.enabled = false;
            }
            playerUI.SetActive(false);
        }

        private void HandleMenuState()
        {
            //Logic for Menu state
            playerUI.SetActive(false);
            playerController.enabled = false;
        }
    }
}
