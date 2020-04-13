using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour
{
    [SerializeField] Tower tower;

    void UseTower()
    {
        tower.StartCoroutine("ActivateTower");
    }
}
