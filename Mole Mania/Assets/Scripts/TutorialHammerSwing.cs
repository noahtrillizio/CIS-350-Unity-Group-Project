/*
 * Caleb Kahn
 * Project 2
 * Adjusted from hammer swing but still swings the hammer for the tutorial
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TutorialHammerSwing : MonoBehaviour
{
    public Camera cam;
    public float lowYFlat;
    public float highYFlat;
    public float lowYTop;
    public float swingTime;

    public bool canSwing;
    public bool swingHori;
    public bool swingVert;
    private float timer;
    readonly private Vector3 horiRot = new Vector3(30, -180, 67.5f);//x->90
    readonly private Vector3 vertRot = new Vector3(180, -30, -23);//y->
    readonly private Vector3 turn = new Vector3(.4f, -1, 0);
    readonly private Vector3 awayRot = new Vector3(30, -180, 60);
    readonly private Vector3 awayPos = new Vector3(-4, 6, -54);
    private float scale;
    private float HammerDistance;

    private void Start()
    {
        scale = transform.localScale.z / 100;
        HammerDistance = scale * 6;
        swingHori = false;
        swingVert = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSwing)
        {
            if (swingHori)
            {
                transform.Rotate(turn, Time.deltaTime * 60 / swingTime);
                transform.position = new Vector3(transform.position.x, transform.position.y - ((HammerDistance - scale) / swingTime) * Time.deltaTime, transform.position.z);
                //transform.Translate(Vector3.down * Time.deltaTime * 5 / swingTime);
                timer += Time.deltaTime;
                if (timer > swingTime)
                {
                    swingHori = false;
                    timer = 0;
                    transform.position = awayPos;
                    transform.eulerAngles = awayRot;
                }
            }
            else if (swingVert)
            {
                transform.Rotate(turn, Time.deltaTime * 60 / swingTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - ((HammerDistance - scale) / swingTime) * Time.deltaTime);
                //transform.Translate(Vector3.down * Time.deltaTime * 5 / swingTime);
                timer += Time.deltaTime;
                if (timer > swingTime)
                {
                    swingVert = false;
                    timer = 0;
                    transform.position = awayPos;
                    transform.eulerAngles = awayRot;
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {//0 = left click, 1 = right click, 2 = middle (if there are more buttons they work too)
             //*Use Other Debug*Debug.Log(cam.ScreenToWorldPoint(Input.mousePosition));//2D
                Ray r = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(r, out RaycastHit hit))
                {//If ray hit then covert hit into vector3
                 //If you want it to hit certain things and miss others then use layers
                    Vector3 pos = hit.point;
                    //Debug.Log(pos);
                    if (pos.x < -1.6)
                    {
                        if (pos.y > highYFlat)
                        {
                            if (pos.y < lowYTop)
                            {//Vertical Swing
                                transform.position = new Vector3(pos.x + .1f, pos.y, pos.z + HammerDistance);
                                transform.eulerAngles = vertRot;
                                swingVert = true;

                            }
                            else
                            {//Horizontal Swing
                                transform.position = new Vector3(pos.x, pos.y + HammerDistance, pos.z + .1f);
                                transform.eulerAngles = horiRot;
                                swingHori = true;
                            }
                        }
                        else if (pos.y >= lowYFlat && pos.y <= highYFlat)
                        {//Horizontal Swing
                            transform.position = new Vector3(pos.x, pos.y + HammerDistance, pos.z + .1f);
                            transform.eulerAngles = horiRot;
                            swingHori = true;
                        }
                        else
                        {//Virtical Swing
                            transform.position = new Vector3(pos.x + .1f, pos.y, pos.z + HammerDistance);
                            transform.eulerAngles = vertRot;
                            swingVert = true;
                        }
                    }
                }
            }
        }

    }
}