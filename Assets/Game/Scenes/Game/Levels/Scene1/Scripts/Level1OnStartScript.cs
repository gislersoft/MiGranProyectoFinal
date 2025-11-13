/**
 * @autor Gisler Garcés <gislersoft@hotmail.com>
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Level1OnStartScript : MonoBehaviour {

	public AudioClip sheThaInstructionsAudioClip;

    public GameObject HUDBarraVida;
    public GameObject HUDBalas;
    public GameObject HUDOxigeno;
    public GameObject HUDMira;

    private Camera cam;

	public float delayTimeInSeconds = 3;

    public Text refTextMessage;

    private Vector3 cameraPosition;
    private Quaternion cameraRotation;

    private Vector3 characterPosition;
    private Quaternion characterRotation;

    private RigidbodyFirstPersonController rigidFPSControllerRef;
    private HeadBob headBobRef;
    private CameraRotateAround cameraRotateRef;

    IEnumerator Start()
    {
        this.cam = Camera.main;
        this.HUDBarraVida.SetActive(true);
        this.HUDBalas.SetActive(true);
        this.HUDOxigeno.SetActive(true);
        this.HUDMira.SetActive(true);
        this.refTextMessage.enabled = true;

        AudioSource audio = GetComponent<AudioSource>();

        rigidFPSControllerRef = GameObject.Find("RigidBodyFPSController").GetComponent<RigidbodyFirstPersonController>();
        headBobRef = GameObject.Find("MainCamera").GetComponent<HeadBob>();
        cameraRotateRef = GameObject.Find("MainCamera").GetComponent<CameraRotateAround>();

        audio.Play();

        StartCoroutine(MostrarTexto("Commander Welcome to Euora Moon.", 0.0f));
        StartCoroutine(MostrarTexto("I'am She-Tha your suit computer.", 2.0f));
        StartCoroutine(MostrarTexto("Right down in the display of your helmet, you can watch your oxigen levels.", 5.0f));
        this.toggleHUD(this.HUDOxigeno,5.0f,17);
        StartCoroutine(MostrarTexto("Left top corner in the display of your helmet, you can watch your health and body temperature levels.", 10.0f));
        this.toggleHUD(this.HUDBarraVida,10.0f,17);
        StartCoroutine(MostrarTexto("Right top corner in the display of your helmet, your ammo.", 15.0f));
        this.toggleHUD(this.HUDBalas,15.0f,17);
        StartCoroutine(MostrarTexto("Be careful Commander.", 15.0f));
        StartCoroutine(MostrarTexto("Something is happening with the base.\ngathering data, please wait...", 21.0f));
        StartCoroutine(MostrarTexto("", 24.0f));


        yield return new WaitForSeconds(audio.clip.length + delayTimeInSeconds);
        audio.clip = sheThaInstructionsAudioClip;
        audio.Play();

        StartCoroutine(RotarCamara(0.0f));
        StartCoroutine(MostrarTexto("[!!! ALERT !!!] YOU MUST SHUTDOWN THE SENTINEL ROBOT.", 0.0f));
        StartCoroutine(MostrarTexto("The robot is in BASE_DEFEND_MODE.", 4.0f)); 
        StartCoroutine(MostrarTexto("Doors of the base are locked.", 6.0f));
        StartCoroutine(MostrarTexto("The switch to shutdown the robot is inside the base.", 9.0f));
        StartCoroutine(MostrarTexto("Find a way to enter into the base without being killed by the Sentinel.", 13.0f));
        StartCoroutine(MostrarTexto("Watch your oxigen and temperature levels.", 16.0f));
        StartCoroutine(MostrarTexto("", 20.0f));

        StartCoroutine(DejarDeRotarCamara(20.0f));
    }

    private void toggleHUD(GameObject gameObject, float delayTime, int times) {
        bool toggle = true;
        float timeOffset = 0.25f;
        float initialOffset = delayTime;
        for (int i = 0; i < times; i++ ) {
            StartCoroutine(ToggleObject(gameObject, toggle, initialOffset ));
            toggle = !toggle;
            initialOffset = initialOffset + timeOffset;
        }
        // One last time to leave the HUD enabled.
        StartCoroutine(ToggleObject(gameObject, true, initialOffset ));
    }

    IEnumerator ToggleObject (GameObject gameObject, bool toggle, float delayTime) {
		yield return new WaitForSeconds(delayTime);
		gameObject.SetActive(toggle);
	}

    IEnumerator MostrarTexto (string mensaje, float delayTime) {
		yield return new WaitForSeconds(delayTime);
		refTextMessage.text = mensaje;
	}

    IEnumerator RotarCamara(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        this.characterPosition = rigidFPSControllerRef.transform.position;
        this.characterRotation = rigidFPSControllerRef.transform.rotation;
        this.cameraPosition = this.cam.transform.position;
        this.cameraRotation = this.cam.transform.rotation;

        this.HUDBarraVida.SetActive(false);
        this.HUDBalas.SetActive(false);
        this.HUDOxigeno.SetActive(false);
        this.HUDMira.SetActive(false);

        headBobRef.enabled = false;
        rigidFPSControllerRef.enabled = false;
        cameraRotateRef.enabled = true;
    }

    IEnumerator DejarDeRotarCamara(float delayTime) {
        yield return new WaitForSeconds(delayTime);

        rigidFPSControllerRef.transform.position = this.characterPosition;
        rigidFPSControllerRef.transform.rotation = this.characterRotation;
        this.cam.transform.position = this.cameraPosition;
        this.cam.transform.rotation = this.cameraRotation;

        cameraRotateRef.enabled = false;
        headBobRef.enabled = true;
        rigidFPSControllerRef.enabled = true;

        this.HUDBarraVida.SetActive(true);
        this.HUDBalas.SetActive(true);
        this.HUDOxigeno.SetActive(true);
        this.HUDMira.SetActive(true);
    }
}
