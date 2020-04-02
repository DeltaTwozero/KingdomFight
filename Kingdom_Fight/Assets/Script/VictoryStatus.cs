using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryStatus : MonoBehaviour
{
    [SerializeField] Castle castle1, castle2; //assign in scene
    bool changeScene = false;
    float time = 10;
    void Update()
    {
        if (castle1.GetCastleHealth() <= 0)
        {
            changeScene = true;
            //add victory scene
        }

        if (castle2.GetCastleHealth() <= 0)
        {
            changeScene = true;
            //add loose scene
        }

        if(changeScene)
        time -= Time.deltaTime;

        if (time < 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
