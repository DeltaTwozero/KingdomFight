using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLogic : MonoBehaviour
{
    [SerializeField] bool team1, isRanged;
    [SerializeField] float dmg;
    [SerializeField] Mob_Ranged mobRanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mob" && !isRanged)
        {
            if (other.gameObject.GetComponent<Mob_Melee>() && team1 != other.gameObject.GetComponent<Mob_Melee>().team1)
            {
                other.gameObject.GetComponent<Mob_Melee>().TakeDamage(dmg);
            }

            else if (other.gameObject.GetComponent<Mob_Ranged>().enabled && team1 != other.gameObject.GetComponent<Mob_Ranged>().team1)
            {
                other.gameObject.GetComponent<Mob_Ranged>().TakeDamage(dmg);
            }
        }

        if (other.gameObject.tag == "Mob" && team1 != other.gameObject.GetComponent<Mob_Melee>().team1 && isRanged)
        {
            mobRanged.Attack(other.gameObject.transform);
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
