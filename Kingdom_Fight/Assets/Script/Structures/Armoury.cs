using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Armoury : MonoBehaviour
{
    [SerializeField] int meleeDamage, rangedDamage, meleeHP, rangedHP;
    [SerializeField] Text melee_DMG_TXT, ranged_DMG_TXT, melee_HP_TXT, ranged_HP_TXT;

    void Start()
    {
        melee_DMG_TXT.text = meleeDamage.ToString();
        ranged_DMG_TXT.text = rangedDamage.ToString();
        melee_HP_TXT.text = meleeHP.ToString();
        ranged_HP_TXT.text = rangedHP.ToString();
    }

    #region Melee Variables
    public int GetMeleeDMG()
    {
        return meleeDamage;
    }

    public void AddMeleeDMG(int number)
    {
        meleeDamage += number;
        melee_DMG_TXT.text = meleeDamage.ToString();
    }

    public int GetMeleeHP()
    {
        return meleeHP;
    }

    public void AddMeleeHP(int amount)
    {
        meleeHP += amount;
        melee_HP_TXT.text = meleeHP.ToString();
    }
    #endregion

    #region Ranged Variables
    public int GetRangedDMG()
    {
        return rangedDamage;
    }

    public void AddRangedDMG(int number)
    {
        rangedDamage += number;
        ranged_DMG_TXT.text = rangedDamage.ToString();
    }

    public int GetRangedHP()
    {
        return rangedHP;
    }

    public void AddRangedHP(int amount)
    {
        rangedHP += amount;
        ranged_HP_TXT.text = rangedHP.ToString();
    }
    #endregion
}
