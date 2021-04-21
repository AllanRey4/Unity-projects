
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    static public Score Sc;
    public Text Points;
    public static int score = 0;
    public static int pointsPerEnemy;
    static float timeLife = 1500; //500 = 10 секунд
    
    private void Awake()
    {
        GameObject scoreGO = GameObject.Find("Points");
        Points = scoreGO.GetComponent<Text>();
        Points.text = "0";    
    }

    private void FixedUpdate()
    {      
            if(Enemy.pointsPerEnemy == 100) //работает вместе с лотом. Если покупка совершена,
            {     
                timeLife--;                      // то идёт отсчёт времени для удаления бонуса
                if(timeLife < 1)
                {
                   print("Снова по 50 очков");
                   Enemy.pointsPerEnemy = 50;
                   timeLife = 1500;
                }
            }
            Points.text = score.ToString();
    }
    public static void ScoreBonus(int pointsPerEnemy)
    {
        score += pointsPerEnemy;
    }
}
