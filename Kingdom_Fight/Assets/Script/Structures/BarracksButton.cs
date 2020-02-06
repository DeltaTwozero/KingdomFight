using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksButton : MonoBehaviour
{
    [SerializeField] GameObject barracks;

    void AddMeleeUnit()
    {
        Barracks script = barracks.GetComponent<Barracks>();
        int unit = script.GetUnitMeleeCount();
        script.SetUnitMeleeCount(unit += 1);
        print("succsess!");
    }
}
