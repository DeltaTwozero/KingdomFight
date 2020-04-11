using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmouryButton : MonoBehaviour
{
    [SerializeField] GameObject armoury;
    [SerializeField] bool upgradeDMG, isMelee;
    [SerializeField] int melee_DMG_price, melee_HP_price, ranged_DMG_price, ranged_HP_price;
    [SerializeField] Text upgradeTXT;
    GameManager resources;

    void Start()
    {
        resources = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (isMelee && upgradeDMG)
        {
            upgradeTXT.text = melee_DMG_price.ToString();
        }

        else if (!isMelee && upgradeDMG)
        {
            upgradeTXT.text = ranged_DMG_price.ToString();
        }

        else if (isMelee && !upgradeDMG)
        {
            upgradeTXT.text = melee_HP_price.ToString();
        }
        else if (!isMelee && !upgradeDMG)
        {
            upgradeTXT.text = ranged_HP_price.ToString();
        }
    }

    void Upgrade()
    {
        Armoury script = armoury.GetComponent<Armoury>();

        if (isMelee && upgradeDMG && melee_DMG_price < resources.GetGold())
        {
            resources.RemoveGold(melee_DMG_price);
            script.AddMeleeDMG(5);
            melee_DMG_price += 5;
            upgradeTXT.text = melee_DMG_price.ToString();
        }

        else if (!isMelee && upgradeDMG && ranged_DMG_price < resources.GetGold())
        {
            resources.RemoveGold(ranged_DMG_price);
            script.AddRangedDMG(5);
            ranged_DMG_price += 5;
            upgradeTXT.text = ranged_DMG_price.ToString();
        }

        else if (isMelee && !upgradeDMG && melee_HP_price < resources.GetGold())
        {
            resources.RemoveGold(melee_HP_price);
            script.AddMeleeHP(5);
            melee_HP_price += 5;
            upgradeTXT.text = melee_HP_price.ToString();
        }
        else if (!isMelee && !upgradeDMG && ranged_HP_price < resources.GetGold())
        {
            resources.RemoveGold(ranged_HP_price);
            script.AddRangedHP(5);
            ranged_HP_price += 5;
            upgradeTXT.text = ranged_HP_price.ToString();
        }

        print("unit upgraded");
    }
}
