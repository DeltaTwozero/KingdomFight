using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
    [SerializeField] int unit_melee_count, unit_range_count;
    [SerializeField] float unit_spawn_time, unit_spawn_time_current;
    [SerializeField] GameObject spawnPos;
    [SerializeField] List<GameObject> unitList;

    void Start()
    {
        unit_spawn_time_current = unit_spawn_time;
    }


    void Update()
    {
        unit_spawn_time_current -= Time.deltaTime;
        if (unit_melee_count > 0 && unit_spawn_time_current <= 0)
        {
            for (int i = 0; i < unit_melee_count; i++)
            {
                Instantiate(unitList[0], spawnPos.transform.position, spawnPos.transform.rotation);
                i++;
                print(i);
            }

            unit_spawn_time_current = unit_spawn_time;
        }
    }

    #region Melee unit get;set
    public int GetUnitMeleeCount()
    {
        return unit_melee_count;
    }

    public void SetUnitMeleeCount(int number)
    {
        unit_melee_count += number;
    }
    #endregion
}
