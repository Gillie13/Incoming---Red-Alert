using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrusterBar : MonoBehaviour
{
    private Image _barImage;
    public const int thrusterMax = 100;
    public float thrusterFuel = 0;
    private float _thrusterRefillAmount = 30.0f;


    // Start is called before the first frame update
    void Start()
    {
        _barImage = transform.Find("Bar").GetComponent<Image>();

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {

        }
        else
        {
            thrusterFuel += _thrusterRefillAmount * Time.deltaTime;
            thrusterFuel = Mathf.Clamp(thrusterFuel, 0f, thrusterMax);
        }

        _barImage.fillAmount = GetThrusterNormalised();
    }

    public void UseThruster(int amount)
    {
        if (thrusterFuel >= amount)
        {
            thrusterFuel -= amount;
        }
        else
        {
            thrusterFuel = 0;
        }
    }

    public float GetThrusterNormalised()
    {
        return thrusterFuel / thrusterMax;
    }
}
