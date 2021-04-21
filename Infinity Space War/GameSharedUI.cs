using UnityEngine;
using UnityEngine.UI;

public class GameSharedUI : MonoBehaviour
{
    #region Singleton class: GameSharedUI

    public static GameSharedUI Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion


    [SerializeField] Text[] coinsUIText; 

    private void Start()
    {
        UpdateCoinsUIText();
    }

    public void UpdateCoinsUIText()
    {
        for (int i = 0; i < coinsUIText.Length; i++)
        {
            SetCoinsText(coinsUIText[i], GameDataManager.GetCoins());
        }

    }

    void SetCoinsText(Text textMesh, int value)
    {  
        textMesh.text = value.ToString();
    }

    int GetFirstDigitFromNumber(int num)
    {
        return int.Parse(num.ToString()[0].ToString());
    }

}
