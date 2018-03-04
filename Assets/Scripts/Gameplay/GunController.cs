using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunController : MonoBehaviour
{
    [Header("Weapon Parameters")]
    [SerializeField] Gun gun;
    public string gunName;
    public int currentAmmo;
    public int magSize, totalAmmo;
    public int damage;
    [SerializeField] AudioClip[] audioFiles = new AudioClip[2];
    bool canShoot = true;
    GameObject casing;
    Transform barrel;
    [SerializeField] GameObject hitDecal;

    [Space(10)]

    Animator animator;
    AudioSource audioSource;
    
    [SerializeField] GameObject cam;

    


	void Start ()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        cam = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(0).gameObject;

        InitializeWeapon();

        barrel = transform.Find("Barrel");
    }
	
	void InitializeWeapon()
    {
        gunName = gun.gunName;
        totalAmmo = gun.totalClips * gun.ammoCount;
        magSize = gun.ammoCount;
        currentAmmo = magSize;

        damage = gun.damage;

        casing = gun.gunCasing;
    }

    public void Shoot()
    {
        if (canShoot && currentAmmo > 0)
        {
            //Animation, ammo management.
            currentAmmo--;
            canShoot = false;
            if (currentAmmo > 0) animator.SetTrigger("Shot");
            else animator.SetTrigger("LastShot");
            PlaySound(0);
            cam.transform.DOPunchPosition(Vector3.forward * -0.05f, .15f, 7, 1, false);
            cam.transform.DOPunchRotation(Vector3.right * -1, .15f, 7, 1);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().UpdateUI();

            //Actually shooting.
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f))
            {
                Instantiate(hitDecal, hit.point, Quaternion.LookRotation(hit.normal), null);
            }

        }
        else if (canShoot && currentAmmo == 0 && totalAmmo > 0) Reload();
        else if (canShoot && currentAmmo == 0 && totalAmmo == 0) animator.SetTrigger("EmptyShot");
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
        audioSource.PlayOneShot(audioFiles[n]);
    }
}
