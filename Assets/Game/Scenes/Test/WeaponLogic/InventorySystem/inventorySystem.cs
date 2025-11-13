using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventorySystem : MonoBehaviour
{
    public GameObject[] weapons;
    int selectWeapon;
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

        print(selectWeapon);
        cambiarArma();
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) //Scroll hacia arriba
            {
            if (selectWeapon <= weapons.Length-2)
            {
                selectWeapon++;
            }
            else
            {
                selectWeapon = 0;
            }
            
        }else if(Input.GetAxis("Mouse ScrollWheel") < 0f) //Scroll hacia abajo
        {
            if (selectWeapon > 0)
            {
                selectWeapon--;
            }
            else
            {
                selectWeapon = weapons.Length - 1;
            }
        }
    }

    void cambiarArma()
    {
        foreach(var a in weapons)
        {
            int index = Array.IndexOf(weapons, a);
            if (index == selectWeapon)
            {
                a.gameObject.SetActive(true);
            }
            else
            {
                a.gameObject.SetActive(false);
            }
            
        }
    }
}
