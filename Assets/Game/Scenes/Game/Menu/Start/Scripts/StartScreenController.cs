/**
 * Universidad Autónoma de Occidente - 2018
 *
 * Controlador de la pantalla de inicio.
 *
 * @autor Gisler Garcés <gislersoft@hotmail.com>
 */
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour {

	/**
	 * Al iniciar la pantalla ejecute un audio infinitamente.
	 */
	void Start () {
		AudioSource audio = GetComponent<AudioSource>();
		audio.PlayDelayed (300); // Espere 100 milisegundos antes de iniciar.
		audio.loop = true;
	}

	/**
	 * Si el usuario presiona el boton start.
	 */
	public void onClickStartButton() {
		SceneManager.LoadScene("LandingCinematic", LoadSceneMode.Single);
	}

	/**
	 * Si el usuario presiona el boton quit.
	 */
	public void onClickQuitButton() {
		Application.Quit();
	}
}
