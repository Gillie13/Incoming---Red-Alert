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



    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= -6.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
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
                default:
                    Debug.Log("No Power Up");
                    break;
            }

            Destroy(this.gameObject);
        }
    }
}
