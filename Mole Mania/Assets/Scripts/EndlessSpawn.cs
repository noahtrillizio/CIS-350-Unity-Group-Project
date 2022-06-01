/*
 * Noah Trillizio (Based off spawn manager)
 * Project 2 Mole Mania
 * Controls display of score and adjusts the difficulty
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * Anna Breuker, Noah Trillizio, Ian Connors
 * Project 2 Mole Mania
 * Manages what ending occurs when timer.gameOver = true
 */
public class EndlessSpawn : MonoBehaviour
{
    public GameObject[] moles;
    public int[] scoreToChangeMoles;

    private ScoreManager scoreManager;
    public GameObject controlsText;

    public float spawnDelayMin = 1.5f;
    public float spawnDelayMax = 3.0f;

    //where the mole positions are stored
    public float[] spawnX;
    public float[] spawnZ;
    public bool[] moleHere;
    public float spawnPosY = 17;
    public int numOfMoles;
    public float scoreMultiplier = 200f;
    public GameObject loadingPannel;

    // Start is called before the first frame update
    void Start()
    {
        //goodEnd = true;//Temp
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        StartCoroutine(SpawnRandomPrefabWithCoroutine());
        controlsText.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.R)) // reset
        {
            loadingPannel.SetActive(true);
            SceneManager.LoadScene("EndlessMode");
        }
        if (Input.GetKeyDown(KeyCode.T)) // title screen
        {
            loadingPannel.SetActive(true);
            SceneManager.LoadScene("Tutorial");
        }
    }

    IEnumerator SpawnRandomPrefabWithCoroutine()
    {
        //add a 3 second delay before first spawning moles
        yield return new WaitForSeconds(2f);
        controlsText.SetActive(true);
        while (true)
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
                spawnDelayMin = Mathf.Pow(1.015f, -scoreManager.kills) + .05f;
                spawnDelayMax = 3f * Mathf.Pow(1.005f, -2.2f * scoreManager.kills) +.2f;
                float randomDelay = Random.Range(spawnDelayMin, spawnDelayMax);

                yield return new WaitForSeconds(randomDelay);
            }
        }

    }
    void SpawnMole(int locationIndex)
    {
        numOfMoles++;

        //generate a random spawn position from mole holes
        Vector3 spawnPos = new Vector3(spawnX[locationIndex], spawnPosY, spawnZ[locationIndex]);

        //spawn mole
        int molePrefabNum = 0;

        float timeAlive = 3 * Mathf.Pow(1.01f, -scoreManager.kills) + .65f;
        float animationSpeed = 1f + (scoreManager.kills * .01f);
        scoreMultiplier = 200f + (scoreManager.kills * 1.6f);

        EndlessMoveMole mole = Instantiate(moles[molePrefabNum], spawnPos, moles[molePrefabNum].transform.rotation).GetComponent<EndlessMoveMole>();
        mole.timeAlive = timeAlive;
        mole.animationSpeed = animationSpeed;
        moleHere[locationIndex] = true;
        moleHere[locationIndex] = true;
    }
}
