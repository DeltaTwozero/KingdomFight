using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldOre : MonoBehaviour
{
    GameManager resources;
    [SerializeField] int gold_amount_local;
    [SerializeField] float respawnTime;
    [SerializeField] AudioClip[] audioClips;
    bool playOnce = true;
    bool scaleDown = false;
    Vector3 scaleChange, originalScale;
    AudioSource source;
    SphereCollider sphereCollider;

    void Start()
    {
        resources = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        source = this.GetComponent<AudioSource>();
        scaleChange = new Vector3(1, 1, 1);
        originalScale = transform.localScale;
        sphereCollider = this.GetComponent<SphereCollider>();

        GenerateGold();
    }

    private void Update()
    {
        if(scaleDown)
        {
            if(transform.localScale.x > 0)
            transform.localScale -= scaleChange * Time.deltaTime;
        }
    }

    void GatherGold()
    {
        StartCoroutine("Respawn", respawnTime);
    }

    void GenerateGold()
    {
        gold_amount_local = Random.Range(2, 10);
    }

    IEnumerator Respawn(float respawnTimeLocal)
    {
        sphereCollider.enabled = false;
        resources.AddGold(gold_amount_local);
        scaleDown = true;
        source.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        yield return new WaitForSeconds(respawnTimeLocal);
        sphereCollider.enabled = true;
        scaleDown = false;
        transform.localScale = originalScale;
        GenerateGold();
    }
}