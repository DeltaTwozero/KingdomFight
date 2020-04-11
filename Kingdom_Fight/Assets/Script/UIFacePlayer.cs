using UnityEngine;

public class UIFacePlayer : MonoBehaviour
{
    Transform playerCamera;

    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    void Update()
    {
        LookAtPlayer();
    }

    void LookAtPlayer()
    {
        Vector3 lookDir = this.transform.position - playerCamera.transform.position * 2;
        lookDir.y = 0;
        this.transform.rotation = Quaternion.LookRotation(lookDir);
    }
}