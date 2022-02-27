using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Anna Breuker, Ian Connors
 * Mole Mania
 * This script makes the moles move up and down.
 * Might make them do other things once we figure out the timer.
 */

//attach to moles.
public class MoleMove : MonoBehaviour
{
    public float speed;
    public bool isUp;
    private SpawnManager spawnManagerScript;

    private float yPos;
    private float xPos;
    private float zPos;
    // Start is called before the first frame update
    void Start()
    {
        isUp = false;
        //Debug.Log("is this script even running");
        StartCoroutine(MoveMoleCoroutine());
        spawnManagerScript = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();

        xPos = spawnManagerScript.spawnX[spawnManagerScript.locationIndex];
        yPos = spawnManagerScript.spawnPosY;
        zPos = spawnManagerScript.spawnZ[spawnManagerScript.locationIndex];
    }

    IEnumerator MoveMoleCoroutine()
    {
        //add a 1 second delay before first spawning objects
        yield return new WaitForSeconds(1.0f);

        //while (true)
        //{
            MoveMole();

            float randomDelay = Random.Range(3.0f, 5.0f);

            yield return new WaitForSeconds(randomDelay);
        Destroy(gameObject);
        //}

    }
    void MoveMole()
    {
        if (!isUp)
        {
            transform.position = new Vector3(xPos, yPos + speed, zPos); 
            isUp = true;
        }
        //else 
        //{
        //    transform.position = new Vector3(xPos, yPos, zPos);
        //    isUp = false;
        //}
    }
    

}
