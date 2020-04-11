using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGold : MonoBehaviour
{
    GameManager resources;
    int gold_amount_local;
    [SerializeField] Text gold_amountTXT, gold_amount_MAX_TXT;

    void Start()
    {
        resources = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gold_amount_MAX_TXT.text = resources.GetGoldMAX().ToString();
    }

    void Update()
    {
        gold_amount_local = resources.GetGold();
        gold_amountTXT.text = gold_amount_local.ToString();
    }
}
