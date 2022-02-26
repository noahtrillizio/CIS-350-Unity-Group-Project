using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CopiedPlayerBehavior : MonoBehaviour
{
    public float sanity = 100f;
    public Slider sanMeter;

    public GameObject Lantern;
    public GameObject LanternLight;
    public GameObject SceneLantern;
    public bool LanternActive;
    public float LanternFuel = 100f;
    public Slider lanMeter;

    private AudioSource[] wNoise;
    private bool wNPlaying = false;
    private bool correct;
    public bool draining = true;
    private bool pulsing = false;
    public GameObject door;
    public GameObject keyPad;
    public GameObject meterPulse;

    public GameObject ScratchUI;
    public GameObject BloodUI;
    public GameObject BloodPanelUI;

    bool SanityTier1;
    bool SanityTier2;
    bool SanityTier3;

    private Behaviour UpperMiddle;




    // Start is called before the first frame update
    void Start()
    {
        //UpperMiddle = GameObject.Find("FirstPuzzleController").GetComponent<FirstPuzzleController>();

        wNoise = GetComponents<AudioSource>();

        ScratchUI.gameObject.SetActive(false);
        BloodPanelUI.gameObject.SetActive(false);
        BloodUI.gameObject.SetActive(false);

        LanternActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            sanity += 100f;
            LanternFuel += 100f;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }

        LanternCheck();
        SanityCheck();



        if (draining && !pulsing)
        {
            StartCoroutine("Pulsing");
            pulsing = true;
        }

        if (draining)
        {
            sanity -= 1f * Time.deltaTime;
            sanMeter.value = sanity;
        }

        if (keyPad.activeSelf)
        {
            Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            print("correct scene");
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                print("quit");
                Application.Quit();
            }
        }
    }



    void SanityCheck()
    {
        if (sanity > 75)
        {
            SanityTier1 = false;
            SanityTier2 = false;
            SanityTier3 = false;
        }

        if (sanity <= 75 && sanity >= 51)
        {
            SanityTier1 = true;
            SanityTier2 = false;
            SanityTier3 = false;
        }

        else if (sanity <= 50 && sanity >= 26)
        {
            SanityTier1 = false;
            SanityTier2 = true;
            SanityTier3 = false;
        }

        else if (sanity <= 25)
        {
            SanityTier1 = false;
            SanityTier2 = false;
            SanityTier3 = true;
        }

        if (SanityTier1 == true)
        {
            ScratchUI.gameObject.SetActive(true);
            BloodPanelUI.gameObject.SetActive(false);
            BloodUI.gameObject.SetActive(false);
        }

        if (SanityTier2 == true)
        {
            ScratchUI.gameObject.SetActive(true);
            BloodPanelUI.gameObject.SetActive(false);
            BloodUI.gameObject.SetActive(true);
        }
        if (SanityTier3 == true)
        {
            ScratchUI.gameObject.SetActive(true);
            BloodPanelUI.gameObject.SetActive(true);
            BloodUI.gameObject.SetActive(true);
        }



    }

    void LanternCheck()
    {
        if (Input.GetKeyDown(KeyCode.F) && LanternActive == false)
        {
            Lantern.gameObject.SetActive(true);
            LanternLight.gameObject.SetActive(true);
            SceneLantern.gameObject.SetActive(false);

            LanternActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && LanternActive == true)
        {
            LanternLight.gameObject.SetActive(false);
            LanternActive = false;
        }

        if (LanternActive == true)
        {
            LanternFuel -= 1 * Time.deltaTime;

            lanMeter.value = LanternFuel;

        }

        if (LanternFuel == 0.0f)
        {
            LanternLight.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lantern")
        {
            draining = false;
        }

        if (other.gameObject.tag == "Skeleton")
        {
            SceneManager.LoadScene(4);
        }

        // first floor puzzle

        if (other.gameObject.name.Contains("1Trigger"))
        {
            //UpperMiddle.GetComponent<FirstPuzzleController>().T1Active = true;
        }

        if (other.gameObject.name.Contains("2Trigger"))
        {
            //UpperMiddle.GetComponent<FirstPuzzleController>().T2Active = true;
        }

        if (other.gameObject.name.Contains("3Trigger"))
        {
            //UpperMiddle.GetComponent<FirstPuzzleController>().T3Active = true;
        }

        if (other.gameObject.name.Contains("4Trigger"))
        {
            //UpperMiddle.GetComponent<FirstPuzzleController>().T4Active = true;
        }

        if (other.gameObject.name.Contains("5Trigger"))
        {
            //UpperMiddle.GetComponent<FirstPuzzleController>().T5Active = true;
        }

        if (other.gameObject.name.Contains("FinalTrigger"))
        {
            //UpperMiddle.GetComponent<FirstPuzzleController>().FinaleActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Lantern")
        {
            draining = true;
        }
    }

    IEnumerator Pulsing()
    {
        Color tempCol = meterPulse.GetComponent<Image>().color;
        wNoise[1].Play();

        while (draining)
        {
            for (float i = 0; i <= 1; i += 0.1f)
            {
                tempCol.a = i;
                meterPulse.GetComponent<Image>().color = tempCol;
                yield return new WaitForSeconds(0.1f);
            }
            for (float i = 1; i >= 0; i -= 0.1f)
            {
                tempCol.a = i;
                meterPulse.GetComponent<Image>().color = tempCol;
                yield return new WaitForSeconds(0.1f);
            }
        }

        pulsing = false;
        wNoise[1].Stop();
    }
}
