using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSwing : MonoBehaviour
{
    public Camera cam;
    public float lowYFlat;
    public float highYFlat;
    public float lowYTop;
    public float swingTime;
    public float HammerDistance;

    private bool swingHori;
    private bool swingVert;
    private float timer;
    readonly private Vector3 horiPos = new Vector3(30, -90, 67.5f);//x->90
    readonly private Vector3 vertPos = new Vector3(180, 30, -23);//y->-90

    private void Start()
    {
        swingHori = false;
        swingVert = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (swingHori) {
            transform.Rotate(Vector3.down, Time.deltaTime * 60 / swingTime);
            transform.position = new Vector3(transform.position.x, transform.position.y - ((HammerDistance-3f)/swingTime) * Time.deltaTime, transform.position.z);
            //transform.Translate(Vector3.down * Time.deltaTime * 5 / swingTime);
            timer += Time.deltaTime;
            if (timer > swingTime) {
                swingHori = false;
                timer = 0;
            }
        }
        else if (swingVert) {
            transform.Rotate(Vector3.down, Time.deltaTime * 60 / swingTime);
            transform.position = new Vector3(transform.position.x - ((HammerDistance - 3f) / swingTime) * Time.deltaTime, transform.position.y, transform.position.z);
            //transform.Translate(Vector3.down * Time.deltaTime * 5 / swingTime);
            timer += Time.deltaTime;
            if (timer > swingTime)
            {
                swingVert = false;
                timer = 0;
            }
        }
        else if(Input.GetMouseButtonDown(0)) {//0 = left click, 1 = right click, 2 = middle (if there are more buttons they work too)
            //Debug.Log(cam.ScreenToWorldPoint(Input.mousePosition));//2D
            Ray r = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(r, out RaycastHit hit)) {//If ray hit then covert hit into vector3
                //If you want it to hit certain things and miss others then use layers
                Vector3 pos = hit.point;
                Debug.Log(pos);
                if (pos.y > highYFlat) {
                    if (pos.y < lowYTop) {//Vertical Swing
                        transform.position = new Vector3(pos.x + + HammerDistance, pos.y, pos.z +.4f);
                        transform.eulerAngles = vertPos;
                        swingVert = true;

                    }
                    else {//Horizontal Swing
                        transform.position = new Vector3(pos.x+.4f, pos.y + HammerDistance, pos.z);
                        transform.eulerAngles = horiPos;
                        swingHori = true;
                    }
                }
                else if (pos.y >= lowYFlat && pos.y <= highYFlat) {//Horizontal Swing
                    transform.position = new Vector3(pos.x+.4f, pos.y + HammerDistance, pos.z);
                    transform.eulerAngles = horiPos;
                    swingHori = true;
                }
                else {//Virtical Swing
                    transform.position = new Vector3(pos.x + +HammerDistance, pos.y, pos.z + .4f);
                    transform.eulerAngles = vertPos;
                    swingVert = true;
                }
            } 
        }
    }
}
