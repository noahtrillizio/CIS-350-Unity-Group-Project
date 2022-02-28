/*
 * Noah Trillizio, Anna Breuker, Ian Connors
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
    private Timer gameTime;

    public AudioSource MoleRelatedSFX;
    public AudioClip molesHit;
    public AudioSource BackgroundMusic;
    public AudioSource Falling;
    public AudioSource HitGround;

    public TextMesh scoreText;
    public TextMesh timeText;

    public Text narratorText;

    public GameObject panel;

    private float soundChanger;
    public float CurrentSounds = 0;

    private float Timer = 176;

    void Start()
    {
        gameTime = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        scoreManagerScript = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        spawnManagerScript = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        setUIActive(false);
    }

    void Update()
    {
        if (CurrentSounds == 0)
        {
            Falling.Play();
            CurrentSounds++;
        }
        else if (CurrentSounds == 1 && Timer == 0)
        {
            HitGround.Play();
            CurrentSounds++;
            //gameTime.timerIsRunning = true;
        }
        else if (CurrentSounds == 1)
        {
            Timer--;
        }
        else if (CurrentSounds == 2)
        {
            Timer = 40;
            CurrentSounds++;
        }
        else if (CurrentSounds == 3 && Timer == 0)
        {
            narratorText.enabled = true;
            panel.gameObject.SetActive(true);
            CurrentSounds++;
        }
        else if (CurrentSounds == 3)
        {
            Timer--;
        }
        else if (CurrentSounds == 4 && Input.GetKeyDown(KeyCode.Space))
        {
            narratorText.text = "This may look like an ordinary wack-a-mole machine in a back room, but I assure you, its anything but!\n<Space To Continue>";
            CurrentSounds++;
        }
        else if (CurrentSounds == 5 && Input.GetKeyDown(KeyCode.Space))
        {
            narratorText.text = "This is the where your metal will be tested to see if you can truly claim yourself to be the legendary whacker of moles!\n<Space To Continue>";
            CurrentSounds++;
        }
        else if (CurrentSounds == 6 && Input.GetKeyDown(KeyCode.Space))
        {
            narratorText.text = "If you feel the challange is too insermountable you may leave at any point.\n<Space To Continue>";
            CurrentSounds++;
        }
        else if (CurrentSounds == 7 && Input.GetKeyDown(KeyCode.Space))
        {
            narratorText.text = "However, I see a fire in your eyes that says you will see this through to the end!\n<Space To Continue>";
            CurrentSounds++;
        }
        else if (CurrentSounds == 8 && Input.GetKeyDown(KeyCode.Space))
        {
            narratorText.text = "";
            BackgroundMusic.Play();
            scoreText.gameObject.SetActive(true);
            gameTime.timerIsRunning = true;
            timeText.gameObject.SetActive(true);
            panel.gameObject.SetActive(false);
            CurrentSounds++;
        }
        else if (CurrentSounds == 9)
        {
            soundChanger = (scoreManagerScript.score) * .01f;
            if (scoreManagerScript.score >= 3)
            {
                BackgroundMusic.pitch = (1f - soundChanger) + .03f;
            }
        }
    }
    public void setUIActive(bool act)
	{
        scoreText.gameObject.SetActive(act);
        timeText.gameObject.SetActive(act);
        narratorText.enabled = act;
        panel.gameObject.SetActive(act);
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
