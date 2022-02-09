/*
 * Jacob Zydorowicz
 * Project 2 Mole Mania
 * This script allows the moles and machine spots to be clicked and changed accordingly
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineMovement : MonoBehaviour
{
    public int health = 100;
    public bool canHit;
    public GameObject lights;
    public Material lightColor;
    
    // initializes variables, and crit spots
    void Start()
    {
        canHit = true;
        lightColor = lights.GetComponent<Renderer>().material;

        lightColor.SetColor("_EmissionColor", Color.gray);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
 
}
/*Notes:
 * Make the light bars require multiple hits to go away so one hit doesn't give it away
 * 
 */