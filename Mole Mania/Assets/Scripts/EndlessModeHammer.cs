using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessModeHammer : MonoBehaviour
{
    public GameObject SpecialEffect;
    private ScoreManager scoreManagerScript;
    private SpawnManager spawnManagerScript;
    private Timer gameTime;

    public AudioSource MoleRelatedSFX;
    public AudioClip molesHit;
    public AudioSource BackgroundMusic;

    public TextMesh scoreText;
    public TextMesh timeText;

    private float soundChanger;
    public float CurrentSounds;

    void Start()
    {
        gameTime = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        scoreManagerScript = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        spawnManagerScript = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        scoreText.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
        CurrentSounds = 0;
    }

    void Update()
    {
        if (CurrentSounds == 0)
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
            //GameObject clone = (GameObject)Instantiate(SpecialEffect, other.gameObject.transform.position, Quaternion.identity);
            //Destroy(clone, 1.0f);
            Destroy(other.gameObject);
            //Destroy(gameObject);
        }
    }
}
