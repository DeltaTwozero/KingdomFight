using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField] Tower tower;

    void Start()
    {
        tower = GameObject.FindGameObjectWithTag("Tower").GetComponent<Tower>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mob" && !other.gameObject.GetComponent<Mob_Melee>().team1)
        {
            other.gameObject.GetComponent<Mob_Melee>().TakeDamage(tower.ProjectileDamage());
        }
    }
}
