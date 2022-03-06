using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorMovment : MonoBehaviour
{
    public GameObject Hammer;
    public Transform movingUp;
    public Transform movingDown;
    public float speed;
    public float whenNarratorMoves;

    private HammerHitMole OpeningMover;

    // Start is called before the first frame update
    void Start()
    {
        OpeningMover = Hammer.GetComponent<HammerHitMole>();
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        whenNarratorMoves = OpeningMover.CurrentSounds;
        if (whenNarratorMoves == 3)
        {
            transform.position = Vector3.MoveTowards(transform.position, movingUp.position, step);
        }
        if (whenNarratorMoves == 9)
        {
            transform.position = Vector3.MoveTowards(transform.position, movingDown.position, step);
        }
    }
}
