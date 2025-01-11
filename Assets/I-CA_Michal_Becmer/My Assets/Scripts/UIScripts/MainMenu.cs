using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void Quit()
    {
        Application.Quit();
        //Since we can't actually quit when running in unity we just debug a msg for now
        Debug.Log("Game has been closed.");
    }
}
