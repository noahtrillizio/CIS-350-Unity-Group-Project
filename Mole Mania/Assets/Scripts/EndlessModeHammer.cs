/*
 * Noah Trillizio
 * Project 2 Mole Mania
 * Controls the whacking of the moles in endless mode
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessModeHammer : MonoBehaviour
{
    private ScoreManager scoreManagerScript;
    private EndlessSpawn spawnManagerScript;
    private Timer gameTime;

    public AudioSource MoleRelatedSFX;
    public AudioClip molesHit;
    public AudioSource BackgroundMusic;

    public TextMesh scoreText;
    public TextMesh timeText;

    private float soundChanger;
    public float CurrentSounds;

    public Text narratorText;

    public GameObject panel;

    void Start()
    {
        gameTime = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        scoreManagerScript = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        spawnManagerScript = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<EndlessSpawn>();
        gameTime.timerIsRunning = true;
        scoreText.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
        panel.gameObject.SetActive(false);
        narratorText.gameObject.SetActive(false);
        CurrentSounds = 0;
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mole")
        {
            //Debug.Log("hit");
            spawnManagerScript.moleHere[other.GetComponent<EndlessMoveMole>().posIndex] = false;
            spawnManagerScript.numOfMoles--;
            scoreManagerScript.score++;
            MoleRelatedSFX.PlayOneShot(molesHit, 1.0f);
            //GameObject clone = (GameObject)Instantiate(SpecialEffect, other.gameObject.transform.position, Quaternion.identity);
            //Destroy(clone, 1.0f);
            Destroy(other.gameObject);
            //Destroy(gameObject);
        }
    }
}
