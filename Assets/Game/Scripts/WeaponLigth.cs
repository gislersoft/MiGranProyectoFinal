using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLigth : MonoBehaviour {
	public float minIntensity = 0;
	public float maxIntensity = 100;

	public float velocity = 0.5f;

	static float t = 0.0f;
	private Light ligth;
	// Use this for initialization
	void Start () {
		ligth = this.transform.GetComponent<Light>();
	}
    void Update()
    {
        // animate the position of the game object...
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
		ligth.intensity = intensity;

        // .. and increase the t interpolater
        t += velocity * Time.deltaTime;

        // now check if the interpolator has reached 1.0
        // and swap maximum and minimum so game object moves
        // in the opposite direction.
        if (t > 1.0f)
        {
            float temp = maxIntensity;
            maxIntensity = minIntensity;
            minIntensity = temp;
            t = 0.0f;
        }
    }
}
