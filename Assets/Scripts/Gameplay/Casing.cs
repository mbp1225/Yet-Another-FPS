using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casing : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    [SerializeField] float ejectionSpeed;

    Transform playerGun;
    Rigidbody rb;

	void Start ()
    {
        Invoke("Destroy", timeToDestroy);
        playerGun = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().gun.transform;

        rb = GetComponent<Rigidbody>();
        rb.velocity = ((playerGun.right * Random.Range(0.85f,1.15f)) + (playerGun.up * Random.Range(0.35f, 0.65f))) * (ejectionSpeed + Random.Range(-0.75f, 0.75f));
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
