using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int level = 1;
    public static int enemyKills = 0;
    public static int coins = 0;

    public static SaveData saveData;

    public static void saveGame()
    {
        saveData = SaveSystem.LoadGame();
        if (saveData == null)
        {
            saveData = new SaveData();
        }
        switch (level)
        {
            case 1:
                if (coins >= saveData.coinsLevel1)
                {
                    saveData.coinsLevel1 = coins;
                }
                break;
            case 2:
                if (coins >= saveData.coinsLevel2)
                {
                    saveData.coinsLevel2 = coins;
                }
                break;
            case 3:
                if (coins >= saveData.coinsLevel3)
                {
                    saveData.coinsLevel3 = coins;
                }
                break;
            case 4:
                if (coins >= saveData.coinsLevel4)
                {
                    saveData.coinsLevel4 = coins;
                }
                break;
            case 5:
                if (coins >= saveData.coinsLevel5)
                {
                    saveData.coinsLevel5 = coins;
                }
                break;
            case 6:
                if (coins >= saveData.coinsLevel6)
                {
                    saveData.coinsLevel6 = coins;
                }
                break;
        }
        SaveSystem.SaveGame(GameData.saveData);
        coins = 0;
        enemyKills = 0;
    }
}
