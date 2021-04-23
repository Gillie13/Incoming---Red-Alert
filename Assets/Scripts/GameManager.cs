using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private bool _isGameOver;
    [SerializeField]
    private bool _paused = false;
    private UIManager _uIManager;

    private void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uIManager == null)
        {
            Debug.LogError("UIManager is NULL");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if(_paused == false)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }


    }

    private void Pause()
    {
        Time.timeScale = 0;
        _paused = true;
        _uIManager.GamePausedSequence();
        
    }

    private void Unpause()
    {
        Time.timeScale = 1;
        _paused = false;
        _uIManager.GameUnPausedSequence();
    }


    public void GameOver()
    {
        _isGameOver = true;
    }


}
