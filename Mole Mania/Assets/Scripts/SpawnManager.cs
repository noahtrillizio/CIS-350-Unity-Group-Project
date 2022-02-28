using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Anna Breuker, Jacob Zydorowicz, Trillizio, Caleb Kahn, Ian Connors
 * Project 2
 * This scripts manages the moles that spawn and where they spawn.
 */

public class SpawnManager : MonoBehaviour
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

    public HammerHitMole startSpawn;

    private bool started = false;

    public GameObject explosionMoles;
    public bool goodEnd = false;
    private float timer = 0;
    private float ranTime = .25f;

    // Start is called before the first frame update
    void Start()
    {
        //goodEnd = true;//Temp
        time = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        //startSpawn = GameObject.FindGameObjectWithTag("Hammer").GetComponent<HammerHitMole>();
        //StartCoroutine(SpawnRandomPrefabWithCoroutine());
    }

    private void Update()
    {
        if (started == true)
        {

        }
        else if (startSpawn.CurrentSounds == 9 && started == false)
        {
            StartCoroutine(SpawnRandomPrefabWithCoroutine());
            started = true;
        }

        if(time.gameOver)
        {
            StopAllCoroutines();
        }

        if (goodEnd) {
            timer += Time.deltaTime;
            if (timer > ranTime)
            {
                ranTime = Random.Range(.05f, .25f);
                SpawnExplosion();
                timer = 0;
            }
        }
    }
    IEnumerator SpawnRandomPrefabWithCoroutine()
    {
        //add a 3 second delay before first spawning moles
        yield return new WaitForSeconds(2f);
        while (!goodEnd)
        {
            SpawnMole();

            if (scoreManager.score < 10)
			{
                spawnDelayMin = 3;
                spawnDelayMax = 6;
			}
            if (scoreManager.score < 20 && scoreManager.score > 10)
            {
                spawnDelayMin = 1;
                spawnDelayMax = 4;
            }
            if (scoreManager.score < 40 && scoreManager.score > 20)
            {
                spawnDelayMin = 0.3f;
                spawnDelayMax = 2;
            }
            if (scoreManager.score > 40 && scoreManager.score < 60)
            {
                spawnDelayMin = 1;
                spawnDelayMax = 3;
            }
            if (scoreManager.score > 60)
            {
                spawnDelayMin = 2;
                spawnDelayMax = 4;
            }

            float randomDelay = Random.Range(spawnDelayMin, spawnDelayMax);

            yield return new WaitForSeconds(randomDelay);
        }
    }
    IEnumerator WaitForMole(int location)
    {
        yield return new WaitForSeconds(6f);
        moleHere[location] = false;
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

            moleHere[locationIndex] = true;
            StartCoroutine(WaitForMole(locationIndex));
        }
    }


    void SpawnExplosion()
    {
        //pick mole hole
        locationIndex = Random.Range(0, spawnX.Length);

        //generate a random spawn position from mole holes
        Vector3 spawnPos = new Vector3(spawnX[locationIndex], spawnPosY, spawnZ[locationIndex]);

        //spawn mole
        Instantiate(explosionMoles, spawnPos, moles[0].transform.rotation);
    }
}
