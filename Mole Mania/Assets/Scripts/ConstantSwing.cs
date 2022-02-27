/*
 * Caleb Kahn
 * Project 2
 * Constantly swings the hammer up and down
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSwing : MonoBehaviour
{

    private bool down;
    readonly private Vector3 turn = new Vector3(.4f, -1, 0);
    private float scale;
    private float HammerDistance;
    private float startY;

    private void Start()
    {
        scale = transform.localScale.z / 100;
        HammerDistance = scale * 6;
        down = true;
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (down) {
            transform.Rotate(turn, Time.deltaTime * 60);
            transform.position = new Vector3(transform.position.x, transform.position.y - (HammerDistance - scale) * Time.deltaTime, transform.position.z);
            if (transform.position.y <= startY - (HammerDistance - scale)) {
                down = false;
            }
        }
        else {
            transform.Rotate(-turn, Time.deltaTime * 60);
            transform.position = new Vector3(transform.position.x, transform.position.y + (HammerDistance - scale) * Time.deltaTime, transform.position.z);
            if (transform.position.y >= startY)
            {
                down = true;
            }
        }
    }
}
