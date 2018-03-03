using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casing : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    [SerializeField] float ejectionSpeed;

    AudioSource audioSource;

    Transform player;
    Rigidbody rb;

	void Start ()
    {
        audioSource = GetComponent<AudioSource>();

        Invoke("Destroy", timeToDestroy);
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody>();
        rb.velocity = ((player.right * Random.Range(0.85f,1.15f)) + (player.up * Random.Range(0.35f, 0.65f))) * (ejectionSpeed + Random.Range(-0.75f, 0.75f));
        rb.AddTorque(Vector3.up * Random.Range(.25f,3));
        rb.AddTorque(Vector3.right * Random.Range(.25f, 3));
        rb.AddTorque(Vector3.forward * Random.Range(.25f, 3));
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
    }
}
