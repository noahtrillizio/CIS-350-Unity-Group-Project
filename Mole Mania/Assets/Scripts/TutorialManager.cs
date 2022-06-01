/*
 * Caleb Kahn, Noah Trillizio
 * Project 2
 * Manages tutorial camera movement and phases of tutorial
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialManager : MonoBehaviour
{

    public Camera cam;
    public GameObject[] startingObjects;
    public TextMeshProUGUI tutorialText;

    public int posMovement = 0;
    public float timer = 0;
    private TutorialHammerSwing hammer;
    private float textboxUpdater = 0;

    public bool mole = false;

    public AudioSource BackgroundMusic;
    public GameObject loadingPannel;

    private void Start()
    {
        hammer = GameObject.FindGameObjectWithTag("Hammer").GetComponent<TutorialHammerSwing>();
        tutorialText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (posMovement == 0) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                for (int i = 0; i < startingObjects.Length; i++)
                {
                    startingObjects[i].active = false;
                }
                posMovement++;
                BackgroundMusic.Play();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                loadingPannel.SetActive(true);
                SceneManager.LoadScene("EndlessMode");
            }
        }
        else if (posMovement == 1) {
            cam.transform.Rotate(Vector3.left, Time.deltaTime * 10 / 3);
            //cam.transform.position = new Vector3(transform.position.x, transform.position.y - (5f / 3) * Time.deltaTime, transform.position.z - (20f / 3) * Time.deltaTime);
            cam.transform.Translate(Vector3.down * Time.deltaTime * 3.5f / 3);
            cam.transform.Translate(Vector3.forward * Time.deltaTime * 20 / 3);
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                posMovement++;
                timer = 0;
            }
        }
        else if (posMovement == 2) {
            cam.transform.Rotate(Vector3.right, Time.deltaTime * 15 / 3);
            //cam.transform.position = new Vector3(transform.position.x, transform.position.y - (5f / 3) * Time.deltaTime, transform.position.z - (20f / 3) * Time.deltaTime);
            cam.transform.Translate(Vector3.up * Time.deltaTime * 4.25f / 3);
            cam.transform.Translate(Vector3.forward * Time.deltaTime * 15 / 3);
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                posMovement++;
                timer = 0;
            }
        }
        else if (posMovement == 3) {
            tutorialText.enabled = true;
            //if (Input.GetKeyDown(KeyCode.Space)) {
            //    posMovement = 10;
            //    tutorialText.enabled = false;
            //}
            //else if (hammer.swingHori || hammer.swingVert) {
            //    posMovement++;
            //    tutorialText.text = "Good Job!\nNow hit the mole";
            //    mole = true;
            //}
            if (Input.GetKeyDown(KeyCode.Space)&& textboxUpdater == 0)
            {
                tutorialText.text = "You must be the legendary whacker of moles!\n<Space To Continue>";
                textboxUpdater++;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && textboxUpdater == 1)
            {
                posMovement++;
                hammer.canSwing = true;
                tutorialText.text = "Now take this legendary hammer of mole wacking, and click on the mole to whack him back into the darkness from which he came!";
                //textboxUpdater++;
                mole = true;
            }
        }
        else if (posMovement == 4) {
            timer += Time.deltaTime;
        }
        else if (posMovement == 5) {
            if (textboxUpdater == 1)
            {
                tutorialText.text = "Amazing!\nYou are truly the one destined to whack them all!\n<Space To Continue>";
                textboxUpdater++;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && textboxUpdater == 2)
            {
                tutorialText.text = "If you'll come with me your prowess is required elsewhere\n<Space To Continue>";
                textboxUpdater++;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && textboxUpdater == 3)
            {
                textboxUpdater++;
            }
            else if (textboxUpdater == 4)
            {
                posMovement++;
                tutorialText.enabled = false;
                timer = 0;
            }
        }
        else if (posMovement >= 6) {
            hammer.canSwing = false;
            cam.transform.Rotate(Vector3.left, Time.deltaTime * 15 / 3);
            //cam.transform.position = new Vector3(transform.position.x, transform.position.y - (5f / 3) * Time.deltaTime, transform.position.z - (20f / 3) * Time.deltaTime);
            cam.transform.Translate(Vector3.forward * Time.deltaTime * 15.5f / 2);
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                posMovement++;
                timer = 0;
                loadingPannel.SetActive(true);
                SceneManager.LoadScene("ArcadeSetup");
            }
        }
    }
}
