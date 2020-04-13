using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLogic : MonoBehaviour
{
    [SerializeField] bool team1;
    [SerializeField] float dmg;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mob" && team1 != other.gameObject.GetComponent<Mob_Melee>().team1)
        {
            other.gameObject.GetComponent<Mob_Melee>().TakeDamage(dmg);
        }
    }

    public void SetTeam(bool team)
    {
        team1 = team;
    }

    public void SetDMG(float damage)
    {
        dmg = damage;
    }
}
