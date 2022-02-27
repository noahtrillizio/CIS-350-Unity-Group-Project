/*
 * Jacob Zydorowicz, Anna Breuker
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
    public bool firstLight;
    private bool activeHealth;

    public Vector3 newTopLightPos, oldTopPos, newBotLightPos, oldBotPos, oldHatchPos, newHatchPos;

    public GameObject[] lights;
    public Slider healthBar;
    public GameObject healthBarGameObject;

    public Timer timer;

    public Queue<int> lightQueue;

    public Material machineBroke, machineDead;

    public AudioSource MachineSFX;
    public AudioClip machineHitSound;

    void Start()
    {
        activeHealth = false;
        firstLight = false;
        correctPattern = false;
        activePattern = false;

        phaseNum = 0;
        correctPats = 0;
        correctPats = 0;

        lightQueue = new Queue<int> { };

        healthBarGameObject = GameObject.FindGameObjectWithTag("Slider");
        healthBar = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        
        healthBarGameObject.SetActive(false);

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
        //"Phase 1" of machine where player plays simons says with machine lights
        if (!timer.gameOver)
        {
          

            //activates healthbar when first light is hit
            if(!activeHealth && firstLight)
            {
                activeHealth = true;
                healthBarGameObject.SetActive(true);
            }

            //sets healthbar and texture based on how many times the player gets a correct hammer pattern
            if(correctPats ==0)
            {
                healthBar.value = 100;
            }
            else if (correctPats == 1)
            {
                healthBar.value = 84;
            }
            else if(correctPats == 2)
            {
                healthBar.value = 66;
            }
            else if (correctPats == 3)
            {
                gameObject.GetComponent<Renderer>().material = machineBroke;
                healthBar.value = 50;
            }
            else if (correctPats == 4)
            {
                healthBar.value = 32;
            }
            else if (correctPats == 5)
            {
                gameObject.GetComponent<Renderer>().material = machineDead;
                healthBar.value = 16;
            }
            else if(correctPats == 6)
            {
                timer.gameOver = true;
                healthBar.value = 0;
                StopAllCoroutines();
            }

            //starts light pattern when one is not actively showing
            if (!activePattern && !timer.gameOver)
            {
                activePattern = true;
                StartCoroutine(patternCoroutine(randomPattern()));
            }
        }
     
    }

    //decides how to activate lights based on passed in light
    //parameter: currentlight - light being activated from an array of lights
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
    //parameter: currentlight - light being deactivated from an array of lights
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

    //displays light pattern and determines if player input it correctly
    //parameter: lightOrder - array of machine light indexes that represent the order of the pattern
    IEnumerator patternCoroutine(int[] lightOrder)
    {
        float waitTime = Random.Range(.5f, 1f);
        int timeInBetween = Random.Range(4, 5);

        do
        {
            foreach(int light in lightOrder)
            {
                print(light);
            }
            yield return new WaitForSeconds(timeInBetween);

            //disables all lights be default
            for (int i = 0; i < lightOrder.Length; i++)
            {
                lights[i].GetComponent<HitMachine>().canHit = false;
                lightsDeactivate(lights[i]);
            }

            //activates and deactivates lights in order based on parameter
            for(int i =0; i < lightOrder.Length;i ++)
            {
                lightsActivate(lights[lightOrder[i]]);
                yield return new WaitForSeconds(waitTime);
                lightsDeactivate(lights[lightOrder[i]]);
            }
            
            //enables all lights to be hit
            for (int i = 0; i < lightOrder.Length; i++)
            {
                lights[i].GetComponent<HitMachine>().canHit = true;
            }


            yield return new WaitForSeconds(5f);

            //checks if player correctly inputs light order
            int []lightCheck = lightQueue.ToArray();

            if(lightCheck.Length==5)
            {
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
            }
            else
            {
                correctPattern = false;
            }

            lightQueue.Clear();

            for (int i = 0; i < lightOrder.Length; i++)
            {
                lightsDeactivate(lights[lightOrder[i]]);
            }
        }
        while (!correctPattern);
        MachineSFX.PlayOneShot(machineHitSound, 1.0f);
        correctPats++;
        activePattern = false;
    }

    //generates a array with random non repeating values from 0-4
    private int[] randomPattern()
    {

        List<int> nums = new List<int> { 0, 1, 2, 3, 4 };
        List<int> listPat = new List<int> { };

        while(listPat.Count<5)
        {
            int next = Random.Range(0, 5);

            if(nums.Contains(next))
            {
                nums.Remove(next);
                listPat.Add(next);
            }
        }

        int[] pattern = listPat.ToArray();

        return pattern;
        
    }
}

