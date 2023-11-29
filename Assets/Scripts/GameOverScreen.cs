using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void Setup(){
        gameObject.SetActive(true);
    }

    public void RestartButton(int level){
        GameData.level = level;
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenuButton(){
        SceneManager.LoadScene("MainMenu");
    }
}
