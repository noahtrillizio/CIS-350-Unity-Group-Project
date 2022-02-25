/*
 * Noah Trillizio & Anna Breuker
 * Project 2 Mole Mania
 * Makes blood splatter when hammer hits the mole
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HammerHitMole : MonoBehaviour
{
    public GameObject SpecialEffect;
    private ScoreManager scoreManagerScript;
    private SpawnManager spawnManagerScript;

    public AudioSource MoleRelatedSFX;
    public AudioClip molesHit;
    public AudioSource BackgroundMusic;
    public AudioSource Falling;
    public AudioSource HitGround;

    public TextMesh scoreText;
    public TextMesh timeText;

    private float soundChanger;
    private float CurrentSounds = 0;

    private float Timer = 160;

    void Start()
    {
        scoreManagerScript = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        spawnManagerScript = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        scoreText.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (CurrentSounds == 0)
        {
            Falling.Play();
            CurrentSounds++;
        }
        if (CurrentSounds == 1)
        {
            Timer--;
        }
        if (CurrentSounds == 1 && Timer == 0)
        {
            HitGround.Play();
            CurrentSounds++;
            scoreText.gameObject.SetActive(true);
            timeText.gameObject.SetActive(true);
        }
        if (CurrentSounds == 2 && Input.GetKeyDown(KeyCode.Space))
        {
            BackgroundMusic.Play();
        }
        if (CurrentSounds == 3)
        {
            soundChanger = (scoreManagerScript.score) * .01f;
            if (scoreManagerScript.score >= 3)
            {
                BackgroundMusic.pitch = (1f - soundChanger) + .03f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mole")
        {
            //Debug.Log("hit");
            spawnManagerScript.moleHere[spawnManagerScript.locationIndex] = false;
            scoreManagerScript.score++;
            MoleRelatedSFX.PlayOneShot(molesHit, 1.0f);
            GameObject clone = (GameObject)Instantiate (SpecialEffect, other.gameObject.transform.position, Quaternion.identity);
             Destroy(clone, 1.0f);
             Destroy(other.gameObject);
            //Destroy(gameObject);
        }
    }
}
