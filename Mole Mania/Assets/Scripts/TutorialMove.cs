using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMove : MonoBehaviour
{

    public Camera cam;

    private int posMovement = 0;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        posMovement = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (posMovement == 1) {
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
    }
}
