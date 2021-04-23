using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private bool _isGameOver;
    [SerializeField]
    private bool _paused = false;

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
    }

    private void Unpause()
    {
        Time.timeScale = 1;
        _paused = false;
    }


    public void GameOver()
    {
        _isGameOver = true;
    }
}
