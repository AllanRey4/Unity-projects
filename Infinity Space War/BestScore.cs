using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    static public int bScore = 0;

    void Awake()
    {
        if(PlayerPrefs.HasKey("BestScore"))
        {
            bScore = PlayerPrefs.GetInt("BestScore");
        }
        PlayerPrefs.SetInt("BestScore", bScore);
    }


    void Update()
    {
        Text gt = this.GetComponent<Text>();
        gt.text = "Best Score: " + bScore;

        if(bScore > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", bScore);
        }

        if(Score.score > bScore)
          {
             bScore = Score.score;
          }
    }
}
