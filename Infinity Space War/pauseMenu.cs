
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    bool isPaused = false;


    public void PauseGame()
    {
        if (isPaused)
        { 
           Time.timeScale = 1;
           isPaused = false;
        }
        else
        {
           Time.timeScale = 0.0001f;
           isPaused = true;
        }
    }

}
