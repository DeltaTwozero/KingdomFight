﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Mob_Melee : MonoBehaviour
{
    NavMeshAgent agent;
    //NavMesh navMesh;

    [Header("Movement variables")]
    [SerializeField] int currentPoint = 0;
    [SerializeField] float speed;
    public bool team1;
    [SerializeField] GameObject[] pathPoints;

    [SerializeField] int damage, hp;

    //Audio
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip death_SFX;

    [SerializeField] Armoury armoury;

    //UI
    [SerializeField] Text dmgTXT, hpTXT;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = speed;

        //Creating a list of path points
        if (team1)
        {
            pathPoints = GameObject.FindGameObjectsWithTag("Player1MobPoint");
            pathPoints = pathPoints.OrderBy(point => Vector3.Distance(this.transform.position, point.transform.position)).ToArray();
        }

        armoury = GameObject.FindGameObjectWithTag("Armoury").GetComponent<Armoury>();
        damage = armoury.GetMeleeDMG();
        hp = armoury.GetMeleeHP();

        //Setting UI
        dmgTXT.text = damage.ToString();
        hpTXT.text = hp.ToString();

        //GameObject dest1 = GameObject.Find("Destination1");
        //pathPoints.Add(dest1);
        //GameObject dest2 = GameObject.Find("DestinationFinal");
        //pathPoints.Add(dest2);

        //pathPoints.Add(GameObject.FindGameObjectWithTag("PathPoint"));
    }


    void Update()
    {
        MoveToPoint();
    }

    void MoveToPoint()
    {
        if (currentPoint >= pathPoints.Length)
            currentPoint = 0;

        agent.destination = pathPoints[currentPoint].transform.position;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hpTXT.text = hp.ToString();
        if (hp <= 0)
        {
            source.PlayOneShot(death_SFX);
            agent.enabled = false;
            Destroy(this.gameObject, death_SFX.length + .1f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (team1)
        {
            if (other.gameObject.CompareTag("Player1MobPoint"))
            {
                currentPoint++;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Castle")
        {
            collision.gameObject.GetComponent<Castle>().DamageCastle(damage);
            Destroy(this.gameObject);
        }
    }
}
