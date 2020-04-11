using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [SerializeField] int healthMax, healthCurrent;

    //UI
    [SerializeField] Text healthCurrentTXT, respawnTimeCurrentTXT;

    AudioSource audioSource;
    [SerializeField] AudioClip hurt;

    GameManager gameManager;

    void Start()
    {
        if (healthMax <= 0)
            healthMax = 100;
        healthCurrent = healthMax;

        audioSource = this.GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        healthCurrentTXT.text = healthCurrent.ToString();
        if (healthCurrent <= 0)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }

        respawnTimeCurrentTXT.text = gameManager.GetRespawnTime().ToString("F2");
    }

    public void DamageCastle(int damage)
    {
        healthCurrent -= damage;
        audioSource.PlayOneShot(hurt);
    }

    public int GetCastleHealth()
    {
        return healthCurrent;
    }
}
