using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour
{

    public Stats stats;

    [System.Serializable]
    public class Stats
    {
        private float maxOxygen = 100f;
        private float maxHealth = 100f;
        private float maxHeat = 100f;
        public float oxygen;
        public float health;
        public float heat;

        public float getMaxOxygen()
        {
            return this.maxOxygen;
        }
        public float getMaxHealth()
        {
            return this.maxHealth;
        }

        public float getMaxHeat()
        {
            return this.maxHeat;
        }
    }

    public UI ui;

    [System.Serializable]
    public class UI
    {
        public GameObject menuPausa;
    }

    private bool oxygenFlag;
    private float timeInSeconds;
    private float timeWaitHeat;
    private float timeWaitOxygen;
    private bool pausado;
    private bool deadTriggered = false;
    private bool lookCameraPlayerDead = false;

    private HeadBob headBobRef;
    private RigidbodyFirstPersonController rigidFPSController;
    private Animator animatorRef;
    private GameObject playerSpine;
    private GameObject cam;

    void Start()
    {
        oxygenFlag = true;
        timeInSeconds = 0f;
        pausado = false;
        Cursor.visible = false;
        this.headBobRef = GameObject.Find("MainCamera").GetComponent<HeadBob>();
        this.rigidFPSController = GameObject.Find("RigidBodyFPSController").GetComponent<RigidbodyFirstPersonController>();
        this.animatorRef = GameObject.FindWithTag("PlayerAnimator").GetComponent<Animator>();
        this.playerSpine = GameObject.FindWithTag("PlayerSpine");
        this.cam = GameObject.Find("MainCamera");
    }

    IEnumerator ActivarCamaraGameOver(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        this.lookCameraPlayerDead = true;
        StartCoroutine(RecargarEscena(3.0f));
    }

    IEnumerator RecargarEscena(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        //print("!Has muerto!");
        //Time.timeScale =0;
        //if(Input.GetKeyDown(KeyCode.Return))
        //{
        //    Time.timeScale =1;
            SceneManager.LoadScene("escena1");
        //}
    }


    // Update is called once per frame
    void Update()
    {
        timeInSeconds += Time.deltaTime;
        //Debug.Log(oxygenFlag);
        if (timeInSeconds % 4f <= 0.2f && oxygenFlag)
        {
            //Debug.Log("reducir vida");

            reduceOxygen(stats.getMaxHealth() * 0.01f);
            reduceHeat(2);

            oxygenFlag = false;
        }
        else if (timeInSeconds % 4f > 0.2f)
        {
            oxygenFlag = true;
        }
        if (stats.health <= 100 && BulletExplosion.healthflag == true)
        {
            reduceHealth(BulletExplosion.Quitarvida);
            BulletExplosion.healthflag = false;
        }

        if (stats.health == 0)
        {
            this.headBobRef.enabled = false;
            this.rigidFPSController.stopAllForces();
            this.rigidFPSController.enabled = false;
            if (this.deadTriggered == false) {
                this.animatorRef.SetTrigger("Dead");
                this.deadTriggered = true;
            }
            StartCoroutine(ActivarCamaraGameOver(1.2f));
            if (this.lookCameraPlayerDead == true) {
                this.cam.transform.LookAt(this.playerSpine.transform.position);
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!pausado)
            {
                pausar();
                pausado = true;
            }
            else
            {
                quitarPausa();
                pausado = false;
            }
        }		

        if (stats.heat == 0)
        {
            timeWaitHeat += Time.deltaTime;

            if (timeWaitHeat >= 1)
            {
                reduceHealth(2);
                timeWaitHeat = 0f;
            }

        }

        if (stats.oxygen == 0)
        {
            timeWaitOxygen += Time.deltaTime;

            if (timeWaitOxygen >= 1)
            {
                reduceHealth(2);
                timeWaitOxygen = 0f;
            }

        }
    }

    public void quitarPausa()
    {
        Time.timeScale = 1;

        ui.menuPausa.SetActive(false);
        Cursor.visible = false;
    }

    public void pausar()
    {
        Time.timeScale = 0;

        ui.menuPausa.SetActive(true);
        Cursor.visible = true;
    }

    public void reduceOxygen(float cantidad)
    {
        if (stats.oxygen > 0)
        {

            stats.oxygen -= cantidad;
        }

        if (stats.oxygen < 0)
        {
            stats.oxygen = 0;
        }
    }

    public void increaseOxygen(float cantidad)
    {
        if (stats.oxygen < stats.getMaxOxygen())
        {
            stats.oxygen += cantidad;
        }

        if (stats.oxygen > stats.getMaxOxygen())
        {
            stats.oxygen = stats.getMaxOxygen();
        }
    }

    public void reduceHealth(float cantidad)
    {
        if (stats.health > 0)
        {
            stats.health -= cantidad;

        }

        if (stats.health < 0)
        {
            stats.health = 0;
        }
    }

    public void increaseHealth(float cantidad)
    {
        if (stats.health < stats.getMaxHealth())
        {
            stats.health += cantidad;
        }

        if (stats.health > stats.getMaxHealth())
        {
            stats.health = stats.getMaxHealth();
        }
    }

    public void reduceHeat(float cantidad)
    {
        if (stats.heat > 0)
        {
            stats.heat -= cantidad;
        }

        if (stats.heat < 0)
        {
            stats.heat = 0;
        }
    }

    public void increaseHeat(float cantidad)
    {
        if (stats.heat < stats.getMaxHeat())
        {
            stats.heat += cantidad;
        }

        if (stats.heat > stats.getMaxHeat())
        {
            stats.heat = stats.getMaxHeat();
        }
    }
}
