using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    [SerializeField] int goldMax, goldCurrent;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public int GetGold()
    {
        return goldCurrent;
    }

    public void SetGold(int number)
    {
        goldCurrent += number;
    }
}
