using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController singleton; 
    public Text winText;
    public GameObject[] toDisableOnEnd;
    public GameObject gameEnd;

    AudioSource audiosource;
    int lvl; 

    private Ground[] allSquares;
     
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        SetupLevel(); 
    }
     
    public void SetupLevel()
    { 
        allSquares = FindObjectsOfType<Ground>();
    }

    //Before starting the scene check if the GameController GameObject exists,
    //and if there is a duplicate then destroy it and continue on duplicating the one we have.
    private void Awake()
    {
        winText.CrossFadeAlpha(0, 0f, false);
        if (singleton == null)
        {
            singleton = this;
         
        } else if(singleton != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinished;
    }

    private void OnLevelFinished(Scene scene, LoadSceneMode mode)
    {
        SetupLevel();
    }
    
    //A continuous checker to check if all existing tiles were colored successfully by the player.
    public void CheckFinished()
    {
        bool isFinished = true;

        for(int i = 0; i < allSquares.Length; i++)
        {
            if(allSquares[i].isColoured == false)
            {
                isFinished = false;
                break;
            }
        }

        //If the current level is finished, show the player that they've won,
        //save progress and move to the next level
        if (isFinished)
        { 
            winText.CrossFadeAlpha(1, 2.0f, false);
            audiosource.Play();
            LevelSave();
            StartCoroutine(WaitForNextScene()); 
        }
    }
    
    private void NextLevel() {
        //If we've reached the last level,
        //disable all unnecessary GameObjects and enable the End text. Otherwise, go on to the next level
        if(SceneManager.GetActiveScene().buildIndex == 30) { 
     
            for(int i = 0; i < toDisableOnEnd.Length; i++)
            {
                toDisableOnEnd[i].SetActive(false); 
            } 
            gameEnd.SetActive(true);
            StartCoroutine(EndGame()); 
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator WaitForNextScene()
    {
        yield return new WaitForSeconds(2f);
        NextLevel();
        
    }

    //Take us to the main menu if the whole game was finished.
    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }

    //Save Progress
    void LevelSave()
    {
        lvl = PlayerPrefs.GetInt("lvl");

        if (lvl >= 30 || lvl < 1)
        {
            lvl = 2;
            PlayerPrefs.SetInt("lvl", lvl);
        }

        else
        {
            lvl++;
            PlayerPrefs.SetInt("lvl", lvl);
        } 
    } 
}
 