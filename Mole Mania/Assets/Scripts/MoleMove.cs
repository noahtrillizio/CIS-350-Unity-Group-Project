using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Anna Breuker
 * Mole Mania
 * This script makes the moles move up and down.
 * Will probably account for if the moles are hit once the
 * collision is all figured out :)
 */

//attach to moles.
public class MoleMove : MonoBehaviour
{
    public float speed;
    private bool isUp;
    // Start is called before the first frame update
    void Start()
    {
        isUp = false;
        Debug.Log("is this script even running");
        StartCoroutine(MoveMoleCoroutine());
    }

    IEnumerator MoveMoleCoroutine()
    {
        //add a 1 second delay before first spawning objects
        yield return new WaitForSeconds(1.0f);

        while (true)
        {
            MoveMole();

            float randomDelay = Random.Range(3.0f, 5.0f);

            yield return new WaitForSeconds(randomDelay);
        }

    }
    void MoveMole()
    {
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
    

}
