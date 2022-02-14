/*
 * Jacob Zydorowicz
 * Project 2 Mole Mania
 * Makes crit spots on machine move and function with game
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClickDetect : MonoBehaviour
{
    public MachineMovement machineMovementScript;
    public GameObject SpecialEffect;

    public float X_Value;
    public float Y_Value;
    public float Z_Value;

    private void OnMouseDown()
    {
        //when the lights are clicked, they turn off
        if (machineMovementScript.canHit && (gameObject.CompareTag("CircleLights") || gameObject.CompareTag("TriangleLights")))
        {
            machineMovementScript.canHit = false;
            machineMovementScript.lightColor.DisableKeyword("_EMISSION");
            Instantiate(SpecialEffect, new Vector3(X_Value, Y_Value, Z_Value), Quaternion.identity);
        }


    }

   
}
