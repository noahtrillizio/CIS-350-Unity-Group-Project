/*
 * Jacob Zydorowicz, Anna Breuker, Ian Connors
 * Project 2 Mole Mania
 * Makes the light soruce in the scene blink with noise
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightBlink : MonoBehaviour
{

    public AudioSource blinkSound;
    Light myLight;
    // Start is called before the first frame update
    void Start()
    {
        myLight = gameObject.GetComponent<Light>();
        StartCoroutine(blink());
    }

    IEnumerator blink()
    {
        while(true)
        {
            myLight.intensity = Random.Range(0f, .5f);
            blinkSound.Play();
            yield return new WaitForSeconds(Random.Range(.5f, 2f));
        }
        
    }
}
