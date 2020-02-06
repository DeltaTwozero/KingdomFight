using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGold : MonoBehaviour
{
    Resources resources;
    int gold_amount_local;
    [SerializeField] Text gold_amountTXT;

    void Start()
    {
        resources = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Resources>();
    }

    void Update()
    {
        gold_amount_local = resources.GetGold();
        gold_amountTXT.text = gold_amount_local.ToString();
    }
}
