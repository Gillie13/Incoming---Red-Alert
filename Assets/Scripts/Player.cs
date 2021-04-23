using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.2f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    private bool _isSpeedUpActive = false;
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _shieldVisualiser;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _damageLeft, _damageRight;
    [SerializeField]
    private AudioClip _laserAudio;
    [SerializeField]
    private AudioClip _explosionAudio;
    private AudioSource _audioSource;



    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL!");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("UIManager is NULL!");
        }

        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on the Player is NULL");
        } 


    }
    // Update is called once per frame
    void Update()
    {

        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = 7.0f;
        }
        else
        {
            _speed = 3.5f;
        }

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);


        transform.Translate(direction * _speed * Time.deltaTime);
        
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.2f, 0), 0);


        if (transform.position.x >= 3f)
        {
            transform.position = new Vector3(-3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -3f)
        {
            transform.position = new Vector3(3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.58f, 0), Quaternion.identity);
        }

        _audioSource.clip = _laserAudio;
        _audioSource.Play();

    }

    public void Damage()
    {
        if( _isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualiser.SetActive(false);
            return;
        }

        _lives--;
        _uiManager.UpdateLives(_lives);

        if (_lives == 2)
        {
            _damageLeft.SetActive(true);
            _audioSource.clip = _explosionAudio;
            _audioSource.Play();

        } else if (_lives == 1)
        {

            _damageRight.SetActive(true);
            _audioSource.clip = _explosionAudio;
            _audioSource.Play();
        }

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject,1f);
            _audioSource.clip = _explosionAudio;
            _audioSource.Play();

        }
    }

    public void TripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotActive());
    }

    IEnumerator TripleShotActive()
    {
        while (_isTripleShotActive == true)
        {
            yield return new WaitForSeconds(5);
            _isTripleShotActive = false;

        }
    }

    public void SpeedUp()
    {
        _isSpeedUpActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedUpActive());

    }

    IEnumerator SpeedUpActive()
    {
        yield return new WaitForSeconds(5);
        _isSpeedUpActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldUp()
    {
        _isShieldActive = true;
        _shieldVisualiser.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }


}
