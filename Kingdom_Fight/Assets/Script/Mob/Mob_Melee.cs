using System.Collections;
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
        MoveToPoint();

        despawnTimer -= Time.deltaTime;
        if (despawnTimer < 0)
            Destroy(this.gameObject);
    }

    void MoveToPoint()
    {
        if (currentPoint > pathPoints.Length)
            currentPoint = 0;

        agent.destination = pathPoints[currentPoint].transform.position;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        hpTXT.text = hp.ToString();
        if (hp <= 0)
        {
            source.PlayOneShot(death_SFX);
            agent.enabled = false;
            Rigidbody rd = this.GetComponent<Rigidbody>();
            rd.AddForce(0, 500, 0);
            //CapsuleCollider collider = this.GetComponent<CapsuleCollider>();
            //collider.enabled = false;
            targetLogic.enabled = false;
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
            collision.gameObject.GetComponent<Castle>().DamageCastle(damage);
            Destroy(this.gameObject);
        }

        //if (collision.gameObject.tag == "Mob" && team1 != collision.gameObject.GetComponent<Mob_Melee>().team1)
        //{
        //    collision.gameObject.GetComponent<Mob_Melee>().TakeDamage(damage);
        //}
    }
}
