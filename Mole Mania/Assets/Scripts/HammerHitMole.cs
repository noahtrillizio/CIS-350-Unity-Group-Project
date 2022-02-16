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

    //public GameObject Mole;

    void Start()
    {
        scoreManagerScript = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mole")
        {
            //Debug.Log("hit");
            scoreManagerScript.score++;
            Instantiate(SpecialEffect, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
