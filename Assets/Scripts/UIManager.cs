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
    private Text _ammoCount;
    [SerializeField]
    private Text _waveNumberText;
    public bool displayWaveNumber;
    [SerializeField]
    private Text _gameoverText;
    [SerializeField]
    private Text _gameWonText;
    [SerializeField]
    private Text _restartGameText;
    [SerializeField]
    private Text _gamePausedText;
    [SerializeField]
    private Text _unpauseGameText;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesImage;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "SCORE: " + 0;
        _ammoCount.text = "Ammo: " + 15 + "/15";
        _gameoverText.gameObject.SetActive(false);
        _restartGameText.gameObject.SetActive(false);
        _waveNumberText.gameObject.SetActive(false);
        _gamePausedText.gameObject.SetActive(false);
        _unpauseGameText.gameObject.SetActive(false);

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

    public void UpdateAmmo(int ammoCount)
    {
        _ammoCount.text = "Ammo: " + ammoCount.ToString() + "/15";
    }

    public void UpdateLives(int currentLives)
    {
        if (currentLives >= 1)
        {
            _livesImage.sprite = _liveSprites[currentLives];
        } else
        {
            GameOverSequence();
        }
    }

    public void WaveText(int waveNumber)
    {
        _waveNumberText.text = "WAVE " + waveNumber.ToString();
        StartCoroutine(DisplayWaveNumber());
    }

    IEnumerator DisplayWaveNumber()
    {
        while (true)
        {
            if (displayWaveNumber)
            {
                _waveNumberText.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.3f);
                _waveNumberText.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.3f);
            }

            yield return null;
        }
    }

    private void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameoverText.gameObject.SetActive(true);
        _restartGameText.gameObject.SetActive(true);
        StartCoroutine(FlashingGameOver());
    }

    public void GameWonSequence()
    {
        _gameManager.GameOver();
        _gameWonText.gameObject.SetActive(true);
        _restartGameText.gameObject.SetActive(true);
        StartCoroutine(FlashingGameWon());
    }

    public void GamePausedSequence()
    {
        _gamePausedText.gameObject.SetActive(true);
        _unpauseGameText.gameObject.SetActive(true);
        
    }

    public void GameUnPausedSequence()
    {
        _gamePausedText.gameObject.SetActive(false);
        _unpauseGameText.gameObject.SetActive(false);

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

    IEnumerator FlashingGameOver()
    {
        while (true)
        {
            _gameoverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            _gameoverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator FlashingGameWon()
    {
        while (true)
        {
            _gameWonText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            _gameWonText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
    }



}
