using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unlock : MonoBehaviour {
    public BoxCollider[] doors;

    Shooting shooting;
    public Text refTextMessage;
    public Texture textureOn;
    public Texture textureOff;
    public GameObject screen;
    public Animator animator;
    bool onswitch = false;

	// Use this for initialization
	void Start () {

        shooting = GameObject.Find("SentinelHead").GetComponent<Shooting>();

        // Deactivate all doors.
        for(int i = 0; i < doors.Length; i++) {
            doors[i].enabled = false;
        }

        shooting.enabled = true;

        this.screen.GetComponent<Renderer>().material.mainTexture = textureOn;

    }
	
	// Update is called once per frame
	void Update () {
        if (onswitch) {

            if (Input.GetKeyDown(KeyCode.E)) {
                animator.SetTrigger("isTouching");
                this.screen.GetComponent<Renderer>().material.mainTexture = textureOff;

                // Activate all doors.
                for(int i = 0; i < doors.Length; i++) {
                    doors[i].enabled = true;
                }

                // Disable sentinel shooting.
                shooting.enabled = false;

                StartCoroutine(MostrarTexto("Sentinel has been deactivated", 0.0f));
                StartCoroutine(MostrarTexto("Doors have been enabled", 2.0f));
                StartCoroutine(MostrarTexto("", 7.0f));
            }
            
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("P1")) {
            onswitch = true;
        }
    }
    private void OnTriggerExit(Collider other){
        if (other.CompareTag("P1")) {
            onswitch = false;
        }
    }

    IEnumerator MostrarTexto(string mensaje, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        refTextMessage.text = mensaje;
    }
}
