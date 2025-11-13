using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colletable : MonoBehaviour {

	public enum ColletableType {BALAS, SANGRE, DINERO};
	public ColletableType Type;

    public static int BalasCount = 0;
	private int BalaValor = 10;


	// Use this for initialization
	void Start ()
    {
       // Colletable.BalasCount += BalaValor;
		Colletable.BalasCount = 0;
		
	}
	

    void OnDestroy()
    {
        Colletable.BalasCount += BalaValor;

        if (BalasCount == 100)
        {
            Debug.Log("Municion llena");
        }
    }

    void OnTriggerEnter(Collider OtherCollider)
    {
        if (OtherCollider.CompareTag("P1"))
        {
            Destroy(gameObject);
        }
    }


}
