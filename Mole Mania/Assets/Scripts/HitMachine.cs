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

    private Vector3 lightOriginPos;

    public float X_Value;
    public float Y_Value;
    public float Z_Value;

    public GameObject SpecialEffect;
    public Material lightColor;

    public AudioSource MachineHitSound;

    private void Start()
    {
        //sets all lights to green by default
        lightColor = gameObject.GetComponent<Renderer>().material;
        lightColor.SetColor("_EmissionColor", Color.red);

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
            if (canHit)
            {
                Debug.Log("hit");
                canHit = false;
                lightColor.SetColor("_EmissionColor", Color.black);
               
                
                if(hit.collider.name == "topTriLights")
                {

                    gameObject.transform.position = lightOriginPos;
                    //gameObject.transform.position = gameObject.transform.position + new Vector3(0f , 3f, 0f);


                }
                else if (hit.collider.name == "bottomTriLights")
                {
                    gameObject.transform.position = lightOriginPos;
                    //gameObject.transform.position = gameObject.transform.position + new Vector3(2f, 0f, 0f);
                }
                Instantiate(SpecialEffect, lightOriginPos, Quaternion.identity);
                MachineHitSound.Play();
            }



        }

       
    }
}
