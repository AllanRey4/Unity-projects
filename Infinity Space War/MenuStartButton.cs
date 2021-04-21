
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStartButton: MonoBehaviour
{
    public void LoadMenu()
   {
       Score.score = 0;  
       Time.timeScale = 1;
       SceneManager.LoadScene(0);
       Debug.Log("ToMenu");
   }

    public void GoToGame()
    {
        Time.timeScale = 1;
        SceneTransition.SwitchToScene("Game");
        Debug.Log("ToGame");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }
 

}
