using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Anna Breuker, Jacob Zydorowicz
 * Project 2
 * This scripts manages the moles that spawn and where they spawn.
 */

public class SpawnManager : MonoBehaviour
{
    public GameObject moles;
    private Timer time;


    //where the mole positions are stored
    public float[] spawnX;
    public float[] spawnZ;
    public bool[] moleHere;
    public float spawnPosY = 17;
    public int locationIndex;

    // Start is called before the first frame update
    void Start()
    {
        time = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        StartCoroutine(SpawnRandomPrefabWithCoroutine());
    }

    private void Update()
    {
        if(time.gameOver)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator SpawnRandomPrefabWithCoroutine()
    {
        //add a 3 second delay before first spawning moles
        yield return new WaitForSeconds(3f);

        while (true)
        {
            SpawnMole();

            float randomDelay = Random.Range(1.5f, 3.0f);

            yield return new WaitForSeconds(randomDelay);
        }
    }

    void SpawnMole()
    {
        //pick mole hole
        locationIndex = Random.Range(0, spawnX.Length);

        //generate a random spawn position from mole holes
        Vector3 spawnPos = new Vector3(spawnX[locationIndex], spawnPosY, spawnZ[locationIndex]);

        //spawn mole
        if (!moleHere[locationIndex])
        {
          
            
            Instantiate(moles, spawnPos, moles.transform.rotation);
           
            
            moleHere[locationIndex] = true;
        }
    }

}
