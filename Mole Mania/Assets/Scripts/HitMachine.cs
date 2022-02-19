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
    private MachineMovement machineScript;

    private Vector3 lightOriginPos;

    public float X_Value;
    public float Y_Value;
    public float Z_Value;

    public GameObject SpecialEffect;
    public Material lightColor;

    private void Start()
    {
        canHit = false;
        lightColor = GetComponent<Renderer>().material;
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
                    queueLight(0);
                    
                }
                if (hit.collider.name == "LRLight")
                {
                    queueLight(1);
                    
                }
                if (hit.collider.name == "ULLight")
                {
                    queueLight(2);
                    
                }
                else if (hit.collider.name == "bottomTriLights")
                {
                    gameObject.transform.position = lightOriginPos;
                    queueLight(3);
                    
                }
                else if (hit.collider.name == "topTriLights")
                {
                    gameObject.transform.position = lightOriginPos;
                    queueLight(4);
                }
                Instantiate(SpecialEffect, lightOriginPos, Quaternion.identity);
            }



        }

       
    }

    //enqueues corresponding light number from machine light array based on which light is hit
    private void queueLight(int lightNum)
    {
        if (machineScript.lightQueue.Count <= 5)
        {
            GameObject.FindGameObjectWithTag("Machine").GetComponent<MachineMovement>().lightQueue.Enqueue(lightNum);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Machine").GetComponent<MachineMovement>().lightQueue.Clear();
            GameObject.FindGameObjectWithTag("Machine").GetComponent<MachineMovement>().lightQueue.Enqueue(lightNum);
        }
    }

 
}
