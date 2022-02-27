/*
* Caleb Kahn
* Project 2
* Makes the mole move and detects collision
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionMove : MonoBehaviour
{

    private float speed;
    private Vector3 turn;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        turn = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        speed = Random.Range(35f, 70f);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 1.5)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            speed -= speed / 2 * Time.deltaTime;
            transform.Rotate(turn, Time.deltaTime * 60);
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}