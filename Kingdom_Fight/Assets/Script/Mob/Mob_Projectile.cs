using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Projectile : MonoBehaviour
{
    [SerializeField] Mob_Ranged mobRanged;
    [SerializeField] bool team1;

    void Start()
    {
        mobRanged = GameObject.FindGameObjectWithTag("Mob").GetComponent<Mob_Ranged>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mob" && team1 != other.gameObject.GetComponent<Mob_Melee>().team1)
        {
            other.gameObject.GetComponent<Mob_Melee>().TakeDamage(mobRanged.GetDamage());
        }

        if (other.gameObject.tag == "Mob" && team1 != other.gameObject.GetComponent<Mob_Ranged>().team1)
        {
            other.gameObject.GetComponent<Mob_Ranged>().TakeDamage(mobRanged.GetDamage());
        }
    }
}
