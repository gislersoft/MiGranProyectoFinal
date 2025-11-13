using UnityEngine;
using System.Collections;

public class RotateLigthAroundPlanetScript : MonoBehaviour {
	public GameObject aroundObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(aroundObject.transform.position, Vector3.forward, 30 * Time.deltaTime);
	}
}
