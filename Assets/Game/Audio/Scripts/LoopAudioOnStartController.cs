/**
 * Universidad Autónoma de Occidente - 2018
 *
 * Controlador de la pantalla de inicio.
 *
 * @autor Gisler Garcés <gislersoft@hotmail.com>
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopAudioOnStartController : MonoBehaviour {

	/**
	 * Al iniciar la pantalla ejecute un audio infinitamente.
	 */
	void Start () {
		AudioSource audio = GetComponent<AudioSource>();
		audio.PlayDelayed (100); // Espere 100 milisegundos antes de iniciar.
		audio.loop = true;
	}

}
