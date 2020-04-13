using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [SerializeField] float healthMax, healthCurrent;

    //UI
    [SerializeField] Text healthCurrentTXT, respawnTimeCurrentTXT;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip hurt, death_SFX;

    GameManager gameManager;

    void Start()
    {
        if (healthMax <= 0)
            healthMax = 100;
        healthCurrent = healthMax;

        healthCurrentTXT.text = healthCurrent.ToString();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (healthCurrent <= 0)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }

        respawnTimeCurrentTXT.text = gameManager.GetRespawnTime().ToString("F2");
    }

    public void DamageCastle(float damage)
    {
        healthCurrent -= damage;

        if (healthCurrent <= 0)
        {
            audioSource.PlayOneShot(death_SFX);
            healthCurrentTXT.text = healthCurrent.ToString();
        }
        else
        {
            audioSource.PlayOneShot(hurt);
            healthCurrentTXT.text = healthCurrent.ToString();
        }
        
    }

    public float GetCastleHealth()
    {
        return healthCurrent;
    }
}
