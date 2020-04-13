using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAOE : MonoBehaviour
{
    [SerializeField] Tower tower;

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Mob" && !other.gameObject.GetComponent<Mob_Melee>().team1)
        //{
        //    tower.Evaporate(other.gameObject.transform);
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Mob" && !other.gameObject.GetComponent<Mob_Melee>().team1)
        {
            tower.Evaporate(other.gameObject.transform);
        }
    }
}
