using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject continueButton;
    int savedLevel;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        ContinueGame();
    } 

    public void StartGame()
    {
        PlayerPrefs.SetInt("lvl", 0);
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
         savedLevel = PlayerPrefs.GetInt("lvl");

        if (savedLevel > 0) {
            continueButton.SetActive(true);
        }  
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(savedLevel);
    }

}
