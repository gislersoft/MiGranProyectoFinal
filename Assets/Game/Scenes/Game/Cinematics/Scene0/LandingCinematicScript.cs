using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LandingCinematicScript : MonoBehaviour {
	// CONST
	private const short ACERCAMIENTO_AL_PLANETA = 0;
	private const short ATERRIZAJE = 1;

	private short acto = ACERCAMIENTO_AL_PLANETA;
	
	public Transform startMarker;
	public Transform endMarker;
	public float speed = 1.0F;
	public float speedRotation = 40.0f;

	public float shipScale = 1.0f;

	public Text refTextMessage;

	private float startTime;
	private float journeyLength;
	private float journeyLength2;
	private float journeyLength3;
	private Camera refCamera;

	private float distCovered = 0;
	private float fracJourney = 0;

	public GameObject nave1;
	public GameObject nave2;
	public GameObject luzNave;

	public Transform startMarker2;
	public Transform endMarker2;


	public Transform inicioCamara;
	public Transform finCamara;


	public float speed2 = 1.0F;
	public float speedCamara = 1.0F;

	private GameObject refQuoraOceano;

	void Start() {
		//Preparativos de la camara para el primer acto ACERCAMIENTO_AL_PLANETA
		refCamera = GameObject.Find("Camera").GetComponent<Camera>();
		refCamera.transform.position = new Vector3 (128.8482f,799.2638f,400.3901f);
		refCamera.transform.Rotate(new Vector3 (-16.498f,27.632f,-4.698f));

		startTime = Time.time;

		//Calculos de distancias a recorrer iniciales
		journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
		journeyLength2 = Vector3.Distance(startMarker2.position, endMarker2.position);
		journeyLength3 = Vector3.Distance(inicioCamara.position, finCamara.position);

		refTextMessage.text = "Year 3145 A.D";

		StartCoroutine(MostrarTexto("Year 3145 A.D\nThe Homo Sapiens has been vanished from five planets (Earth, Moon, Mars and Europa) due to unknown reasons.", 4f));

		StartCoroutine(MostrarTexto("Only Earth-2 and Euora Moon are known as the latest Homo Sapiens Colonies.", 12f));

		StartCoroutine(MostrarTexto("", 18f));

		StartCoroutine(MostrarTexto("Euora Moon", 20f));
		StartCoroutine(MostrarTexto("Euora Moon\nMoon of Blue Hole Quorth", 22f));

		StartCoroutine(MostrarTexto("Earth-2 has send one man for one last mission.", 27f));
		StartCoroutine(MostrarTexto("Astronaut, Physicist and Soldier.\nCommander Alexandre Bob Martinez Wang.", 30f));

		StartCoroutine(MostrarTexto("Your mission Commander Alexandre:\nFind the Homo Sapiens and resolve the vanishment mystery.", 35f));
	}

	IEnumerator MostrarTexto (string mensaje, float delayTime) {
		yield return new WaitForSeconds(delayTime);
		refTextMessage.text = mensaje;
	}

	void Update() {
		// Estados de la escena.
		switch (acto) {

			case ACERCAMIENTO_AL_PLANETA:
			
				acercarNaveAlPlaneta ();

				break;

			case ATERRIZAJE:
			
				AterrizarNave ();

				break;

		}
	}

	/**
	 * Acerca la nave al planeta y hace los ajustes de camara necesarios.
	 */
	private void acercarNaveAlPlaneta() {
		nave1.GetComponent<Collider>().enabled = false; // Deshabilitar colider para acelerar escalado.
		distCovered = (Time.time - startTime) * speed;
		fracJourney = distCovered / journeyLength;
		nave1.transform.position = Vector3.Lerp (startMarker.position, endMarker.position, fracJourney);

		nave1.transform.LookAt (endMarker.position);
		speedRotation = speedRotation + fracJourney * 0.4f;
		nave1.transform.Rotate (new Vector3 (0, 0, speedRotation));

		if ((1 - fracJourney) > 0.01f) {
			float scaleFactor = (1 - fracJourney) * shipScale;
			nave1.transform.localScale = new Vector3 (scaleFactor, scaleFactor, scaleFactor);
			refCamera.transform.LookAt (nave1.transform.position);
		} else {

			//Prepara la camara para el siguiente acto.
			refCamera.transform.position = new Vector3 (131.87f, 6.15f, 204.45f);
			refCamera.transform.Rotate (new Vector3 (-16.498f, -26.742f, -4.698f));
			refCamera.transform.LookAt (nave2.transform.position);
			startTime = Time.time;

			//Pasamos al siguiente acto.
			acto = ATERRIZAJE;
			refTextMessage.text = "";//Limpiar texto
		}
	}

	/**
	 * Aterriza la nave desde un punto definido, y mueve la camara para seguirla.
	 */
	private void AterrizarNave() {
		distCovered = (Time.time - startTime) * speed2;
		fracJourney = distCovered / journeyLength2;
		nave2.transform.position = Vector3.Lerp (startMarker2.position, endMarker2.position, fracJourney);

		distCovered = (Time.time - startTime) * speedCamara;
		float fracJourneyCamara = distCovered / journeyLength3;
		refCamera.transform.position = Vector3.Lerp (inicioCamara.position, finCamara.position, fracJourneyCamara);

		if ((1 - fracJourney) > 0.01f) {
			refCamera.transform.LookAt (nave2.transform.position);
		} else {
			refTextMessage.text = "";//Limpiar texto
			
			// Apague motores al segundo.
			Invoke ("apagarMotores",1f);
			//Cambie en 3 segundos a la siguiente escena
			Invoke ("cambiarASiguienteEscena",3f);
		}
	}

	private void cambiarASiguienteEscena() {
		SceneManager.LoadScene("escena1", LoadSceneMode.Single);
	}

	private void apagarMotores() {
		luzNave.SetActive(false);
	}
}
