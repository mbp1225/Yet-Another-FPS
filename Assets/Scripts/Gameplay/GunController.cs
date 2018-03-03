using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunController : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    [SerializeField] AudioClip[] audioFiles = new AudioClip[2];
    [SerializeField] GameObject cam;

    bool canShoot = true;
    public GameObject casing;

    [Space(10)]

    [Header("Weapon Parameters")]
    public int currentAmmo, magSize, totalAmmo;


	void Start ()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        cam = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(0).gameObject;

        currentAmmo = magSize;
	}
	
	void Update ()
    {
		
	}

    public void Shoot()
    {
        if (canShoot && currentAmmo > 0)
        {
            currentAmmo--;
            canShoot = false;
            animator.SetTrigger("Shot");
            PlaySound(0);
            cam.transform.DOPunchPosition(Vector3.forward * -0.05f, .15f, 7, 1, false);
            cam.transform.DOPunchRotation(Vector3.right * -1, .15f, 7, 1);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().UpdateUI();
        }
        else if (canShoot && currentAmmo == 0) Reload();
        else return;
    }

    void EjectCasing()
    {
        Instantiate(casing, transform.position + Vector3.up * 0.2f + Vector3.right * 0.05f, Quaternion.identity, null);
    }

    void ResetShot()
    {
        canShoot = true;
    }

    public void Reload()
    {
        if (currentAmmo == magSize + 1) return;
        if (totalAmmo == 0) return;
        else if (currentAmmo > 0 && totalAmmo > magSize)
        {
            canShoot = false;
            animator.SetTrigger("Reload");
            currentAmmo = magSize + 1;
            totalAmmo -= magSize;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().UpdateUI();
        }
        else if (currentAmmo > 0 && totalAmmo <= magSize)
        {
            canShoot = false;
            animator.SetTrigger("Reload");
            currentAmmo = currentAmmo + totalAmmo; //Can reload for values higher than the total;
            totalAmmo = 0;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().UpdateUI();
        }
        else
        {
            canShoot = false;
            animator.SetTrigger("Reload");
            currentAmmo = magSize;
            totalAmmo -= magSize;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().UpdateUI();
        }
    }

    public void PlaySound(int n)
    {
        switch (n)
        {
            case 0:
                audioSource.PlayOneShot(audioFiles[0]);
                break;
            case 1:
                break;
        }
    }
}
