using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevate : MonoBehaviour {

	private float floatingStreang;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Elevate();	
	}

	IEnumerator Elevate(){
		floatingStreang = Random.value;
		Debug.Log(floatingStreang);
		transform.GetComponent<Rigidbody>().AddForce(Vector3.up * floatingStreang * 20);
        yield return new WaitForSeconds(5);
		transform.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
		transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; 
	}
}
