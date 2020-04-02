using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksButton : MonoBehaviour
{
    [SerializeField] GameObject barracks;
    [SerializeField] bool isMelee;

    void AddUnit()
    {
        Barracks script = barracks.GetComponent<Barracks>();
        if (isMelee)
            script.AddUnitMeleeCount(1);
        else if (!isMelee)
            script.AddUnitRangedCount(1);
        print("unit added");
    }
}
