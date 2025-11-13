using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{

    // Use this for initialization

    public Transform[] patrolPoints;
    Transform currentPatrolPoint;
    public Transform player;
    int currentPatrolIndex;
    public float speed;


    void Start()
    {
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 direction = currentPatrolPoint.position - transform.position;
        this.transform.Translate(0, 0, Time.deltaTime * speed);
        this.transform.rotation = Quaternion.Slerp(transform.rotation,
        Quaternion.LookRotation(direction), 0.1f);

        if (Vector3.Distance(player.position, this.transform.position) < 20)
        {
            Vector3 direct = player.position - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direct), 0.1f);

            if (direct.magnitude > 3)
            {
                this.transform.Translate(0, 0, 0.11f);
            }
        }else if(Vector3.Distance(currentPatrolPoint.position, transform.position) < 5.0f)
        {
            if (currentPatrolIndex + 1 < patrolPoints.Length)
            {
                currentPatrolIndex++;
            }
            else
            {
                currentPatrolIndex = 0;
            }
            currentPatrolPoint = patrolPoints[currentPatrolIndex];
        }

        

    }
}
