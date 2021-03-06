using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    private Transform _transform;
    private float _duration = 0.0f;
    private float _strength = 0.3f;
    private float _remainingDuration = 1.0f;
    private Vector3 _cameraPosition = new Vector3(0,1,-10);

    private void Awake()
    {
        if (_transform == null)
        {
            
            _transform = GetComponent(typeof(Transform)) as Transform;

        }
    }

    private void OnEnabled()
    {
        _cameraPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(_duration > 0.0f)
        {
            transform.localPosition = _cameraPosition + Random.insideUnitSphere * _strength;
            _duration -= Time.deltaTime * _remainingDuration;
        } else
        {
            _duration = 0.0f;
            transform.localPosition = _cameraPosition;
        }
    }

    public void StartShake()
    {
        _duration = 0.5f;
    }
}
