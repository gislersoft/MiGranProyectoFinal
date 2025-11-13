using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerarOxigeno : MonoBehaviour
{

    public PlayerController oxigeno;
    public float tiempo = 0;
    public float tiempoEspera = 1;
    public float valorRecuperar = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "P1" && oxigeno.stats.oxygen < oxigeno.stats.getMaxOxygen())
        {
            oxigeno.increaseOxygen(valorRecuperar);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "P1" && oxigeno.stats.oxygen < oxigeno.stats.getMaxOxygen())
        {
            tiempo += Time.deltaTime;

            if (tiempo >= tiempoEspera)
            {
                oxigeno.increaseOxygen(valorRecuperar);
                tiempo = 0;
            }
        }
    }
}
