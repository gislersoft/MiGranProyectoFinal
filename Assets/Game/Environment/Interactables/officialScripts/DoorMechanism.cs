/**
 * Universidad Autónoma de Occidente - 2018
 *
 * Allows to open the door when the user is near the door, you can
 * have as many doors you want in the same scene.
 *
 * @autor Cesar Salazar <cesar0572011@hotmail.com>
 * @autor Gisler Garces <gislersoft@hotmail.com>
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanism : MonoBehaviour {
    GameObject doorL;
    GameObject doorR;   
    GameObject targetL;
    GameObject targetR;
    GameObject mid;

    AudioSource au;
    ParticleSystem  psLe;
    ParticleSystem  psRe;
    float distance1;
    float distance2;
    bool open = false;
    void Start()
    {
        
        au = gameObject.GetComponent<AudioSource>();

        for (int i = 0; i < this.transform.parent.childCount; i++) {
            GameObject gameComponent = this.transform.parent.GetChild(i).gameObject;

            if ("SteamL".Equals(gameComponent.name)) {
                psLe = gameComponent.GetComponent<ParticleSystem>();
                continue;
            }

            if ("SteamR".Equals(gameComponent.name)) {
                psRe = gameComponent.GetComponent<ParticleSystem>();
                continue;
            }

            if ("DoorRigth".Equals(gameComponent.name)) {
                doorR = gameComponent;
                continue;
            }

            if ("DoorLeft".Equals(gameComponent.name)) {
                doorL = gameComponent;
                continue;
            }

            if ("refRight".Equals(gameComponent.name)) {
                targetR = gameComponent;
                continue;
            }

            if ("refLeft".Equals(gameComponent.name)) {
                targetL = gameComponent;
                continue;
            }

            if ("middle".Equals(gameComponent.name)) {
                mid = gameComponent;
                continue;
            }
        }
    }

    // Update is called once per frame
    void Update()
    { 
        if (open)
        {
            doorL.transform.position = Vector3.MoveTowards(doorL.transform.position, targetL.transform.position, 0.05f);
            doorR.transform.position = Vector3.MoveTowards(doorR.transform.position, targetR.transform.position, 0.05f);
        }
        if (open == false)
        {
            doorL.transform.position = Vector3.MoveTowards(doorL.transform.position, mid.transform.position, 0.05f);
            doorR.transform.position = Vector3.MoveTowards(doorR.transform.position, mid.transform.position, 0.05f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("P1"))
        {
            open = true;
            var Le = psLe.emission;
            var Re = psRe.emission;
            Le.enabled = true;
            Re.enabled = true;
            psLe.Play();
            psRe.Play();
            au.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("P1"))
        {
            open = false;
        }
    }
}
