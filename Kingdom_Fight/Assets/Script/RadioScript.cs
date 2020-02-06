using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScript : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip clip;
    bool playOnce, countdownStart;
    float clipLength;
    [SerializeField] GameObject exitLVL;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        playOnce = true;
        countdownStart = false;
        clipLength = clip.length;
        exitLVL = GameObject.Find("Exit");
        exitLVL.SetActive(false);
    }

    private void Update()
    {
        if(countdownStart)
        clipLength -= Time.deltaTime;

        if (clipLength < 0)
        {
            playOnce = true;
            exitLVL.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && playOnce)
        {
            audioSource.PlayOneShot(clip);
            playOnce = false;
            countdownStart = true;
        }
    }
}
