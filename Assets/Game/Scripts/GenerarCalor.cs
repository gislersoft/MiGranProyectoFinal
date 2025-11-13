using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarCalor : MonoBehaviour
{

    private PlayerController calor;
    public float tiempo = 0;
    public float tiempoEspera = 4;
    public float valorRecuperar = 2;

    void Awake()
    {
        calor = GameObject.FindWithTag("P1").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "P1")
        {
            calor.increaseHeat(valorRecuperar);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "P1")
        {
            tiempo += Time.deltaTime;

            if (tiempo >= tiempoEspera)
            {
                calor.increaseHeat(valorRecuperar);
                tiempo = 0;
            }
        }
    }
}
