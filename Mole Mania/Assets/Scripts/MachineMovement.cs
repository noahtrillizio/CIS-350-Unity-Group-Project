/*
 * Jacob Zydorowicz, Anna Breuker, Ian Connors
 * Project 2 Mole Mania
 * Moves crit spots on machine based on running timer
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineMovement : MonoBehaviour
{
    public int correctPats;
    private bool activePattern;
    bool correctPattern;
    public bool firstLight;
    private bool activeHealth;
    public bool hatchDown = false;
    public bool topLightOut = false;
    public bool bottomLightOut = false;
    private GameObject hatch;

    public Vector3 newTopLightPos, oldTopPos, newBotLightPos, oldBotPos, oldHatchPos, newHatchPos;

    public GameObject[] lights;
    public Slider healthBar;
    public GameObject healthBarGameObject;

    public SpawnManager spawnManager;

    public Timer timer;

    public Queue<int> lightQueue;

    public Material[] machineMat;

    public AudioSource MachineSFX;
    public AudioClip machineHitSound;
    public AudioSource failedPatternSound;
    public AudioSource normalPlayMusic;
    public AudioSource goodEndingMusic;

    void Start()
    {
        hatch = GameObject.FindGameObjectWithTag("Hatch");
        activeHealth = false;
        firstLight = false;
        correctPattern = false;
        activePattern = false;
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

        oldHatchPos = new Vector3(hatch.transform.position.x, hatch.transform.position.y, hatch.transform.position.z);
        newHatchPos = new Vector3(hatch.transform.position.x, hatch.transform.position.y, hatch.transform.position.z) + new Vector3(0f, -3f, 0f);
    }

   
    void Update()
    {
        //"Phase 1" of machine where player plays simons says with machine lights
        if (!timer.gameOver&&timer.timerIsRunning)
        {
            //activates healthbar when first light is hit
            if(!activeHealth && firstLight)
            {
                activeHealth = true;
                healthBarGameObject.SetActive(true);
                healthBar.value = 100;
            }

            //starts light pattern when one is not actively showing
            if (!activePattern && !timer.gameOver)
            {
                activePattern = true;
                if (correctPats == 0)
                {
                    StartCoroutine(HitAllButtons());
                }
                else
                {
                    StartCoroutine(PatternCoroutine());
                }
            }
        }
    }

    IEnumerator machineBreakFlashiness(Material oldMat, Material newMat, int flashes)
	{
        for (int i = 0; i < flashes; i++)
		{
            gameObject.GetComponent<Renderer>().material = newMat;
            yield return new WaitForSeconds(0.05f);
            gameObject.GetComponent<Renderer>().material = oldMat;
            yield return new WaitForSeconds(0.05f);
		}
        gameObject.GetComponent<Renderer>().material = newMat;
    }

    //decides how to activate lights based on passed in light
    //parameter: currentlight - light being activated from an array of lights
    public void lightsActivate(GameObject currentLight)
    {
        if(currentLight.CompareTag("topTriLight"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(169, 0, 0, 0));
            if (!topLightOut)
            {
                StartCoroutine(MoveTopLightOut());
            }
        }
        else if(currentLight.CompareTag("botTriLight"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(169, 0, 0, 0));
            if (!bottomLightOut)
            {
                StartCoroutine(MoveBottomLightOut());
            }
        }
        else if(currentLight.CompareTag("TriangleLights"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(169, 0, 0, 0));
            if (!hatchDown)
            {
                StartCoroutine(MoveHatchDown());
            }

        }
        else if(currentLight.CompareTag("CircleLights"))
        {
            currentLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(169, 0, 0, 0));
        }
        
    }

    IEnumerator MoveTopLightOut()
    {
        topLightOut = true;
        float timer = 0f;
        while (timer < .2f)
        {
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
            lights[4].transform.position = (newTopLightPos - oldTopPos) * (timer / .2f) + oldTopPos;
        }
        lights[4].transform.position = newTopLightPos;
    }

    public void CallMoveTopLightIn()
    {
        StartCoroutine(MoveTopLightIn());
    }

    IEnumerator MoveTopLightIn()
    {
        topLightOut = false;
        float timer = 0f;
        while (timer < .2f)
        {
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
            lights[4].transform.position = (oldTopPos - newTopLightPos) * (timer / .2f) + newTopLightPos;
        }
        lights[4].transform.position = oldTopPos;
    }

    IEnumerator MoveBottomLightOut()
    {
        bottomLightOut = true;
        float timer = 0f;
        while (timer < .2f)
        {
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
            lights[3].transform.position = (newBotLightPos - oldBotPos) * (timer / .2f) + oldBotPos;
        }
        lights[3].transform.position = newBotLightPos;
    }

    public void CallMoveBottomLightIn()
    {
        StartCoroutine(MoveBottomLightIn());
    }

    IEnumerator MoveBottomLightIn()
    {
        bottomLightOut = false;
        float timer = 0f;
        while (timer < .2f)
        {
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
            lights[3].transform.position = (oldBotPos - newBotLightPos) * (timer / .2f) + newBotLightPos;
        }
        lights[3].transform.position = oldBotPos;
    }

    IEnumerator MoveHatchDown()
    {
        hatchDown = true;
        float timer = 0f;
        while (timer < .2f)
        {
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
            hatch.transform.position = (newHatchPos - oldHatchPos) * (timer / .2f) + oldHatchPos;
        }
        hatch.transform.position = newHatchPos;
    }

    public void CallMoveHatchUp()
    {
        StartCoroutine(MoveHatchUp());
    }

    IEnumerator MoveHatchUp()
    {
        hatchDown = false;
        float timer = 0f;
        while (timer < .2f)
        {
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
            hatch.transform.position = (oldHatchPos - newHatchPos) * (timer/.2f) + newHatchPos;
        }
        hatch.transform.position = oldHatchPos;
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

    IEnumerator HitAllButtons()
    {
        yield return new WaitForSeconds(Random.Range(2f, 3f));

        do
        {
            //resets all lights
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].GetComponent<HitMachine>().canHit = false;
                lightsActivate(lights[i]);
                yield return new WaitForSeconds(Random.Range(.1f, .2f));
            }

            yield return new WaitForSeconds(Random.Range(.5f, .8f));

            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].GetComponent<HitMachine>().canHit = true;
                lightsDeactivate(lights[i]);
                yield return new WaitForSeconds(Random.Range(.1f, .2f));
            }

            float playerInputWindow = waitForSecs(Random.Range(9f, 15f));

            yield return new WaitUntil(() => ((lightQueue.Count == 5) || (timer.timeRemaining <= playerInputWindow)));

            //checks if player hit all buttons
            if (lightQueue.Count == 5)
            {
                correctPattern = true;
            }
            else if (lightQueue.Count > 0)
            {
                failedPatternSound.Play();
            }
            lightQueue.Clear();
        }
        while (!correctPattern);
        correctPattern = false;
        MachineSFX.PlayOneShot(machineHitSound, 1.0f);
        correctPats++;
        activePattern = false;

        StartCoroutine(machineBreakFlashiness(machineMat[1], machineMat[0], 5));
        healthBar.value = 83;
    }

    //displays light pattern and determines if player input it correctly
    //parameter: lightOrder - array of machine light indexes that represent the order of the pattern
    IEnumerator PatternCoroutine()
    {
        int[] lightOrder = randomPattern();
        yield return new WaitForSeconds(Random.Range(3f, 4f));

        do
        {
            yield return new WaitForSeconds(Random.Range(1.5f, 2f));
            /*string lightNumbers = "";
            foreach (int light in lightOrder)
            {
                lightNumbers += light;
            }
            Debug.Log("Current Pattern: " + lightNumbers);*/
            //activates and deactivates lights in order based on parameter
            for(int i =0; i < correctPats;i ++)
            {
                lightsActivate(lights[lightOrder[i]]);
                yield return new WaitForSeconds(Random.Range(.6f, .8f));
                lightsDeactivate(lights[lightOrder[i]]);
                if (i+1 != correctPats)
                {
                    yield return new WaitForSeconds(Random.Range(.1f, .2f));
                }
            }
            
            //enables all lights to be hit
            for (int i = 0; i < lights.Length; i++)
            {
                lightsActivate(lights[i]);
                lightsDeactivate(lights[i]);
                lights[i].GetComponent<HitMachine>().canHit = true;
            }

            float playerInputWindow = waitForSecs(Random.Range(8f, 11.5f));

            yield return new WaitUntil(() => ((lightQueue.Count == correctPats) || (timer.timeRemaining <= playerInputWindow) || CheckFailedPattern(lightOrder)));

            //checks if player correctly inputs light order
            int []lightCheck = lightQueue.ToArray();

            //only checks correct pattern if minimum number of lights were hit
            if(lightCheck.Length==correctPats)
            {
                for (int i = 0; i < correctPats; i++)
                {
                    if (!lightCheck[i].Equals(lightOrder[i]))
                    {
                        correctPattern = false;
                        failedPatternSound.Play();
                        break;
                    }
                    else
                    {
                        correctPattern = true;
                    }
                }
            }
            //only plays in correct sound if more than 1 light is hit to allow player to accidently discover mechanics
            else if (lightCheck.Length > 0)
            {
                failedPatternSound.Play();
            }

            lightQueue.Clear();
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].GetComponent<HitMachine>().canHit = false;
            }
        }
        while (!correctPattern);
        correctPattern = false;
        MachineSFX.PlayOneShot(machineHitSound, 1.0f);
        correctPats++;
        activePattern = false;

        if (correctPats == 2)
        {
            StartCoroutine(machineBreakFlashiness(machineMat[0], machineMat[2], 19));
            healthBar.value = 67;
        }

        else if (correctPats == 3)
        {
            StartCoroutine(machineBreakFlashiness(machineMat[0], machineMat[2], 19));
            healthBar.value = 50;
        }
        else if (correctPats == 4)
        {
            StartCoroutine(machineBreakFlashiness(machineMat[2], machineMat[1], 13));
            healthBar.value = 33;
        }

        else if (correctPats == 5)
        {
            StartCoroutine(machineBreakFlashiness(machineMat[2], machineMat[3], 10));
            healthBar.value = 17;
        }
        else if (correctPats == 6)
        {
            StartCoroutine(machineBreakFlashiness(machineMat[3], machineMat[4], 30));
            timer.gameOver = true;
            healthBar.value = 0;
            spawnManager.goodEnd = true;
            StopAllCoroutines();
            normalPlayMusic.Stop();
            goodEndingMusic.Play();
        }
    }

    private bool CheckFailedPattern(int[] lightOrder)
    {
        int[] lightCheck = lightQueue.ToArray();
        for (int i = 0; i < lightCheck.Length; i++)
        {
            if (!lightCheck[i].Equals(lightOrder[i]))
            {
                return true;
            }
        }
        return false;
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

    private float waitForSecs(float seconds)
    {
    
        float waitForTime = timer.timeRemaining - seconds;
        if (waitForTime > 0)
        {
            return waitForTime;
        }
        else
            return 0f;
       
    }
}