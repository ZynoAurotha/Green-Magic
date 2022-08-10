using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    //private static bool GameIsPause = false;

    [SerializeField] private GameObject _pauseMenu;
    // Start is called before the first frame update
    [SerializeField] private GameObject _gameOverScene;

    private bool _gameIsOver = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnPauseGame();
        }
        if(_gameIsOver)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(1);
            }
        }      
    }

    public void OnDelayBeforeGameOver()
    {
        Invoke("OnGameOver",1f);
    }

    public void OnGameOver()
    {
        _gameOverScene.SetActive(true);
        Time.timeScale = 0f;
        _gameIsOver = true;
    }

    public void OnPauseGame()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        //GameIsPause = true;
    }

    public void OnResumeGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        //GameIsPause = false;
    }

    public void OnQuitGame()
    {
        _pauseMenu.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        //GameIsPause = true;
    }
}
