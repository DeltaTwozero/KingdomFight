using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int goldMax, goldCurrent;
    [SerializeField] float unit_spawn_time_default, unit_spawn_time_current;
    [SerializeField] Barracks barracks1;

    void Start()
    {

    }

    void Update()
    {
        unit_spawn_time_current -= Time.deltaTime;
        if (unit_spawn_time_current <= 0)
        {
            barracks1.SpawnUnits();

            unit_spawn_time_current = unit_spawn_time_default;
        }
    }

    public int GetGold()
    {
        return goldCurrent;
    }

    public void AddGold(int number)
    {
        goldCurrent += number;
    }

    public void RemoveGold(int number)
    {
        goldCurrent -= number;
    }

    public float GetRespawnTime()
    {
        return unit_spawn_time_current;
    }
}
