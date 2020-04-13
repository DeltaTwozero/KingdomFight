using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    //Variables
    [SerializeField] int cost;
    [SerializeField] float cooldown, spawnForce, projectileDamage;
    [SerializeField] bool isActive;
    [SerializeField] GameObject spawnPos, projectile, button;

    //UI
    [SerializeField] Text headerTXT, costTXT;

    //Gold
    [SerializeField] GameManager gameManager;
    
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        costTXT.text = cost.ToString();
        //isActive = false;
    }

    public void Evaporate(Transform coordinates)
    {
        if (isActive)
        {
            GameObject clone = Instantiate(projectile, spawnPos.transform.position, Quaternion.identity);
            Vector3 dir = coordinates.transform.position - clone.transform.position;
            dir = dir.normalized;
            clone.GetComponent<Rigidbody>().AddForce(dir * spawnForce);
        }
    }

    public float ProjectileDamage()
    {
        return projectileDamage;
    }

    public IEnumerator ActivateTower()
    {
        if (cost <= gameManager.GetGold())
        {
            gameManager.RemoveGold(cost);
            button.SetActive(false);
            isActive = true;
            headerTXT.color = Color.red;

            yield return new WaitForSeconds(cooldown);

            button.SetActive(true);
            isActive = false;
            headerTXT.color = Color.green;
            cost += cost;
            costTXT.text = cost.ToString();
        }
    }
}
