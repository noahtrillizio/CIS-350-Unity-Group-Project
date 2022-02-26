/*
 * Caleb Kahn
 * Project 2
 * Makes the mole move and detects collision
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach to moles.
public class TutorialMoleMove : MonoBehaviour
{
    public float speed;
    public bool isUp;
    private TutorialManager tm;
    // Start is called before the first frame update

    private void Start()
    {
        tm = GameObject.FindGameObjectWithTag("TutorialManager").GetComponent<TutorialManager>();
    }

    void Update()
    {
        if (tm.mole)
        {
            transform.Translate(Vector3.up * .8f);
            tm.mole = false;
        }
        /*if (!isUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            isUp = true;
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            isUp = false;
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if(tm.timer > .45f) {
            tm.timer = 0;
            tm.posMovement++;
            transform.Translate(Vector3.down * .8f);
        }
    }
}