using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    [Header("Cannon Configuration")]
    public Transform Objective;
    private float MovementSpeed = 10f;
    public Rigidbody Bullet;
    private float BulletSpeed = 150f;
    private float ShotCooldown=2f;
    private float scope = 2500;
    public float Health = 30;
    [Space(10)]

    [Header("Audio Effects")]
    public AudioClip ShotSound;
    public AudioClip ExplosionSound;
    public float Volume = 1.0f;

    private  bool AllowShot = true;
    private Rigidbody bullets;
    private int serie;
    private float myTime;

    public void setAllowShot(bool permitirDisparo) { this.AllowShot = permitirDisparo; }
    public bool getAllowShot() { return AllowShot; }


    void Start()
    {
        this.serie = 0;
        this.myTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Vdistance = Objective.transform.position - transform.position;
        if(Vdistance.sqrMagnitude <= scope)
        {
            gameObject.GetComponentInParent<Animator>().enabled = false;
            LookPoint();
            RaycastHit vision;
            Debug.DrawRay(transform.position, transform.forward * scope, Color.red);
            if(Physics.Raycast(transform.position, transform.forward, out vision, scope))
            {
                if(vision.collider.tag == "P1")
                {
                    Shoot();
                }
            }
        }else gameObject.GetComponentInParent<Animator>().enabled = true;

    }

    private void LookPoint()
    {
        Vector3 direccion = Objective.position - transform.position;
        Quaternion rotacion = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, MovementSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        if(AllowShot && Time.time - this.myTime >= ShotCooldown && serie % 1 == 0)
        {
            AllowShot = false;
            this.myTime = Time.time;
            bullets = Instantiate(Bullet, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(ShotSound, transform.position, Volume);
            bullets.tag = "bullet";
            Physics.IgnoreCollision(bullets.GetComponent<Collider>(), GetComponent<Collider>());
            bullets.linearVelocity = transform.TransformDirection(new Vector3(0, 0, BulletSpeed));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //los modelos que colisionen con el modelo debe tener el tag bullet
        if (collision.gameObject.tag == "bullet") Health = Health - 5;
    }
}
