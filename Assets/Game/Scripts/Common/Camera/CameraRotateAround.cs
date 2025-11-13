using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateAround : MonoBehaviour {
	// The target object to reference during rotation.
	public GameObject targetObject;
	public Camera cameraRef;
	public GameObject cameraReference;

	// Get the high of the camera.
	public float cameraHigh = 25.0f;

	// If the initial movement has been applied to the camera.
	public bool cameraMoved = false;
	// Use this for initialization
	void Start () {
		this.cameraRef = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if (targetObject != null) {
			if (this.cameraMoved != true) {
				this.cameraMoved = true;
				this.cameraRef.transform.position = new Vector3(cameraReference.transform.position.x,cameraHigh,cameraReference.transform.position.z - 4.0f);
			}
			this.cameraRef.transform.LookAt(targetObject.transform);
			transform.RotateAround(targetObject.transform.position, Vector3.up, 20 * Time.deltaTime);
		}
			
	}
}
