/*
 * Jacob Zydorowicz
 * Project 2 Mole Mania
 * Moves crit spots on machine based on running timer
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineMovement : MonoBehaviour
{
    
    private int lightsHit;

    Vector3 newTopLightPos, oldTopPos, newBotLightPos, oldBotPos;
    public GameObject[] lights;
   
    public Timer timer;
    
   
    void Start()
    {
        //gets current and future positons of moving lights
        newTopLightPos = new Vector3(lights[4].gameObject.transform.position.x, lights[4].gameObject.transform.position.y, lights[4].gameObject.transform.position.z) + new Vector3(0f, 3f, 0f);
        oldTopPos = new Vector3(lights[4].gameObject.transform.position.x, lights[4].gameObject.transform.position.y, lights[4].gameObject.transform.position.z);
        newBotLightPos = new Vector3(lights[3].gameObject.transform.position.x, lights[3].gameObject.transform.position.y, lights[3].gameObject.transform.position.z) + new Vector3(2f, 0f, 0f);
        oldBotPos = new Vector3(lights[3].gameObject.transform.position.x, lights[3].gameObject.transform.position.y, lights[3].gameObject.transform.position.z);
    }

   
    void Update()
    {
  
        //determines when light events should be active and inactive
        if (timer.timeRemaining > 0 && !timer.gameOver)
        {
            //turns lower right circle light on and off
            if(Mathf.FloorToInt(timer.timeRemaining) == timer.maxTime-30)
            {
                lights[1].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(255, 40, 0, 0));
                lights[1].GetComponent<HitMachine>().canHit = true;
                //lightsHit++;

                //circLightsOn(lights[1]);
            }
            else if(Mathf.FloorToInt(timer.timeRemaining) == timer.maxTime - 60)
            {
                lights[1].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(146, 0, 3, 0));
                lights[1].GetComponent<HitMachine>().canHit = false;
                //circLightsOff(lights[1]);
            }

            //turns top triangle light on and off
            if (Mathf.FloorToInt(timer.timeRemaining) == timer.maxTime - 100)
            {
                lights[4].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(255, 40, 0, 0));
                lights[4].transform.position = newTopLightPos;
                lights[4].GetComponent<HitMachine>().canHit = true;
                //lightsHit++;
                
                //circLightsOn(lights[1]);
            }
            else if (Mathf.FloorToInt(timer.timeRemaining) == timer.maxTime - 120)
            {
                lights[4].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(146, 0, 3, 0));
                lights[4].transform.position = oldTopPos;
                lights[4].GetComponent<HitMachine>().canHit = false;
                //circLightsOff(lights[1]);
            }

            //turns upper left circle light on and off
            if (Mathf.FloorToInt(timer.timeRemaining) == timer.maxTime - 160)
            {
                lights[2].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(255, 40, 0, 0));
                lights[2].GetComponent<HitMachine>().canHit = true;
                //lightsHit++;
                
                //circLightsOn(lights[1]);
            }
            else if (Mathf.FloorToInt(timer.timeRemaining) == timer.maxTime - 180)
            {
                lights[2].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(146, 0, 3, 0));
                lights[2].GetComponent<HitMachine>().canHit = false;
                //circLightsOff(lights[1]);
            }

            //will open hatch for other light
            if (Mathf.FloorToInt(timer.timeRemaining) == timer.maxTime - 195)
            {
                lights[0].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(255, 40, 0, 0));
                lights[0].GetComponent<HitMachine>().canHit = true;
                //lightsHit++;
                
                //circLightsOn(lights[1]);
            }
            else if (Mathf.FloorToInt(timer.timeRemaining) == timer.maxTime - 205)
            {
                lights[0].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(146, 0, 3, 0));
                lights[0].GetComponent<HitMachine>().canHit = false;
                //circLightsOff(lights[1]);
            }

            //turns bottom triangle light on and off
            if (Mathf.FloorToInt(timer.timeRemaining) == timer.maxTime - 265)
            {
                lights[3].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(255, 40, 0, 0));
                lights[3].transform.position = newBotLightPos;
                lights[3].GetComponent<HitMachine>().canHit = true;
                //lightsHit++;
               
                //circLightsOn(lights[1]);
            }
            else if (Mathf.FloorToInt(timer.timeRemaining) == timer.maxTime - 285)
            {
                lights[3].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(146, 0, 3, 0));
                lights[3].transform.position = oldBotPos;
                lights[3].GetComponent<HitMachine>().canHit = false;
                //circLightsOff(lights[1]);
            }
        }
    }

    public void circLightsOn(GameObject circLight)
    {

    }

    public void circLightsOff(GameObject circLight)
    {

    }

    public void triLightsActivate(GameObject triLight)
    {

    }

    public void triLightsDeactivate(GameObject triLight)
    {

    }


}
/*Notes:
 * Make the light bars require multiple hits to go away so one hit doesn't give it away
 * 
 */