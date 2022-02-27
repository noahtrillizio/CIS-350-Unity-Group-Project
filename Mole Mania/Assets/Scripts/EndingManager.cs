using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        scoreManagerScript = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        spawnManagerScript = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        machineScript = GameObject.FindGameObjectWithTag("Machine").GetComponent<MachineMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreManagerScript.score >= 50)
        {
            timer.gameOver = true;
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-30, -40, 0);
            GameObject.FindGameObjectWithTag("Player").transform.rotation = Quaternion.Euler(20, -90, 0);
            //need text and the ability to press r to restart.
            //also this just straight up doesn't work.
        }
        else if (timer.gameOver && machineScript.correctPats == 9)
        {
            //can't find the good end bool
        }
        else if (timer.gameOver)
        { 
            //need some kind of game over text like "you ran out of time! try again?"
        }
    }
}
