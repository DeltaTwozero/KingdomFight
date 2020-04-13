using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int goldMax, goldCurrent;
    [SerializeField] float unit_spawn_time_default, unit_spawn_time_current;
    [SerializeField] Barracks barracks1, barracks2;
    [SerializeField] EnemyManager enemyManager;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        unit_spawn_time_current -= Time.deltaTime;
        
        if (unit_spawn_time_current <= 0)
        {
            barracks1.SpawnUnits();
            enemyManager.SpawnUnits();

            unit_spawn_time_current = unit_spawn_time_default;
        }
    }


    #region Gold get; set;
    public int GetGoldMAX()
    {
        return goldMax;
    }

    public int GetGold()
    {
        return goldCurrent;
    }

    public void AddGold(int number)
    {
        goldCurrent += number;
        if (goldCurrent > goldMax)
            goldCurrent = goldMax;
    }

    public void RemoveGold(int number)
    {
        goldCurrent -= number;
    } 
    #endregion

    public float GetRespawnTime()
    {
        return unit_spawn_time_current;
    }
}
