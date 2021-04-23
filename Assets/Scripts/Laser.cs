using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private bool _enemyLaser = false;

    // Update is called once per frame
    void Update()
    {

        if (_enemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }


        if (transform.position.y >= 10f || transform.position.y <= -10f)
        {

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }

    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }


    public void EnemyLaser()
    {
        _enemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == ("Player") && _enemyLaser == true)
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }

    }

}
