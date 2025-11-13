using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponLogic : MonoBehaviour
{
    /*
        @current_Ammo es la munición actual
        @current_Carryng es la munición total
        @charger_Size la cantidad de balas que puede tener el arma
        @current_Damage es el daño actual del arma
        @modify_Damage es el daño que aumentan las modificaciones a la armas
        @rate_Fire es el tiempo entre cada disparo
        @shot_RateTime es el tiempo en el que se podra ejecutar el siguiente disparo
        @weapon_Bullet es el prefab de la bala que dispara el objeto
        @spawn_Bullet es la posición de spawn ubicado al frente del cañon donde se crearan las balas
        @shot_Force es la fuerza con la que saldra disparada la bala
        @isShooting sera verdadero cuando el jugador mantenga presionado el click izquierdo
        @AudioSource es la fuente de sonido para los efectos del arma
        @clip_NotAmmo sonido de cargador vacio
        @clip_Reload sonido de recargando arma
        @clip_Shoot sonido de disparo
        @GuiTextBullets Componente Text del objeto de la GUI para las balas
        @ammo inventario de armamento general
    */
    public int current_Ammo;
    public int charger_Size;
    public float reload_Time;

    public float current_Damage;
    public float modify_Damage;

    public float rate_Fire;
    public float shot_Force;
    private float shot_RateTime;

    public GameObject weapon_Bullet;
    public Transform spawn_Bullet;

    private bool isShooting;

    public AudioSource audio_Source;
    public AudioClip clip_NotAmmo;
    public AudioClip clip_Reload;
    public AudioClip clip_Shoot;

    [SerializeField]
    private Text GuiTextBullets;

    public Ammo ammo;

    void Start()
    {
        shot_RateTime = 0;
        isShooting = false;
        GuiTextBullets = GameObject.FindGameObjectWithTag("InterfaceBulletText").GetComponent<Text>();
        UpdateGUIBullets();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
        }

        if (isShooting && current_Ammo != 0)
        {
            Shoot();
        }

        if (Input.GetMouseButtonUp(0) && current_Ammo == 0)
        {
            NotAmmo();
        }

        if(current_Ammo != charger_Size && Input.GetKeyDown(KeyCode.R) && ammo.ammoLightCount != 0)
        {
            Reload();
        }
    }

    void Shoot()
    {
        if (Time.time > shot_RateTime)
        {
            //Creamos la bala y configuramos tanto su posición, como daño y velocidad
            GameObject newBullet;
            newBullet = Instantiate(weapon_Bullet, spawn_Bullet.position, spawn_Bullet.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * shot_Force);
            newBullet.GetComponent<bulletStats>().damage = (current_Damage + modify_Damage);

            shot_RateTime = Time.time + rate_Fire;

            UpdateGUIBullets();
            audio_Source.clip = clip_Shoot;
            audio_Source.Play();
            current_Ammo--;

            //Eliminar destroy cuando exista modelo de la bala
            Destroy(newBullet, 5);
        }
            
    }

    void Reload()
    {
        if ((ammo.ammoLightCount + current_Ammo) > charger_Size)
        {
            int ammo_recharger = charger_Size - current_Ammo;
            current_Ammo = charger_Size;

            ammo.ammoLightCount -= ammo_recharger;
        } else
        {
            current_Ammo += ammo.ammoLightCount;
            ammo.ammoLightCount = 0;
        }

        UpdateGUIBullets();
        shot_RateTime = Time.time + reload_Time;
        audio_Source.clip = clip_Reload;
        audio_Source.Play();
    }

    void NotAmmo()
    {
        audio_Source.clip = clip_NotAmmo;
        audio_Source.Play();
    }

    void UpdateGUIBullets()
    {
        //Se desactivo el script de UI Balas del GameObject TextBalas dado que esta incompleto
        GuiTextBullets.text = current_Ammo + "/" + ammo.ammoLightCount;
    }
}
