using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEnginePartiles;
    [SerializeField] ParticleSystem leftThrusterPartiles;
    [SerializeField] ParticleSystem rightThrusterPartiles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEnginePartiles.isPlaying)
        {
            mainEnginePartiles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEnginePartiles.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);

        if (!rightThrusterPartiles.isPlaying)
        {
            rightThrusterPartiles.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);

        if (!leftThrusterPartiles.isPlaying)
        {
            leftThrusterPartiles.Play();
        }
    }

    void StopRotating()
    {
        rightThrusterPartiles.Stop();
        leftThrusterPartiles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezing rotation so the physics system can take over   
    }


}