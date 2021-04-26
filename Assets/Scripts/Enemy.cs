using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Player _player;
    private Animator _anim;
    [SerializeField]
    private AudioClip _explosionAudio;
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 3.0f;
    private float _canFire = -1;

    //Randomised Movements Variables
    private float _latestChangeDirectionTime;
    private float _directionChangeTime = 2f;
    private float _enemyVelocity = 3f;
    private Vector2 _movementDirection;
    private Vector2 _movementPerSecond;

    private SpawnManager _spawnManager;

    public bool _enemyIsDestroyed = false;

    private void Start()
    {
        //Randomised Movement
        _latestChangeDirectionTime = 0f;
        CalculateNewMovementVector();

        _player = GameObject.Find("Player").GetComponent<Player>();

        if(_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL");
        }

        _anim = gameObject.GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }

        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("Explosion Audio on Enemy is null");
        } else
        {
            _audioSource.clip = _explosionAudio;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate the frequency of the direction Change
        if (Time.time - _latestChangeDirectionTime > _directionChangeTime && _enemyIsDestroyed == false)
        {
            _latestChangeDirectionTime = Time.time;
            CalculateNewMovementVector();
        }

        transform.position = new Vector2(transform.position.x + (_movementPerSecond.x * Time.deltaTime), transform.position.y + (_movementPerSecond.y * Time.deltaTime));

        if(Time.time > _canFire && _enemyIsDestroyed == false)
        {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
       
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
            
        }

    }

    void CalculateNewMovementVector()
    {
        _movementDirection = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0f)).normalized;
        _movementPerSecond = _movementDirection * _enemyVelocity;

        if (transform.position.y <= -5f || transform.position.y >= 8f || transform.position.x <= -10f || transform.position.x >= 10f)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == ("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            EnemyDestoryed();
        }

        if (other.tag == ("Laser"))
        {

            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(10);
            }
            EnemyDestoryed();
        }

        if (other.tag == "Missile")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(10);
            }
            EnemyDestoryed();
        }
    
    }

    private void EnemyDestoryed()
    {
        _enemyIsDestroyed = true;
        _spawnManager.OnEnemyDeath();
        _anim.SetTrigger("OnEnemyDeath");
        _audioSource.Play();
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, 2.8f);
    }


}
