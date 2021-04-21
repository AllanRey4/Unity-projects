
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public GameObject restart;


    public void OnClickBottom()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
        Score.score = 0;
        Debug.Log("ReloadScene");
    }

   
}
