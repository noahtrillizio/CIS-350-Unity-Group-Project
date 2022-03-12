/*
 * Noah Trillizio
 * Project 2 Mole Mania
 * Controls the movement of the narrator
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarratorMovment : MonoBehaviour
{
    public GameObject Hammer;
    public GameObject moleMachineFinal;
    public Transform movingUp;
    public Transform movingDown;
    public float speed;
    public float whenNarratorMoves;
    public float duringGameTimer;
    public float earlyGameTimer;

    public bool called;
    public bool called2;

    private HammerHitMole HammerHitScript;
    private MachineMovement NarratorMidFightMove;

    // Start is called before the first frame update
    void Start()
    {
        HammerHitScript = Hammer.GetComponent<HammerHitMole>();
        NarratorMidFightMove = moleMachineFinal.GetComponent<MachineMovement>();
        duringGameTimer = 0;
        earlyGameTimer = 0;
        called = false;
        called2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        whenNarratorMoves = HammerHitScript.CurrentSounds;
        if (whenNarratorMoves == 3)
        {
            transform.position = Vector3.MoveTowards(transform.position, movingUp.position, step);
        }
        else if (whenNarratorMoves == 11 && earlyGameTimer <= 300)
        {
            transform.position = Vector3.MoveTowards(transform.position, movingDown.position, step);
            called = false;
            earlyGameTimer++;
        }
        else if (duringGameTimer >= 360 && called2 == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, movingDown.position, step);
            HammerHitScript.narratorText.text = "";
            HammerHitScript.panel.gameObject.SetActive(false);
            called = false;
        }
        else if (transform.position.y >= 28.58356 && whenNarratorMoves == 11 && called2 == true)
        {
            duringGameTimer++;
        }
        else if (NarratorMidFightMove.correctPats == 7 && transform.position.y <= 28.58356)
        {
            transform.position = Vector3.MoveTowards(transform.position, movingUp.position, step);
            HammerHitScript.narratorText.text = "No, please don't free the moles. You have no idea the atrocities these creature's have committed, they deserve it I swear!";
            HammerHitScript.panel.gameObject.SetActive(true);
            duringGameTimer = 0;
            called2 = true;
        }
        else if (duringGameTimer >= 300 && called == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, movingDown.position, step);
            HammerHitScript.narratorText.text = "";
            HammerHitScript.panel.gameObject.SetActive(false);
        }
        else if (transform.position.y >= 28.58356 && whenNarratorMoves == 11 && called == true)
        {
            duringGameTimer++;
        }
        else if (NarratorMidFightMove.correctPats == 5 && transform.position.y <= 28.58356)
        {
            transform.position = Vector3.MoveTowards(transform.position, movingUp.position, step);
            HammerHitScript.narratorText.text = "If you don't stop that right now you'll never be able to claim your legendary title!";
            HammerHitScript.panel.gameObject.SetActive(true);
            duringGameTimer = 0;
            called = true;
        }
        else if (duringGameTimer >= 300)
        {
            transform.position = Vector3.MoveTowards(transform.position, movingDown.position, step);
            HammerHitScript.narratorText.text = "";
            HammerHitScript.panel.gameObject.SetActive(false);
        }
        else if (transform.position.y >= 28.58356 && whenNarratorMoves == 11)
        {
            duringGameTimer++;
        }
        else if (NarratorMidFightMove.correctPats == 3 && transform.position.y <= 28.58356)
        {
            transform.position = Vector3.MoveTowards(transform.position, movingUp.position, step);
            HammerHitScript.narratorText.text = "Hey! That's destroying my machine stop that!";
            HammerHitScript.panel.gameObject.SetActive(true);
            duringGameTimer = 0;
        }
    }
}