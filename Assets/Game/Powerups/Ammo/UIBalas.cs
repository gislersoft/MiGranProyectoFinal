using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBalas : MonoBehaviour {

	private Text textBalas;

	private int ObjetivoBalas = 0;
	void Start () {
		
		textBalas = GetComponent<Text> ();
		ObjetivoBalas = Colletable.BalasCount;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		int BalasRestantes = Colletable.BalasCount;
		int ColletableBalas = ObjetivoBalas + BalasRestantes;

		if(BalasRestantes < 100){
			textBalas.text = ColletableBalas + "/" +"100";
		
		}else{
			textBalas.text = "Municion llena";
		}


	}
}
