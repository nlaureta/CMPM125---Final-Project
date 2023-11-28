using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticles : MonoBehaviour
{
    public ParticleSystem Blood;


    private void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.CompareTag("Player"))
        {
            var emit = Blood.emission;

            emit.enabled = true;

            Blood.Play();

            //Debug.Log("hit");
        }
    }
}
