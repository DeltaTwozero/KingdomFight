using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryStatus : MonoBehaviour
{
    [SerializeField] Castle castle1, castle2; //assign in scene
    bool changeScene = false;
    float time = 10;
    int scene;

    void Update()
    {
        if (castle1.GetCastleHealth() <= 0)
        {
            changeScene = true;
            scene = 3;
        }

        if (castle2.GetCastleHealth() <= 0)
        {
            changeScene = true;
            scene = 2;
        }

        if (changeScene)
        time -= Time.deltaTime;

        if (time < 0)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
