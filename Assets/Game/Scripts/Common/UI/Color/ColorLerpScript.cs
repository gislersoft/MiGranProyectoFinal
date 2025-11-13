/**
 * GOMA (Grupo de Desarrollo de Video Juegos UAO) - 2019
 *
 * Permite interpolar desde un color original a un color final.
 *
 * @autor Gisler Garcés <gislersoft@hotmail.com>
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorLerpScript : MonoBehaviour {
	public Color originalColor = Color.white;
	public Color lerpColor = Color.blue;
	public float speed = 0.5f;

	private Color startColor;
	private Color endColor;
	private float fraction = 0.0f;

	// Use this for initialization
	void Start () {
		startColor = originalColor;
		endColor = lerpColor;
	}
	
	// Update is called once per frame
	void Update () {
		fraction = fraction + (speed * Time.deltaTime);

		GetComponent<Image>().color = Color.Lerp(startColor, endColor,  fraction);

		if (GetComponent<Image>().color == originalColor ) {
			startColor = originalColor;
			endColor = lerpColor;
			fraction = 0;
		}

		if (GetComponent<Image>().color == lerpColor ) {
			startColor = lerpColor;
			endColor = originalColor;
			fraction = 0;
		}

	}
}
