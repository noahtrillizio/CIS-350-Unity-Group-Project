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
    public HammerSwing hammer;
    public float scoreMultiplier = 2000f;

    // Start is called before the first frame update
    void Start()
    {
        hammer.canSwing = false;
        //goodEnd = true;//Temp
        time = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        //startSpawn = GameObject.FindGameObjectWithTag("Hammer").GetComponent<HammerHitMole>();
        //StartCoroutine(SpawnRandomPrefabWithCoroutine());
    }

    void Update()
    {
        if (startSpawn.CurrentSounds > 10 && !started)
        {
            hammer.canSwing = true;
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
        yield return new WaitForSeconds(Random.Range(1f, 2f));
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
                if (scoreManager.kills <= 70)
			    {
                    spawnDelayMin = 2 * Mathf.Pow(1.06f, -scoreManager.kills);
                    spawnDelayMax = 4 * Mathf.Pow(1.015f, -2.2f * scoreManager.kills);
			    }
                else
                {
                    spawnDelayMin = 4 * Mathf.Pow(1.3f, .5f* (scoreManager.kills-80));
                    spawnDelayMax = 6 * Mathf.Pow(1.3f, .5f * (scoreManager.kills-75));
                }
                float randomDelay = Random.Range(spawnDelayMin, spawnDelayMax);

                yield return new WaitForSeconds(randomDelay);
			}
        }
    }
    //spawns mole from a random hole
    void SpawnMole(int locationIndex)
    {
        float timeAlive = 6 * Mathf.Pow(1.02f, -scoreManager.kills);
        float animationSpeed = 1f + (scoreManager.kills * .01f);
        scoreMultiplier = 2000f + (scoreManager.kills * 50);
        if (scoreManager.kills > 70)
        {
            timeAlive = 6f;
            animationSpeed = 1f;
            scoreMultiplier = 5f;
        } 
        numOfMoles++;
        //generates spawn position based on hole index
        Vector3 spawnPos = new Vector3(spawnX[locationIndex], spawnPosY, spawnZ[locationIndex]);

        //spawn mole
        int molePrefabNum = 0;
        //choses which mole to spawn based on current score
        if (scoreManager.kills < scoreToChangeMoles[0])
            molePrefabNum = 0;

        else if (scoreManager.kills < scoreToChangeMoles[1] && scoreManager.kills > scoreToChangeMoles[0] - 1)
            molePrefabNum = Random.Range(0, 2);

        else if (scoreManager.kills < scoreToChangeMoles[2] && scoreManager.kills > scoreToChangeMoles[1] - 1)
            molePrefabNum = Random.Range(1, 3);

        else if (scoreManager.kills > scoreToChangeMoles[2] - 1)
            molePrefabNum = 2;

        //spawns sign moles at certains scores
        if (scoreManager.kills > 7 && !moleSigns[0])
		{
            molePrefabNum = 3;
            moleSigns[0] = true;
            timeAlive = 8f;
            animationSpeed = 1f;
        }
        else if (scoreManager.kills > 14 && !moleSigns[1])
        {
            molePrefabNum = 4;
            moleSigns[1] = true;
            timeAlive = 8f;
            animationSpeed = 1f;
        }
        else if (scoreManager.kills > 20 && !moleSigns[2])
        {
            molePrefabNum = 5;
            moleSigns[2] = true;
            timeAlive = 8f;
            animationSpeed = 1f;
        }
        else if (scoreManager.kills > 32 && !moleSigns[3])
        {
            molePrefabNum = 6;
            moleSigns[3] = true;
            timeAlive = 8f;
            animationSpeed = 1f;
        }
        else if (scoreManager.kills > 46 && !moleSigns[4])
        {
            molePrefabNum = 7;
            moleSigns[4] = true;
            timeAlive = 8f;
            animationSpeed = 1f;
        }
        else if (scoreManager.kills > 70 && !moleSigns[5])
        {
            molePrefabNum = 8;
            moleSigns[5] = true;
            timeAlive = 8f;
            animationSpeed = 1f;
        }
        else if (scoreManager.kills > 74 && !moleSigns[6])
        {
            molePrefabNum = 9;
            moleSigns[6] = true;
            timeAlive = 8f;
            animationSpeed = 1f;
        }
        else if (scoreManager.kills > 78 && !moleSigns[7])
        {
            molePrefabNum = 5;
            moleSigns[7] = true;
            timeAlive = 8f;
            animationSpeed = 1f;
        }
        else if (scoreManager.kills > 80 && !moleSigns[8])
        {
            molePrefabNum = 4;
            moleSigns[8] = true;
            timeAlive = 8f;
            animationSpeed = 1f;
        }

        MoleMove mole = Instantiate(moles[molePrefabNum], spawnPos, moles[molePrefabNum].transform.rotation).GetComponent<MoleMove>();
        mole.timeAlive = timeAlive;
        mole.animationSpeed = animationSpeed;
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
