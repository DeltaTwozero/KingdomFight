using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barracks : MonoBehaviour
{
    [SerializeField] int unit_melee_count, unit_range_count, unit_melee_count_MAX, unit_range_count_MAX, melee_price, ranged_price;
    [SerializeField] GameObject spawnPos, meleeButton, rangedButton;
    [SerializeField] List<GameObject> unitList;
    [SerializeField] bool team1; //Set team
    GameManager resources;

    //UI
    [SerializeField] Text meleeText, rangedText, meleeCount, rangedCount, meleePriceText, rangedPriceText;

    void Start()
    {
        resources = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (team1)
        {
            Mob_Melee melee = unitList[0].GetComponent<Mob_Melee>();
            melee.team1 = team1;
        }

        meleePriceText.text = melee_price.ToString();
        rangedPriceText.text = ranged_price.ToString();
    }

    void Update()
    {
        if (unit_melee_count == unit_melee_count_MAX)
        {
            Destroy(meleeButton);
            meleeText.text = "Melee MAX";
        }

        if (unit_range_count == unit_range_count_MAX)
        {
            Destroy(rangedButton);
            rangedText.text = "Ranged MAX";
        }
    }

    public void SpawnUnits()
    {
        for (int i = 1; i <= unit_melee_count; i++)
        {
            if(i == 1)
                Instantiate(unitList[0], spawnPos.transform.position, spawnPos.transform.rotation);
            else
                Instantiate(unitList[0], new Vector3(spawnPos.transform.position.x + i/2, spawnPos.transform.position.y, spawnPos.transform.position.z), spawnPos.transform.rotation);

            print(i);
        }
    }

    #region Melee unit get;set;
    public int GetUnitMeleeCount()
    {
        return unit_melee_count;
    }

    public void AddUnitMeleeCount(int number)
    {
        if (melee_price > resources.GetGold())
            print("Not enough gold");
        else
        {
            resources.RemoveGold(melee_price);
            unit_melee_count += number;
            melee_price += 10;
            meleeCount.text = unit_melee_count.ToString();
            meleePriceText.text = melee_price.ToString();
        }
    }
    #endregion

    #region Ranged unit get;set;
    public int GetUnitRangedCount()
    {
        return unit_range_count;
    }

    public void AddUnitRangedCount(int number)
    {
        if (ranged_price > resources.GetGold())
            print("Not enough gold");
        else
        {
            resources.RemoveGold(ranged_price);
            unit_range_count += number;
            ranged_price += 10;
            rangedCount.text = unit_range_count.ToString();
            rangedPriceText.text = ranged_price.ToString();
        }
    }
    #endregion
}
