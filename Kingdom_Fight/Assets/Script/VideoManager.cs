using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    VideoPlayer videoPlayer;
    AudioSource audioSource;
    GameObject wall;

    void Start()
    {
        videoPlayer = this.GetComponent<VideoPlayer>();
        audioSource = this.GetComponent<AudioSource>();
        wall = this.transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            videoPlayer.Play();
            wall.SetActive(false);
        }
    }
}
