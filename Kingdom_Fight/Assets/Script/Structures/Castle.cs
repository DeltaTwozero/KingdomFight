using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [SerializeField] int healthMax, healthCurrent;

    //UI
    [SerializeField] Text healthCurrentText;

    void Start()
    {
        if (healthMax <= 0)
            healthMax = 100;
        healthCurrent = healthMax;
    }

    void Update()
    {
        healthCurrentText.text = healthCurrent.ToString();
        if (healthCurrent <= 0)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }
    }

    public void DamageCastle(int damage)
    {
        healthCurrent -= damage;
    }

    public int GetCastleHealth()
    {
        return healthCurrent;
    }
}
