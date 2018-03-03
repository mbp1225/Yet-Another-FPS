using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casing : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    [SerializeField] float ejectionSpeed;

    Transform player;
    Rigidbody rb;

	void Start ()
    {
        Invoke("Destroy", timeToDestroy);
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody>();
        rb.velocity = ((player.right * Random.Range(0.85f,1.15f)) + (player.up * Random.Range(0.35f, 0.65f))) * (ejectionSpeed + Random.Range(-0.75f, 0.75f));
        rb.AddTorque(Vector3.up * Random.Range(.25f,3));
	}
	
	void Update ()
    {
		
	}

    void Destroy()
    {
        Destroy(gameObject);
    }
}
