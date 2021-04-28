using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerUpId;
    [SerializeField]
    private AudioClip _clip;

    //Player Pickup of Powerup variables
    private Transform _player;
    private Rigidbody2D _rigidbody;
    private float _movementSpeed = 6.0f;
    private Vector2 _moveDirection;
    private bool _cKeyPressed = false;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_player == null)
        {
            Debug.LogError("Player is Null!");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (_cKeyPressed == false)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        } 

        if (Input.GetKeyDown(KeyCode.C) && _cKeyPressed == false)
        {
            _cKeyPressed = true;
            transform.Translate(Vector3.zero);
            _moveDirection = (_player.transform.position - transform.position).normalized * _movementSpeed;
            _rigidbody.velocity = new Vector2(_moveDirection.x, _moveDirection.y);
        }
        if(transform.position.y <= -6.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyLaser")
        {
            Destroy(this.gameObject);
        } 
        else if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);

            switch(_powerUpId)
            {
                case 0:
                    player.TripleShot();
                    break;
                case 1:
                    player.SpeedUp();
                    break;
                case 2:
                    player.ShieldUp();
                    break;
                case 3:
                    player.CollectAmmo();
                    break;
                case 4:
                    player.RestoreLife();
                    break;
                case 5:
                    player.MissileActive();
                    break;
                case 6:
                    player.NoAmmoPowerDown();
                    break;
                default:
                    Debug.Log("No Power Up");
                    break;
            }

            Destroy(this.gameObject);
        }
    }

}
