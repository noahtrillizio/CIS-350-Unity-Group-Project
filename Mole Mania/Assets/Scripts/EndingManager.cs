﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 * Anna Breuker
 * Project 2 Mole Mania
 * Manages what ending occurs when timer.gameOver = true
 */
public class EndingManager : MonoBehaviour
{
    private Timer timer;
    private ScoreManager scoreManagerScript;
    private SpawnManager spawnManagerScript;
    private MachineMovement machineScript;
    public Text endingText;

    private bool started;

    public Camera mainCam;
    public Camera badEndCam;


    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        scoreManagerScript = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        spawnManagerScript = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        machineScript = GameObject.FindGameObjectWithTag("Machine").GetComponent<MachineMovement>();
        endingText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreManagerScript.score >= 50 || timer.gameOver)//score gets above a certain amount or time runs out.
        {
            timer.gameOver = true;
            mainCam.enabled = false;
            badEndCam.enabled = true;
            //GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-30, -40, 0); //this is buggy.
            //GameObject.FindGameObjectWithTag("Player").transform.rotation = Quaternion.Euler(20, -90, 0); //this is also buggy.
            if (started == true)
            {

            }
            else if (started == false)
            {
                StartCoroutine(ChangeBadEndText());
                started = true;
            }

            if (Input.GetKeyDown(KeyCode.R)) // reset
            {
                SceneManager.LoadScene("ArcadeSetup");
            }
        }
        else if (timer.gameOver && machineScript.correctPats == 9)
        {
            endingText.text = ("You destroyed the machine and freed the moles!\nPress R to restart");
            if (Input.GetKeyDown(KeyCode.R)) //reset
            {
                SceneManager.LoadScene("ArcadeSetup");
            }

        }
    }


    
    IEnumerator ChangeBadEndText()
    {
    endingText.text = ("Your score: " + scoreManagerScript.score + "\nPress R to restart");

    yield return new WaitForSeconds(5f);

    endingText.text = ("Moles killed: " + scoreManagerScript.score + "\nPress R to restart");

        yield return new WaitForSeconds(2f);
    }

}