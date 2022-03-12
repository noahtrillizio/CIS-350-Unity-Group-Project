using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Anna Breuker, Jacob Zydorowicz, Noah Trillizio, Caleb Kahn, Ian Connors
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
    public int numOfMoles;

    public HammerHitMole startSpawn;

    private bool started = false;

    public GameObject explosionMoles;
    public bool goodEnd = false;
    private float timer = 0;
    private float ranTime = .25f;

    public bool[] moleSigns;

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
                SpawnExplosion(Random.Range(0, spawnX.Length));
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
            //doesn't spawn mole if all the holes are occupied
            yield return new WaitUntil(() => numOfMoles < 8);
            while (numOfMoles < 8)
			{
                //finds an unoccupied hole
                int location = Random.Range(0, spawnX.Length);
                while (moleHere[location])
                    location = Random.Range(0, spawnX.Length);

                //spawns a mole
                SpawnMole(location);

                //changes speed of spawning based on score
                if (scoreManager.score < 10)
			    {
                    spawnDelayMin = 2;
                    spawnDelayMax = 4;
			    }
                if (scoreManager.score < 20 && scoreManager.score > 10)
                {
                    spawnDelayMin = 1;
                    spawnDelayMax = 2;
                }
                if (scoreManager.score < 40 && scoreManager.score > 20)
                {
                    spawnDelayMin = 0.3f;
                    spawnDelayMax = 1;
                }
                if (scoreManager.score > 40)
                {
                    spawnDelayMin = 0.1f;
                    spawnDelayMax = 1;
                }
                float randomDelay = Random.Range(spawnDelayMin, spawnDelayMax);

                yield return new WaitForSeconds(randomDelay);
			}
        }
    }
    //spawns mole from a random hole
    void SpawnMole(int locationIndex)
    {
        numOfMoles++;
        //generates spawn position based on hole index
        Vector3 spawnPos = new Vector3(spawnX[locationIndex], spawnPosY, spawnZ[locationIndex]);

        //spawn mole
        int molePrefabNum = 0;
        //choses which mole to spawn based on current score
        if (scoreManager.score < scoreToChangeMoles[0])
            molePrefabNum = 0;

        else if (scoreManager.score < scoreToChangeMoles[1] && scoreManager.score > scoreToChangeMoles[0] - 1)
            molePrefabNum = Random.Range(0, 2);

        else if (scoreManager.score < scoreToChangeMoles[2] && scoreManager.score > scoreToChangeMoles[1] - 1)
            molePrefabNum = Random.Range(1, 3);

        else if (scoreManager.score > scoreToChangeMoles[2] - 1)
            molePrefabNum = 2;

        //spawns sign moles at certains scores
        if (scoreManager.score > 21 && !moleSigns[0])
		{
            molePrefabNum = 3;
            moleSigns[0] = true;
        }
        if (scoreManager.score > 46 && !moleSigns[1])
        {
            molePrefabNum = 4;
            moleSigns[1] = true;
        }
        if (scoreManager.score > 59 && !moleSigns[2])
        {
            molePrefabNum = 5;
            moleSigns[2] = true;
        }
        if (scoreManager.score > 86 && !moleSigns[3])
        {
            molePrefabNum = 6;
            moleSigns[3] = true;
        }
        if (scoreManager.score > 103 && !moleSigns[4])
        {
            molePrefabNum = 7;
            moleSigns[4] = true;
        }
        if (scoreManager.score > 133 && !moleSigns[5])
        {
            molePrefabNum = 8;
            moleSigns[5] = true;
        }
        if (scoreManager.score > 165 && !moleSigns[6])
        {
            molePrefabNum = 9;
            moleSigns[6] = true;
        }
        if (scoreManager.score > 207 && !moleSigns[7])
        {
            molePrefabNum = 5;
            moleSigns[7] = true;
        }
        if (scoreManager.score > 298 && !moleSigns[8])
        {
            molePrefabNum = 4;
            moleSigns[8] = true;
        }

        Instantiate(moles[molePrefabNum], spawnPos, moles[molePrefabNum].transform.rotation);
        moleHere[locationIndex] = true;
    }


    void SpawnExplosion(int locationIndex)
    {
        //generate a random spawn position from mole holes
        Vector3 spawnPos = new Vector3(spawnX[locationIndex], spawnPosY, spawnZ[locationIndex]);

        //spawn mole
        Instantiate(explosionMoles, spawnPos, moles[0].transform.rotation);
    }
}
