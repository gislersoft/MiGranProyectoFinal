using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaAttack : MonoBehaviour
{
 
    public float moveSpeed = 3; //move speed
    public float rotationSpeed = 3; //speed of turning
    public float distanciaAtaque = 9;
    private Transform myTransform; //current transform data of this enemy
    private Transform target; //the enemy's target
    private Transform planoAgua;
    private Rigidbody rb;
    private float tiempo=0.0f;
    private float angulo = 0.0f;

    void Awake()
    {
        myTransform = this.transform; //cache transform data for easy access/preformance
        rb = GetComponent<Rigidbody>();

        // Moves the GameObject using it's transform.
        //rb.isKinematic = true;
        angulo = Random.Range(0, 359);
    }


    void Start()
    {
        target = GameObject.FindWithTag("P1").transform; //target the player
        planoAgua = GameObject.FindWithTag("PlanoAgua").transform;
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;
        Vector3 distanceVector = target.position - myTransform.position;

        if (distanceVector.magnitude >= distanciaAtaque)
        {
            if (tiempo > Random.Range(1.0f, 5.0f))
            {
                angulo = Random.Range(0, 359);
                tiempo = 0.0f;
            }
            distanceVector = new Vector3(Mathf.Cos(angulo), Mathf.Sin(angulo), 0);
            distanceVector = distanceVector * 10.0f;
        }

        //rotate to look at the player
        myTransform.rotation = Quaternion.Slerp(
            myTransform.rotation,
            Quaternion.LookRotation(distanceVector),
            rotationSpeed * Time.deltaTime);

        Vector3 moveVector = myTransform.forward * moveSpeed * Time.deltaTime;
        //move towards the player
        //myTransform.position += moveVector;
        // Las medusas no pueden dejar el agua.
        if (myTransform.position.y >= planoAgua.position.y)
        {
            rb.mass = 1.0f;
            //planoAgua.position.y,
            //myTransform.position.y,
            /*myTransform.position = new Vector3(
                myTransform.position.x,
                planoAgua.position.y,
                myTransform.position.z
            );*/
            /*
            rb.MovePosition(new Vector3(
                myTransform.position.x,
                planoAgua.position.y,
                myTransform.position.z
            ));
            */
        } else
        {
            rb.mass = 0.0001f;
            myTransform.position += moveVector;
        }
 
    }
}
