using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFire : MonoBehaviour
{

    private Transform _player;
    private Rigidbody2D _rigidBody;
    [SerializeField]
    private float _particleFireSpeed = 7f;
    private Vector2 _moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rigidBody = GetComponent<Rigidbody2D>();

        if (_player == null)
        {
            Debug.LogError("Player is Null!");
        }

        _moveDirection = (_player.transform.position - transform.position).normalized * _particleFireSpeed;
        _rigidBody.velocity = new Vector2(_moveDirection.x, _moveDirection.y);
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
        }
    }
}
