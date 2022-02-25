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

    public AudioSource MachineSFX;
    public AudioClip machineHitSound;

    private void Start()
    {
        machineScript = GameObject.FindGameObjectWithTag("Machine").GetComponent<MachineMovement>();
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

                canHit = false;
                lightColor.SetColor("_EmissionColor", Color.black);

                GameObject clone = (GameObject)Instantiate(SpecialEffect, gameObject.transform.position, Quaternion.identity);
                Destroy(clone, 1.0f);
                MachineSFX.PlayOneShot(machineHitSound, 1.0f); //hi i changed this, it should work but the script never? gets to this point?

                if (hit.collider.name == "Hatch Light")
                {
                    machineScript.lightQueue.Enqueue(0);
                    GameObject.FindGameObjectWithTag("Hatch").transform.position = machineScript.oldHatchPos;
                }
                if (hit.collider.name == "LRLight")
                {
                    machineScript.lightQueue.Enqueue(1);
                    
                }
                if (hit.collider.name == "ULLight")
                {
                    machineScript.lightQueue.Enqueue(2);
                    
                }
                else if (hit.collider.name == "bottomTriLights")
                {
                    gameObject.transform.position = lightOriginPos;
                    machineScript.lightQueue.Enqueue(3);
                    
                }
                else if (hit.collider.name == "topTriLights")
                {
                    gameObject.transform.position = lightOriginPos;
                    machineScript.lightQueue.Enqueue(4);
                }
            }



        }

       
    }
}
