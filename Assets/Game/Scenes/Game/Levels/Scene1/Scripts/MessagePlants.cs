using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePlants : MonoBehaviour {

    float contadorSegundos;
    private GameObject mensaje;
    int contador = 0;

    private void Start() {
        mensaje = GameObject.Find("MensajeTemporal");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "P1" && contador == 0)
        {
            mensaje.SetActive(true);
        }
    }
    void OnTriggerStay(Collider other)
    {
        contadorSegundos += Time.deltaTime;
        if (other.gameObject.tag == "P1" && contador == 1)
        {
            if (contadorSegundos >= 15 || contador == 1)
            {
                mensaje.SetActive(false);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "P1")
        {
            mensaje.SetActive(false);
        }
    }
}
