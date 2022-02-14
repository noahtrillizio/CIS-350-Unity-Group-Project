/*
 * Jacob Zydorowicz
 * Project 2 Mole Mania
 * Detects if hammer hits machine spots and progresses game
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMachine : MonoBehaviour
{
    private Vector3 topLightPos;
    private Vector3 bottomLightPos;

    public float X_Value;
    public float Y_Value;
    public float Z_Value;

    public MachineMovement machineMovementScript;
    public ScoreManager scoreManObj;
    public GameObject SpecialEffect;

    private void Start()
    {
        topLightPos = (GameObject.Find("topTriLights").transform.position);
        bottomLightPos = (GameObject.Find("bottomTriLights").transform.position);
    }

    private void OnMouseDown()
    {
      
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
    
        if (Physics.Raycast(ray, out hit))
        {
            //if machine spot is clicked, resets lights
            if (machineMovementScript.canHit && (hit.collider.CompareTag("CircleLights") || hit.collider.CompareTag("TriangleLights")))
            {
                machineMovementScript.canHit = false;
                machineMovementScript.lightColor.DisableKeyword("_EMISSION");
                
                if(hit.collider.name == "topTriLights")
                {
                    hit.collider.transform.position = topLightPos;
                }
                else if (hit.collider.name == "bottomTriLights")
                {
                    hit.collider.transform.position = bottomLightPos;
                }
                Instantiate(SpecialEffect, new Vector3(X_Value, Y_Value, Z_Value), Quaternion.identity);
            }



        }

       
    }
}
