using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Stats
    [SerializeField] int meleeDamage, rangedDamage, meleeHP, rangedHP, enemyMeleeCount, enemyMeleeCountMAX, enemyRangedCount, enemyRangedCountMAX;

    //Unit list
    [SerializeField] List<GameObject> unitList;
    //[SerializeField] Mob_Melee melee;

    [SerializeField] GameObject spawnPos;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SpawnUnits()
    {
        DamageUp();
        HealthUp();
        CountUp();


        for (int i = 1; i <= enemyMeleeCount; i++)
        {
            if (i == 1)
            {
                Mob_Melee melee = unitList[0].GetComponent<Mob_Melee>();
                melee.team1 = false;
                Instantiate(unitList[0], spawnPos.transform.position, spawnPos.transform.rotation);
            }
            else
            {
                Mob_Melee melee = unitList[0].GetComponent<Mob_Melee>();
                melee.team1 = false;
                Instantiate(unitList[0], new Vector3(spawnPos.transform.position.x + i, spawnPos.transform.position.y, spawnPos.transform.position.z), spawnPos.transform.rotation);
            }

            print(i);
        }

        //for (int i = 1; i <= enemyRangedCount; i++)
        //{
        //    if (i == 1)
        //        Instantiate(unitList[1], spawnPos.transform.position, spawnPos.transform.rotation);
        //    else
        //        Instantiate(unitList[1], new Vector3(spawnPos.transform.position.x + i / 2, spawnPos.transform.position.y, spawnPos.transform.position.z), spawnPos.transform.rotation);

        //    print(i);
        //}
    }

    void DamageUp()
    {
        Roll();
        if (Roll())
        {
            if (meleeDamage < 100)
                meleeDamage += 5;
        }
        else
        {
            if(rangedDamage < 50)
                rangedDamage += 5;
        }

    }

    void HealthUp()
    {
        Roll();
        if (Roll())
        {
            if (meleeHP < 100)
                meleeHP += 5;
        }
        else
        {
            if(rangedHP < 75)
                rangedHP += 5;
        }
    }

    void CountUp()
    {
        Roll();
        if (Roll())
        {
            if (enemyMeleeCount < enemyMeleeCountMAX)
                enemyMeleeCount += 1;
        }
        else
        {
            if(enemyRangedCount < enemyRangedCountMAX)
                enemyRangedCount += 1;
        }
    }

    bool Roll()
    {
        int roll = Random.Range(0, 2);
        bool rollResult;

        if (roll == 0)
            rollResult = false;
        else
            rollResult = true;

        print(roll);
        print(rollResult);
        return rollResult;
    }

    #region Get Melee Stats
    public int GetMeleeDamage()
    {
        return meleeDamage;
    }

    public int GetMeleeHP()
    {
        return meleeHP;
    }
    #endregion

    #region Get Ranged Stats
    public int GetRangedDamage()
    {
        return rangedDamage;
    }

    public int GetRangedHP()
    {
        return rangedHP;
    } 
    #endregion
}
