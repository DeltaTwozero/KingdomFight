using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldOre : MonoBehaviour
{
    Resources resources;
    [SerializeField] int gold_amount_local;

    void Start()
    {
        resources = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Resources>();
        gold_amount_local = Random.Range(10, 30);
    }

    void GatherGold()
    {
        resources.SetGold(gold_amount_local);
        print("gold taken");
        Destroy(this.gameObject);
    }
}
