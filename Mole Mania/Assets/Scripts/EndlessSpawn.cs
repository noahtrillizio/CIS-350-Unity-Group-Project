using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Anna Breuker, Noah Trillizio, Ian Connors
 * Project 2 Mole Mania
 * Manages what ending occurs when timer.gameOver = true
 */
public class EndlessSpawn : MonoBehaviour
{
    public GameObject[] moles;
    public int[] scoreToChangeMoles;

    private Timer time;
    private ScoreManager scoreManager;

    public float spawnDelayMin = 1.5f;
    public float spawnDelayMax = 3.0f;

    //where the mole positions are stored
    public float[] spawnX;
    public float[] spawnZ;
    public bool[] moleHere;
    public float spawnPosY = 17;
    public int locationIndex;

    // Start is called before the first frame update
    void Start()
    {
        //goodEnd = true;//Temp
        time = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        StartCoroutine(SpawnRandomPrefabWithCoroutine());
    }

    void Update()
    {
        //if (scoreManager.score >= 30)
        //{
        //    spawnDelayMin = 1f;
        //    spawnDelayMax = 1.5f;
        //}
        //else if (scoreManager.score >= 20)
        //{
        //    spawnDelayMin = 1f;
        //    spawnDelayMax = 2f;
        //}
        //else if (scoreManager.score >= 10)
        //{
        //    spawnDelayMin = 1f;
        //    spawnDelayMax = 2.5f;
        //}
    }

    IEnumerator SpawnRandomPrefabWithCoroutine()
    {
        //add a 3 second delay before first spawning moles
        yield return new WaitForSeconds(2f);
        int scoreMod;
        while (true)
        {
            SpawnMole();
            if (scoreManager.score > 30)
                scoreMod = (scoreManager.score % 40) + 20;
            else
                scoreMod = scoreManager.score;


            spawnDelayMin = 4 - Mathf.FloorToInt(scoreMod / 3);
            spawnDelayMax = 6 - Mathf.FloorToInt(scoreMod / 3);
			
            
            float randomDelay = Random.Range(spawnDelayMin, spawnDelayMax);

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
            //choses which mole to spawn based on current score
            if (scoreManager.score < scoreToChangeMoles[0])
                Instantiate(moles[0], spawnPos, moles[0].transform.rotation);

            else if (scoreManager.score < scoreToChangeMoles[1] && scoreManager.score > scoreToChangeMoles[0] - 1)
                Instantiate(moles[1], spawnPos, moles[1].transform.rotation);

            else if (scoreManager.score < scoreToChangeMoles[2] && scoreManager.score > scoreToChangeMoles[1] - 1)
                Instantiate(moles[2], spawnPos, moles[2].transform.rotation);

            else if (scoreManager.score > scoreToChangeMoles[2] - 1)
                Instantiate(moles[3], spawnPos, moles[3].transform.rotation);

            moleHere[locationIndex] = true;
        }
    }
}
