using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject continueButton; 

    int savedLevel;  

     
    void Start()
    { 
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

    //If the player played before and reached to a stage greater than the first one, display the Continue button
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
