using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    // handle to Text
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Text _gameoverText;
    [SerializeField]
    private Text _restartGameText;
    [SerializeField]
    private Sprite[] _liveSprites;
    private GameManager _gameManager;


    
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "SCORE: " + 0;
        _gameoverText.gameObject.SetActive(false);
        _restartGameText.gameObject.SetActive(false);

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("GameManager is Null");
        }
    }


    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "SCORE: " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _liveSprites[currentLives];
        if(currentLives == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        _gameoverText.gameObject.SetActive(true);
        _restartGameText.gameObject.SetActive(true);
        _gameManager.GameOver();

        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        while (true)
        {
            _gameoverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameoverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        
    }
}
