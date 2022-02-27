using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessMoveMole : MonoBehaviour
{
    public float speed;
    public bool isUp;
    private EndlessSpawn spawnManagerScript;

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

        xPos = spawnManagerScript.spawnX[spawnManagerScript.locationIndex];
        yPos = spawnManagerScript.spawnPosY;
        zPos = spawnManagerScript.spawnZ[spawnManagerScript.locationIndex];
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
            transform.position = new Vector3(xPos, yPos + speed, zPos);
            isUp = true;
        }
        else
        {
            transform.position = new Vector3(xPos, yPos, zPos);
            isUp = false;
        }
    }
}
