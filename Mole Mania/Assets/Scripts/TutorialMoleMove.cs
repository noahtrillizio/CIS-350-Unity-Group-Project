/*
 * Caleb Kahn (copied scrpt from Anna Breuker)
 * Project 2
 * Makes the mole move and detects collision
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach to moles.
public class TutorialMoleMove : MonoBehaviour
{
    public GameObject SpecialEffect;
    public AudioSource TutorialMolesHit;
    public float speed;
    public bool isUp;
    private TutorialManager tm;
    private TutorialHammerSwing hammer;
    // Start is called before the first frame update

    private void Start()
    {
        tm = GameObject.FindGameObjectWithTag("TutorialManager").GetComponent<TutorialManager>();
        hammer = GameObject.FindGameObjectWithTag("Hammer").GetComponent<TutorialHammerSwing>();
    }

    void Update()
    {
        if (tm.mole)
        {
            transform.Translate(Vector3.up * .7f);
            tm.mole = false;
        }
        if (!isUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            isUp = true;
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            isUp = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(tm.timer > .45f && hammer.swingHori) {
            tm.timer = 0;
            tm.posMovement++;
            transform.Translate(Vector3.down * .7f);
            //GameObject clone = (GameObject)Instantiate(SpecialEffect, other.gameObject.transform.position, Quaternion.identity);
            TutorialMolesHit.Play();
        }
    }
}