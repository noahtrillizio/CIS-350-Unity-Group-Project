using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHitMole : MonoBehaviour
{
    public GameObject SpecialEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Mole") return;
        {
            Instantiate(SpecialEffect, new Vector3(0, 5, 0), Quaternion.identity);
        }
    }
}
