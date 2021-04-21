using UnityEngine;



//player data holder
[System.Serializable]public class PlayerData
{
    public int dataCoins;
}

public static class GameDataManager
{
    static PlayerData playerData = new PlayerData();
    
    static GameDataManager()
    {
        LoadPlayerData();
    }

    public static int GetCoins()
    {
        return playerData.dataCoins;
    }

    public static void AddCoins(int amount)
    {
        playerData.dataCoins += amount;
        SavePlayerData();
    }

    public static bool CanSpendCoins (int amount)
    {
        return (playerData.dataCoins >= amount);
    }

    public static void SpendCoins (int amount)
    {
        playerData.dataCoins -= amount;
        SavePlayerData();
    }

    static void LoadPlayerData()
    {
        playerData = BinarySerializer.Load<PlayerData>("player-data.txt");
        Debug.Log("<color=green>[PlayerData] Loaded.</color>");
    }
    static void SavePlayerData()
    {
        BinarySerializer.Save(playerData, "player-data.txt");
        Debug.Log("<color=magenta>[PlayerData] Saved.</color>");
    }
}
