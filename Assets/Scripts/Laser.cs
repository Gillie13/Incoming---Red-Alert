using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _laserSpeed = 8.0f;
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
    }


    public void AssignEnemyLaser()
    {
        _enemyLaser = true;
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);
        if (transform.position.y > 8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void MoveDown()
    {
                transform.Translate(Vector3.down * _laserSpeed * Time.deltaTime);
        if(transform.position.y < -8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
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

        }

    }

}
