using UnityEngine;
using UnityEngine.SceneManagement;
using State;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;//Reference to the pause menu UI panel

    private bool isPaused = false;//Tracks if the game is paused
    [SerializeField] private StateManager stateManager;//Reference to the StateManager 

    //Pauses the game and shows the pause menu
    public void PauseGame()
    {
        //Show the pause menu UI
        pauseMenuUI.SetActive(true);
        stateManager.SetState(State.PlayerStates.GameState.Menu);
        //Freeze game time
        Time.timeScale = 0f;
        isPaused = true;
    }

    //Resumes the game and hides the pause menu
    public void ResumeGame()
    {
        //Hide the pause menu UI
        pauseMenuUI.SetActive(false);
        stateManager.SetState(State.PlayerStates.GameState.Default);
        //Resume game time
        Time.timeScale = 1f;
        isPaused = false;
    }

    //Quits the game
    public void QuitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        stateManager.SetState(State.PlayerStates.GameState.Default);
        Time.timeScale = 1;
    }
}
