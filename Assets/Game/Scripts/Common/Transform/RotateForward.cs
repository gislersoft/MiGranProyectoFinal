using UnityEngine;
using System.Collections;

public class RotateForward : MonoBehaviour {
	public float direction = 1.0f;
	public float speed = 9.0f;

	public Vector3 axis = Vector3.forward;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(axis * Time.deltaTime * speed * direction);
	}
}
