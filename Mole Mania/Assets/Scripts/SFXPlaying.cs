using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlaying : MonoBehaviour
{
    public AudioSource MolesHit;
    public AudioSource MachineHit;

    public void PlayMolesHit()
    {
        MolesHit.Play();
    }

    public void PlayMachineHit()
    {
        MachineHit.Play();
    }

    void OnTriggerEnter()
    {
        MolesHit.Play();
    }
}
