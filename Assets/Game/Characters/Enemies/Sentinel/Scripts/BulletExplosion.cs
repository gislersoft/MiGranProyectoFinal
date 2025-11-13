using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour {
    Shooting scriptShot;
    private Vector3 InitialPosition;
    public AudioClip ExplosionSound;
    public float Volume = 2.0f;
    public ParticleSystem explosion;
    healthBehaviour vida;
    public static int Quitarvida;
    public static bool healthflag;
    GameObject barravida;
    // Use this for initialization
    void Start () {
        scriptShot = GameObject.Find("SentinelHead").GetComponent<Shooting>();
        InitialPosition = transform.position;
        healthflag = false;
        //explosion = GameObject.Find("Explosion").GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 VDistance = InitialPosition - transform.position;
        if (VDistance.sqrMagnitude >= 100000)
        {
            scriptShot.setAllowShot(true);
            Destroy(gameObject);
            //Debug.Log("BalaPerdida");
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        scriptShot.setAllowShot(true);
        //Crear Explosion de Particulas
        explosion.Play();
        Destroy(gameObject,1f);
        AudioSource.PlayClipAtPoint(ExplosionSound, transform.position, Volume);
        //Debug.Log("BalaImpactada en: " + collision.gameObject.name);

        if (collision.gameObject.tag == "P1")
        {
            collision.gameObject.GetComponent<PlayerController>().reduceHealth(10);

        }
    }

}
