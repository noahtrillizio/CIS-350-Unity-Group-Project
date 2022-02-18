/*
 * Jacob Zydorowicz, Noah Trillizio
 * Project 2 Mole Mania
 * Detects if player clicks on crit spots or not
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMachine : MonoBehaviour
{
    public bool canHit;
    private int lightNum;

    private Vector3 lightOriginPos;

    public float X_Value;
    public float Y_Value;
    public float Z_Value;

    public GameObject SpecialEffect;
    public Material lightColor;

    private void Start()
    {
        canHit = false;

        lightOriginPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
       
    }

    //detects when player clicks on machine parts
    private void OnMouseDown()
    {
        
        //object click detection based on https://answers.unity.com/questions/1128405/how-do-i-detect-a-click-on-an-object-with-a-partic.html
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
     

        if (Physics.Raycast(ray, out hit))
        {
            //if machine spot is clicked, resets lights
            if (canHit && hit.collider.name != "Hatch")
            {
                Debug.Log("hit");
                canHit = false;
                lightColor.SetColor("_EmissionColor", Color.black);
               
                if(hit.collider.name == "Hatch Light")
                {
                    lightNum = 0;
                }
                if (hit.collider.name == "LRLight")
                {
                    lightNum = 1;
                }
                if (hit.collider.name == "ULLight")
                {
                    lightNum = 2;
                }
                else if (hit.collider.name == "bottomTriLights")
                {
                    gameObject.transform.position = lightOriginPos;
                    lightNum = 3;
                }
                else if (hit.collider.name == "topTriLights")
                {
                    gameObject.transform.position = lightOriginPos;
                    lightNum = 4;
                }
                Instantiate(SpecialEffect, lightOriginPos, Quaternion.identity);
            }



        }

       
    }
}
