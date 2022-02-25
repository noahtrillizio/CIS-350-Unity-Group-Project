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
    private int correctPats;
    public int phaseNum;

    private bool activePattern;
    bool correctPattern;



    public Vector3 newTopLightPos, oldTopPos, newBotLightPos, oldBotPos, oldHatchPos, newHatchPos;

    public GameObject[] lights;
   

    public Timer timer;
    private ScoreManager scoreMan;

    public Queue<int> lightQueue;

    public Material machineBroke, machineDead;



    void Start()
    {
        correctPats = 0;
        correctPattern = false;
        lightQueue = new Queue<int> { };
        activePattern = false;
        scoreMan = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        phaseNum = 0;
        correctPats = 0;

        //gets current and future positons of moving lights
        newTopLightPos = new Vector3(lights[4].gameObject.transform.position.x, lights[4].gameObject.transform.position.y, lights[4].gameObject.transform.position.z) + new Vector3(0f, 3f, 0f);
        oldTopPos = new Vector3(lights[4].gameObject.transform.position.x, lights[4].gameObject.transform.position.y, lights[4].gameObject.transform.position.z);

        newBotLightPos = new Vector3(lights[3].gameObject.transform.position.x, lights[3].gameObject.transform.position.y, lights[3].gameObject.transform.position.z) + new Vector3(2f, 0f, 0f);
        oldBotPos = new Vector3(lights[3].gameObject.transform.position.x, lights[3].gameObject.transform.position.y, lights[3].gameObject.transform.position.z);

        oldHatchPos = new Vector3(GameObject.FindGameObjectWithTag("Hatch").transform.position.x, GameObject.FindGameObjectWithTag("Hatch").transform.position.y, GameObject.FindGameObjectWithTag("Hatch").transform.position.z);
        newHatchPos = new Vector3(GameObject.FindGameObjectWithTag("Hatch").transform.position.x, GameObject.FindGameObjectWithTag("Hatch").transform.position.y, GameObject.FindGameObjectWithTag("Hatch").transform.position.z) + new Vector3(0f, -3f, 0f);

    }

   
    void Update()
    {
        //starts "phase 0" of machine where player goes through tutorial with fake moles
        if (phaseNum == 0 && scoreMan.score == 0)
        {
                phaseNum++;   
        }

        //"Phase 1" of machine where player plays simons says with machine lights
        if (phaseNum == 1 && !timer.gameOver)
        {
            int[] lightPattern = {1,4,2,0,3};
           
            if(!activePattern)
            {
                activePattern = true;
                StartCoroutine(patternCoroutine(lightPattern));
            }

            if(correctPats == 2)
            {
               
                gameObject.GetComponent<Renderer>().material = machineBroke;
            }

            if (correctPats == 3)
            {
             
                gameObject.GetComponent<Renderer>().material = machineDead;
            }

            if(correctPats == 4 )
            {
                timer.gameOver = true;
            }
        }
     
    }

    //decides how to activate lights based on passed in light
    //parameter: currentlight - light from lights array being activated
    public void lightsActivate(GameObject currentLight)
    {
        if(currentLight.CompareTag("topTriLight"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(169, 0, 0, 0));
            currentLight.transform.position = newTopLightPos;
        }
        else if(currentLight.CompareTag("botTriLight"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(169, 0, 0, 0));
            currentLight.transform.position = newBotLightPos;
        }
        else if(currentLight.CompareTag("TriangleLights"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(169, 0, 0, 0));
            GameObject.FindGameObjectWithTag("Hatch").transform.position = newHatchPos;

        }
        else if(currentLight.CompareTag("CircleLights"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(169, 0, 0, 0));
        }
        
    }

    //decides how to deactivate lights based on passed in light
    //parameter: currentlight - light from lights array being deactivated
    public void lightsDeactivate(GameObject currentLight)
    {
        if (currentLight.CompareTag("topTriLight"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(40, 0, 0, 0));
        }
        else if (currentLight.CompareTag("botTriLight"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(40, 0, 0, 0));
        }
        else if (currentLight.CompareTag("TriangleLights"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(40, 0, 0, 0));

        }
        else if(currentLight.CompareTag("CircleLights"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(40, 0, 0, 0));
        }
    }

    IEnumerator patternCoroutine(int[] lightOrder)
    {
        do
        {
            yield return new WaitForSeconds(5f);

            for (int i = 0; i < lightOrder.Length; i++)
            {
                lights[i].GetComponent<HitMachine>().canHit = false;
                lightsDeactivate(lights[i]);
            }

            //activates and deactivates lights in order based on parameter
            for(int i =0; i < lightOrder.Length;i ++)
            {
                lightsActivate(lights[lightOrder[i]]);
                yield return new WaitForSeconds(2f);
                lightsDeactivate(lights[lightOrder[i]]);
            }
            
            //enables all lights to be hit
            for (int i = 0; i < lightOrder.Length; i++)
            {
                lights[i].GetComponent<HitMachine>().canHit = true;
            }

            yield return new WaitForSeconds(10f);

            int []lightCheck = lightQueue.ToArray();

            foreach(int lightInd in lightCheck)
            {
                Debug.Log(lightInd);
            }

            for (int i = 0; i < lightCheck.Length; i++)
            {
                if (!lightCheck[i].Equals(lightOrder[i]))
                {
                    correctPattern = false;
                    break;
                }
                else
                {
                    correctPattern = true;
                }
               
            }
            
            Debug.Log(correctPattern);
            lightQueue.Clear();
        }
        while (!correctPattern);

        correctPats++;
        Debug.Log(correctPats);
        activePattern = false;

        
        


    }

}
/*Notes:
 * Make the light bars require multiple hits to go away so one hit doesn't give it away
 * 
 */