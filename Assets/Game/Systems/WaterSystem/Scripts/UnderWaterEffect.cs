using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/**
 * Cambia el color de la luz del sol cuando se esta bajo el agua, para dar un efecto underwater,
 * tambien aplica efectos de sonido como ingreso al agua y el efecto the estar sumergido.
 */
public class UnderWaterEffect : MonoBehaviour {

	// Public
	private GameObject planoAgua;
	private Camera cam;
	public AudioClip splashSound;
	public Color colorAgua = new Color (0.0f, 0.0f, 1.0f);// Default Blue.
	public float intensidadDelSol = 10.0f;
	public Light luzDeSol;
	public GameObject refPlayer;
	private Color colorFogOriginal;
	private float fogDensityOriginal = 0.01f;
	public float fogDensityBajoAgua = 0.3f;

	private float timer;
	public float tiempoEsperaReducirCalor = 8;
	public float valorAReducirEnCalor = 2;

	// Private
	private bool playSplash = false;
	private Color colorOriginalSol;
	private float alturaAgua = 0.0f;
	private AudioSource aguaCiclo;
	private float intensidadOriginal = 0.0f;
	private Material skyBoxOriginal;

	void Start()
	{
		cam = Camera.main;
		planoAgua = GameObject.FindWithTag("PlanoAgua");
		alturaAgua = planoAgua.transform.position.y;

		// Recuerde el color original que tenia el sol antes de continuar.
		this.colorOriginalSol = this.luzDeSol.color;
		this.intensidadOriginal = this.luzDeSol.intensity;

		this.skyBoxOriginal = RenderSettings.skybox;
		this.colorFogOriginal = RenderSettings.fogColor;
		this.fogDensityOriginal = RenderSettings.fogDensity;

		aguaCiclo = GetComponent<AudioSource>();
		aguaCiclo.loop = true;
	}

	/**
	 * Mientras se esta debajo del agua pierde calor.
	 */
	void reducirCalor() {
		timer += Time.deltaTime;
		if (timer >= tiempoEsperaReducirCalor)
		{
			refPlayer.GetComponent<PlayerController> ().reduceHeat(valorAReducirEnCalor);
			timer = 0.0f;
		}
	}

	// Metodo update
	void Update()
	{
		if (cam.transform.position.y < this.alturaAgua ) {
			this.luzDeSol.color = this.colorAgua;
			this.luzDeSol.intensity = intensidadDelSol;
		
			RenderSettings.fogColor = this.colorAgua;
			RenderSettings.fogDensity = fogDensityBajoAgua;
			RenderSettings.skybox = null;

			reducirCalor ();
		} else {
			this.luzDeSol.color = this.colorOriginalSol;
			this.luzDeSol.intensity = this.intensidadOriginal;

			RenderSettings.fogColor = this.colorFogOriginal;
			RenderSettings.fogDensity = this.fogDensityOriginal;
			RenderSettings.skybox = this.skyBoxOriginal;
		}

		if (cam.transform.position.y < this.alturaAgua) {
			//this.playerControllerRef.gravity = 4.0f;

			if (this.playSplash == true) {
				AudioSource.PlayClipAtPoint (splashSound,cam.transform.position);

				aguaCiclo.Play ();
				//Debug.Log (Physics.gravity);

				this.playSplash = false;
			}

		} else {
			//this.playerControllerRef.gravity = 20.0f;// TODO: Se puede obtener la referencia original.
			this.playSplash = true;
			aguaCiclo.Stop ();
		}

	}

}
