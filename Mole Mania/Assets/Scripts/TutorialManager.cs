/*
 * Caleb Kahn
 * Project 2
 * Manages tutorial camera movement and phases of tutorial
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    public Camera cam;
    public Text startingText;
    public Text tutorialText;

    public int posMovement = 0;
    public float timer = 0;
    private TutorialHammerSwing hammer;

    public bool mole = false;

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
                startingText.enabled = false;
                posMovement++;
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
            hammer.canSwing = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                posMovement = 10;
                tutorialText.enabled = false;
            }
            else if (hammer.swingHori || hammer.swingVert) {
                posMovement++;
                tutorialText.text = "Good Job!\nNow hit the mole";
                mole = true;
            }
        }
        else if (posMovement == 4) {
            timer += Time.deltaTime;
        }
        else if (posMovement == 5) {
            tutorialText.text = "Great\nNow for the real game";
            timer += Time.deltaTime;
            if (timer >= 3){
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
                SceneManager.LoadScene("ArcadeSetup");
            }
        }
    }
}
