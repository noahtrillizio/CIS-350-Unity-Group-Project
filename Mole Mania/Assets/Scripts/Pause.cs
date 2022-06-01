using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject hammer;
    public GameObject pausePannel;
    public GameObject loadingPannel;
    public bool paused;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0f;
        pausePannel.SetActive(true);
        HammerSwing hammerSwing = hammer.GetComponent<HammerSwing>();
        if (hammerSwing == null)
        {
            hammer.GetComponent<TutorialHammerSwing>().canSwing = false;
        }
        else
        {
            hammerSwing.canSwing = false;
        }
    }

    public void UnpauseGame()
    {
        paused = false;
        Time.timeScale = 1f;
        pausePannel.SetActive(false);
        HammerSwing hammerSwing = hammer.GetComponent<HammerSwing>();
        if (hammerSwing == null)
        {
            if (GameObject.FindGameObjectWithTag("TutorialManager").GetComponent<TutorialManager>().posMovement == 4)
            {
                hammer.GetComponent<TutorialHammerSwing>().canSwing = true;
            }
        }
        else
        {
            hammerSwing.canSwing = true;
        }
    }

    public void ReturnToMainMenu()
    {
        loadingPannel.SetActive(true);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tutorial");
    }
}
