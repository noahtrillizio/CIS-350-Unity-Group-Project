/*
 * Noah Trillizio
 * Project 2 Mole Mania
 * Makes blood splatter when hammer hits the mole
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHitMole : MonoBehaviour
{
    public GameObject SpecialEffect;

    public GameObject Mole;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Mole") return;
        {
            Instantiate(SpecialEffect, GameObject.FindGameObjectWithTag("Mole").transform.position, Quaternion.identity);
            Destroy(GameObject.FindGameObjectWithTag("Mole"));
        }
    }
}
