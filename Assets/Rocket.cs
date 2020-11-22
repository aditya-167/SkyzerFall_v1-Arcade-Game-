﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float Throttle = 20f;
    
    Rigidbody rigidBody;
    AudioSource audioSource;
    // Start is called before the first frame update
              
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;

        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationSpeed);

        }

        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(-Vector3.forward * rotationSpeed);
        rigidBody.freezeRotation = false;
    }

    private void Thrust()
    {

        float thrustSpeed = Throttle * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * Throttle);
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
        
    }
}
