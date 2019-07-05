using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController singleton; 
    public Text winText;

    AudioSource audiosource;
    int lvl;

    private Ground[] allSquares;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        SetupLevel(); 
    }
     
    public void SetupLevel()
    { 
        allSquares = FindObjectsOfType<Ground>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

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

        if (isFinished)
        { 
            winText.CrossFadeAlpha(1, 2.0f, false);
            audiosource.Play();
            LevelSave();
            StartCoroutine(WaitForNextScene());
            
        }
    }

    private void NextLevel() { 
        if(SceneManager.GetActiveScene().buildIndex == 13) { 
    
        SceneManager.LoadScene(0);
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

    void LevelSave()
    {
        lvl = PlayerPrefs.GetInt("lvl");

        if (lvl >= 10 || lvl < 1)
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
