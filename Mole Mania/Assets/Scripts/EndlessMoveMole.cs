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
    public float animationSpeed = 1f;
    public float animationSpeedMultiplier = 1f;
    public float timeAlive;
    private float localTimer = 0f;
    public Animator moleAnimation;

    public bool isUp;
    private EndlessSpawn spawnManagerScript;
    private ScoreManager scoreManagerScript;
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
        scoreManagerScript = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();

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
            transform.position = new Vector3(xPos, yPos + (5 * (localTimer / moveTime)), zPos);
        }
        transform.position = new Vector3(xPos, yPos + 5, zPos);
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
            transform.position = new Vector3(xPos, yPos + 5 - (5 * ((localTimer - (timeAlive - moveTime)) / moveTime)), zPos);
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
