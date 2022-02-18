/*
 * Jacob Zydorowicz
 * Project 2 Mole Mania
 * Moves crit spots on machine based on running timer
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineMovement : MonoBehaviour
{
    
    private int lightsHit;
    private int phaseNum;
    private int correctPatterns;
    private bool correctPattern;
    Vector3 newTopLightPos, oldTopPos, newBotLightPos, oldBotPos, oldHatchPos, newHatchPos;
    public GameObject[] lights;
    public Timer timer;
    private ScoreManager scoreMan;
    private HammerSwing hammer;
    public GameObject startText;
    
   
    void Start()
    {
        correctPattern = false;
        hammer = GameObject.FindGameObjectWithTag("Hammer").GetComponent<HammerSwing>();
        scoreMan = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        startText.SetActive(false);
        phaseNum = 0;

        //gets current and future positons of moving lights
        newTopLightPos = new Vector3(lights[4].gameObject.transform.position.x, lights[4].gameObject.transform.position.y, lights[4].gameObject.transform.position.z) + new Vector3(0f, 3f, 0f);
        oldTopPos = new Vector3(lights[4].gameObject.transform.position.x, lights[4].gameObject.transform.position.y, lights[4].gameObject.transform.position.z);
        newBotLightPos = new Vector3(lights[3].gameObject.transform.position.x, lights[3].gameObject.transform.position.y, lights[3].gameObject.transform.position.z) + new Vector3(2f, 0f, 0f);
        oldBotPos = new Vector3(lights[3].gameObject.transform.position.x, lights[3].gameObject.transform.position.y, lights[3].gameObject.transform.position.z);
        oldHatchPos = new Vector3(GameObject.FindGameObjectWithTag("Hatch").transform.position.x, GameObject.FindGameObjectWithTag("Hatch").transform.position.y, GameObject.FindGameObjectWithTag("Hatch").transform.position.z);
        newHatchPos = new Vector3(GameObject.FindGameObjectWithTag("Hatch").transform.position.x, GameObject.FindGameObjectWithTag("Hatch").transform.position.y, GameObject.FindGameObjectWithTag("Hatch").transform.position.z) + new Vector3(0f, 4f, 0f);
    }

   
    void Update()
    {
        //starts "phase 0" of machine where player goes through tutorial with fake moles
        if (phaseNum == 0 && scoreMan.score >= 10)
        {
            hammer.canSwing = false;
            startText.SetActive(true);

            //when player hits space when prompted, game starts
            if (Input.GetKeyDown(KeyCode.Space))
            {
                timer.timerIsRunning = true;
                hammer.canSwing = true;
                startText.SetActive(false); ;
                scoreMan.score = 0;
                phaseNum++;
            }
        }

        //"Phase 1" of machine where player plays simons says with machine lights
        if (phaseNum == 1 && !timer.gameOver)
        {
            int[] lightPattern = {1,4,2,0,3};
            while(!correctPattern)
            {
                phase1Pattern(lightPattern);
            }
        }
     
    }


    public void lightsActivate(GameObject currentLight)
    {
        if(currentLight.CompareTag("topTriLight"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(169, 0, 0, 0));
            currentLight.transform.position = newTopLightPos;
            currentLight.GetComponent<HitMachine>().canHit = true;
        }
        else if(currentLight.CompareTag("botTriLight"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(169, 0, 0, 0));
            currentLight.transform.position = newBotLightPos;
            currentLight.GetComponent<HitMachine>().canHit = true;
        }
        else if(currentLight.CompareTag("TriangleLights"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(169, 0, 0, 0));
            currentLight.transform.position = newHatchPos;
            currentLight.GetComponent<HitMachine>().canHit = true;
        }
        else if(currentLight.CompareTag("CircleLights"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(169, 0, 0, 0));
            currentLight.GetComponent<HitMachine>().canHit = true;
        }
        
    }

    public void lightsDeactivate(GameObject currentLight)
    {
        if (currentLight.CompareTag("topTriLight"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(40, 0, 0, 0));
            currentLight.transform.position = oldTopPos;
            currentLight.GetComponent<HitMachine>().canHit = false;
        }
        else if (currentLight.CompareTag("botTriLight"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(40, 0, 0, 0));
            currentLight.transform.position = oldBotPos;
            currentLight.GetComponent<HitMachine>().canHit = false;
        }
        else if (currentLight.CompareTag("TriangleLights"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(40, 0, 0, 0));
            currentLight.transform.position = oldHatchPos;
            currentLight.GetComponent<HitMachine>().canHit = false;
        }
        else if(currentLight.CompareTag("CircleLights"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(40, 0, 0, 0));
            currentLight.GetComponent<HitMachine>().canHit = false;
        }
    }

    //displays the light order based off of an array that holds the order
    public void phase1Pattern(int[] lightOrder)
    {
        for(int i = 0; i< lights.Length;i++)
        {
            lightsActivate(lights[lightOrder[i]]);
            delayCoroutine();
            lightsDeactivate(lights[lightOrder[i]]);
        }
    }

    IEnumerator delayCoroutine()
    {
        yield return new WaitForSeconds(2f);
    }

}
/*Notes:
 * Make the light bars require multiple hits to go away so one hit doesn't give it away
 * 
 */