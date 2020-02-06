using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mob_Melee : MonoBehaviour
{
    NavMeshAgent agent;
    //NavMesh navMesh;
    //CapsuleCollider collider;

    [Header("Movement variables")]
    [SerializeField] int currentPoint = 0;
    [SerializeField] float speed, destroyTimer;
    [SerializeField] List<GameObject> pathPoints;


    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        //collider = this.GetComponent<CapsuleCollider>();
        destroyTimer = 10f;

        //Creating a list of path points
        pathPoints = new List<GameObject>();
        GameObject dest1 = GameObject.Find("Destination1");
        pathPoints.Add(dest1);
        GameObject dest2 = GameObject.Find("DestinationFinal");
        pathPoints.Add(dest2);

        //pathPoints.Add(GameObject.FindGameObjectWithTag("PathPoint"));
    }


    void Update()
    {
        agent.speed = speed;
        destroyTimer -= Time.deltaTime;
        //agent.Move(agent.destination);
        MoveToPoint();

        if (destroyTimer <= 0)
            Destroy(this.gameObject);
    }

    void MoveToPoint()
    {
        if (Vector3.Distance(agent.transform.position, pathPoints[currentPoint].transform.position) < 0.5f)
        {
            currentPoint++;
            destroyTimer = 10f;
        }

        if (currentPoint >= pathPoints.Count)
            currentPoint = 0;

        agent.destination = pathPoints[currentPoint].transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PathPoint"))
        {
            Destroy(this.gameObject);
        }
    }
}
