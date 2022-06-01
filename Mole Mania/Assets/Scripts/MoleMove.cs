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
    public float animationSpeed = 1f;
    public float animationSpeedMultiplier = 1f;
    public float timeAlive;
    private float localTimer = 0f;

    public bool isUp;
    private SpawnManager spawnManagerScript;
    private ScoreManager scoreManagerScript;
    public int posIndex = 0;
    public bool isSignMole;
    public Animator moleAnimation;

    private float yPos;
    private float xPos;
    private float zPos;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("is this script even running");
        StartCoroutine(MoveMoleCoroutine());
        spawnManagerScript = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        scoreManagerScript = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        /*if (!isSignMole)
            speed = 5 + scoreManagerScript.kills * 5 / 20;
        else
            speed = 5;*/
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
        isUp = false;
        moleAnimation.SetFloat("animationSpeed", animationSpeed * animationSpeedMultiplier);
        float moveTime = timeAlive / 8;
        while (localTimer < moveTime)
        {
            localTimer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
            transform.position = new Vector3(xPos, yPos + (7*(localTimer / moveTime)), zPos);
        }
        transform.position = new Vector3(xPos, yPos + 7, zPos);
        isUp = true;
        while (localTimer < timeAlive - moveTime)
        {
            localTimer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        isUp = false;
        while (localTimer < timeAlive)
        {
            localTimer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
            transform.position = new Vector3(xPos, yPos + 7 - (7 * ((localTimer - (timeAlive - moveTime)) / moveTime)), zPos);
        }
        spawnManagerScript.moleHere[posIndex] = false;
        spawnManagerScript.numOfMoles--;
        Destroy(gameObject);
    }

    public float getPercentLived()
    {
        return localTimer / timeAlive;
    }

}
