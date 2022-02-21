/*
 * Noah Trillizio
 * Project 2 Mole Mania
 * Makes blood splatter when hammer hits the mole
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHitMole : MonoBehaviour
{
    //made a few small changes here to make it count score - anna
    public GameObject SpecialEffect;
    private ScoreManager scoreManagerScript;

    public AudioSource MolesHit;
    public AudioSource BackgroundMusic;

    void Start()
    {
        scoreManagerScript = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        if (scoreManagerScript.score >= 15)
        {
            BackgroundMusic.pitch = .25f;
        }
        else if (scoreManagerScript.score >= 10)
        {
            BackgroundMusic.pitch = .5f;
        }
        else if (scoreManagerScript.score >= 5)
        {
            BackgroundMusic.pitch = .75f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mole")
        {
            //Debug.Log("hit");
            scoreManagerScript.score++;
            MolesHit.Play();
            /* GameObject clone = (GameObject)Instantiate (SpecialEffect, other.gameObject.transform.position, Quaternion.identity);
             Destroy(clone, 1.0f);
             Destroy(other.gameObject);*/
            Destroy(gameObject);
        }
    }
}
