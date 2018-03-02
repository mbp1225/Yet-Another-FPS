using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] GameObject casing;
    [SerializeField] AudioClip shot;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        GetComponent<Animator>().SetTrigger("Shoot");
        audioSource.PlayOneShot(shot);
    }

    public void EjectCasing()
    {
        Instantiate(casing, transform.position, Quaternion.identity, null);
    }

    public void EndShot()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetShooting();
    }
}
