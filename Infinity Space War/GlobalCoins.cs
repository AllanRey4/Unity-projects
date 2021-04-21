
using UnityEngine;


public class GlobalCoins : MonoBehaviour
{
    #region SIngleton: GlobalCoins
    public static GlobalCoins Instance;
    public int coins;
    
    void Awake()
    {
        

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion


    public void UseCoins(int amount)
    {     
        coins -= amount;  
    }

    public bool HasEnoughCoins(int amount)
    {
        return (coins >= amount);
    }
}
