/*
 * Noah Trillizio, Ian Connors
 * Project 2 Mole Mania
 * Controls mole movment in endless mode
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessMoveMole : MonoBehaviour
{
    public float speed;
    public bool isUp;
    private EndlessSpawn spawnManagerScript;
    public int posIndex = 0;

    private float yPos;
    private float xPos;
    private float zPos;
    // Start is called before the first frame update
    void Start()
    {
        isUp = false;
        //Debug.Log("is this script even running");
        StartCoroutine(MoveMoleCoroutine());
        spawnManagerScript = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<EndlessSpawn>();

        xPos = transform.position.x;
        yPos = transform.position.y;
        zPos = transform.position.z;
        if (zPos == -4)
            posIndex = 3;
        else if (zPos == 4)
            posIndex = 2;

        else if (zPos == 0)
        {
            if (xPos == 1)
                posIndex = 0;
            else
                posIndex = 1;
        }
        else if (zPos == -8)
        {
            if (xPos == 1)
                posIndex = 4;
            else
                posIndex = 5;
        }
        else if (zPos == 8)
        {
            if (xPos == 1)
                posIndex = 6;
            else
                posIndex = 7;
        }
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
        spawnManagerScript.moleHere[posIndex] = false;
        spawnManagerScript.numOfMoles--;
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
