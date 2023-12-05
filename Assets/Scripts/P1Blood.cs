using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Blood : MonoBehaviour
{
    public ParticleSystem Blood1;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var emit = Blood1.emission;

            emit.enabled = true;

            Blood1.Play();

            //Debug.Log("hit");
        }
    }
}
