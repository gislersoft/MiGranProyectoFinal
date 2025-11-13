using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatBehaviour : MonoBehaviour {
	// Use this for initialization

    public Image heat;
    public PlayerController playerController; 

    // Update is called once per frame
    void Update()
    {
        heat.fillAmount = playerController.stats.heat / playerController.stats.getMaxHeat();
        // if (hp == 0f)
        // {
        //     Debug.Log("Me mori");
        //     TakeHealth(100f);

        // }
    }
}
