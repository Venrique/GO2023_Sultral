using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public Text[] coinsTexts;

    // Start is called before the first frame update
    void Start()
    {
        SaveData loadedData = SaveSystem.LoadGame();
        if (loadedData == null)
        {
            loadedData = new SaveData();
        }
        GameData.saveData = loadedData;

        coinsTexts[0].text = "Coins: " + GameData.saveData.coinsLevel1 + "/3";
        coinsTexts[1].text = "Coins: " + GameData.saveData.coinsLevel2 + "/3";
        coinsTexts[2].text = "Coins: " + GameData.saveData.coinsLevel3 + "/3";
        coinsTexts[3].text = "Coins: " + GameData.saveData.coinsLevel4 + "/3";
        coinsTexts[4].text = "Coins: " + GameData.saveData.coinsLevel5 + "/3";
        coinsTexts[5].text = "Coins: " + GameData.saveData.coinsLevel6 + "/3";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goToLevel(int level)
    {
        GameData.level = level;

        SceneManager.LoadScene("SampleScene");
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.currentLevel = level;
        }
    }

    public void ExitGame() {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
