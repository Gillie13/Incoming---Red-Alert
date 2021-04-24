using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Transform _target;
    private GameObject _closet = null;
    private Rigidbody2D _rigidBody;
    private float _angleChangingSpeed = 200f;
    private float _movementSpeed = 6.0f;


    // Start is called before the first frame update
    void Start()
    {
        FindClosetEnemy();
        _target = _closet.transform;
        _rigidBody = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        if(_target != null)
        {
            Vector2 direction = (Vector2)_target.position - _rigidBody.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            _rigidBody.angularVelocity = _angleChangingSpeed * rotateAmount;
            _rigidBody.velocity = transform.up * _movementSpeed;
            Destroy(this.gameObject, 4f);
        } else
        {
            Destroy(this.gameObject);
        }
    }

    public GameObject FindClosetEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                _closet = go;
                distance = curDistance;
            }
        }
        return _closet;
    }
}
