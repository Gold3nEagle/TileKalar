using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController singleton;

    private Ground[] allSquares;

    // Start is called before the first frame update
    void Start()
    {
        SetupLevel();
        
    }

    private void SetupLevel()
    {
        allSquares = FindObjectsOfType<Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if(singleton == null)
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
            NextLevel();
        }
    }

    private void NextLevel() { 
        if(SceneManager.GetActiveScene().buildIndex == 1) { 
    
        SceneManager.LoadScene(0);
    } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
