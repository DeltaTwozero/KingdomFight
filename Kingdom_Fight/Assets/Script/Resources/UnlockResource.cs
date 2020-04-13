using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockResource : MonoBehaviour
{
    [SerializeField] int price;
    [SerializeField] GameObject vein, sign;

    //UI
    [SerializeField] Text priceTXT;

    //Audio
    [SerializeField] AudioClip unlocked, locked;
    AudioSource source;

    //Access to gold
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        source = this.GetComponent<AudioSource>();

        priceTXT.text = price.ToString();

        sign.SetActive(true);
        vein.SetActive(false);
    }

    void UnlockRes()
    {
        if (price <= gameManager.GetGold())
            StartCoroutine("Unlock");
        else
            source.PlayOneShot(locked);
    }

    IEnumerator Unlock()
    {
        gameManager.RemoveGold(price);
        vein.SetActive(true);
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        source.PlayOneShot(unlocked);
        yield return new WaitForSeconds(unlocked.length);
        sign.SetActive(false);

    }
}
