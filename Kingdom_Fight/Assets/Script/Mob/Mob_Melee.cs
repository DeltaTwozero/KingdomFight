using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Mob_Melee : MonoBehaviour
{
    //NavMesh navMesh;
    NavMeshAgent agent;

    [Header("Movement variables")]
    [SerializeField] int currentPoint = 0;
    [SerializeField] float speed;
    public bool team1;
    [SerializeField] GameObject[] pathPoints;
    [SerializeField] TargetLogic targetLogic;

    [SerializeField] float damage, hp, despawnTimer;

    //Audio
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip death_SFX;

    [SerializeField] Armoury armoury;
    [SerializeField] EnemyManager enemyManager;

    //UI
    [SerializeField] Text dmgTXT, hpTXT;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = speed;

        if (team1)
        {
            //Creating a list of path points
            pathPoints = GameObject.FindGameObjectsWithTag("Player1MobPoint");
            pathPoints = pathPoints.OrderBy(point => Vector3.Distance(this.transform.position, point.transform.position)).ToArray();

            //Set Values
            armoury = GameObject.FindGameObjectWithTag("Armoury").GetComponent<Armoury>();
            damage = armoury.GetMeleeDMG();
            hp = armoury.GetMeleeHP();
            targetLogic.SetDMG(damage);
            targetLogic.SetTeam(team1);
        }
        else
        {
            pathPoints = GameObject.FindGameObjectsWithTag("Castle");
            pathPoints = pathPoints.OrderBy(point => Vector3.Distance(this.transform.position, point.transform.position)).ToArray();
            currentPoint = 1;
            enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
            damage = enemyManager.GetMeleeDamage();
            hp = enemyManager.GetMeleeHP();
            targetLogic.SetDMG(damage);
        }

        //Setting UI
        dmgTXT.text = damage.ToString();
        hpTXT.text = hp.ToString();
    }


    void Update()
    {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer < 0)
            Destroy(this.gameObject);

        MoveToPoint();
    }

    void MoveToPoint()
    {
        if (currentPoint > pathPoints.Length)
            currentPoint = 1;

        agent.destination = pathPoints[1].transform.position;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        hpTXT.text = hp.ToString();
        if (hp <= 0)
        {
            targetLogic.enabled = false;
            source.PlayOneShot(death_SFX);
            agent.enabled = false;
            Rigidbody rd = this.GetComponent<Rigidbody>();
            rd.isKinematic = false;
            rd.useGravity = true;
            rd.AddForce(0, 250, 0);
            Destroy(this.gameObject, death_SFX.length);
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
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<Castle>().DamageCastle(damage);
        }
    }
}
