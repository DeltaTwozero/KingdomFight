using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Mob_Melee : MonoBehaviour
{
    NavMeshAgent agent;
    //NavMesh navMesh;

    [Header("Movement variables")]
    [SerializeField] int currentPoint = 0;
    [SerializeField] float speed, destroyTimer;
    public bool team1;
    [SerializeField] GameObject[] pathPoints;

    [SerializeField] int damage = 10;
    [SerializeField] AudioSource source;
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        destroyTimer = 10f;

        //Creating a list of path points
        if (team1)
        {
            pathPoints = GameObject.FindGameObjectsWithTag("Player1MobPoint");
            pathPoints = pathPoints.OrderBy(point => Vector3.Distance(this.transform.position, point.transform.position)).ToArray();
        }
            


        //GameObject dest1 = GameObject.Find("Destination1");
        //pathPoints.Add(dest1);
        //GameObject dest2 = GameObject.Find("DestinationFinal");
        //pathPoints.Add(dest2);

        //pathPoints.Add(GameObject.FindGameObjectWithTag("PathPoint"));
    }


    void Update()
    {
        agent.speed = speed;
        destroyTimer -= Time.deltaTime;
        MoveToPoint();
    }

    void MoveToPoint()
    {
        if (currentPoint >= pathPoints.Length)
            currentPoint = 0;

        agent.destination = pathPoints[currentPoint].transform.position;
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
