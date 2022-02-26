using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Anna Breuker, Jacob Zydorowicz, Caleb Kahn
 * Project 2
 * This scripts manages the moles that spawn and where they spawn.
 */

public class SpawnManager : MonoBehaviour
{
    public GameObject moles;
    public GameObject fakeMoles;
    public GameObject explosionMoles;

    public MachineMovement machineScript;

    //where the mole positions are stored
    public float[] spawnX;
    public float[] spawnZ;
    public bool[] moleHere;
    public float spawnPosY = 17;
    public int locationIndex;

    bool goodEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        //goodEnd = true;//Temp
        machineScript = GameObject.FindGameObjectWithTag("Machine").GetComponent<MachineMovement>();
        StartCoroutine(SpawnRandomPrefabWithCoroutine());
    }

    IEnumerator SpawnRandomPrefabWithCoroutine()
    {
        //add a 3 second delay before first spawning moles
        yield return new WaitForSeconds(3f);

        while (!goodEnd)
        {
            SpawnMole();

            float randomDelay = Random.Range(1.5f, 3.0f);

            yield return new WaitForSeconds(randomDelay);
        }

        while (true)
        {
            SpawnExplosion();
            yield return new WaitForSeconds(Random.Range(.05f, .25f));
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
            if(machineScript.phaseNum ==0)
            {
                Instantiate(fakeMoles, spawnPos, moles.transform.rotation);
            }
            else
            {
                Instantiate(moles, spawnPos, moles.transform.rotation);
            }
            
            moleHere[locationIndex] = true;
        }
    }

    void SpawnExplosion()
    {
        //pick mole hole
        locationIndex = Random.Range(0, spawnX.Length);

        //generate a random spawn position from mole holes
        Vector3 spawnPos = new Vector3(spawnX[locationIndex], spawnPosY, spawnZ[locationIndex]);

        //spawn mole
        Instantiate(explosionMoles, spawnPos, moles.transform.rotation);
    }

}
