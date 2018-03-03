using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Camera Variables")]
    [SerializeField] Camera cam;
    [SerializeField] int fovNormal, fovAim;

    [Space(10)]

    [Header("Weapon Variables")]
    [SerializeField] Gun[] guns = new Gun[1];
    [SerializeField] GameObject currentGun;
    [SerializeField] Transform gunPosition;
    [SerializeField] Vector3 hipPosition, adsPosition;
    [SerializeField] float aimSpeed;

    [Space(10)]

    [Header("UI Variables")]
    [SerializeField] TextMeshProUGUI gunName;
    [SerializeField] TextMeshProUGUI currentAmmo, totalAmmo, fireMode;

    void Start ()
    {
        cam.fieldOfView = fovNormal;

        gunPosition.localPosition = hipPosition;
        adsPosition = new Vector3(0, guns[0].aimHeight, hipPosition.z);

        currentGun.GetComponent<GunController>().casing = guns[0].gunCasing;

        InitializeWeapons();
        UpdateUI();
	}
	
	void Update ()
    {
        //Weapons related inputs.
        //Firing the gun.
		if (Input.GetButtonDown("Fire1")) currentGun.GetComponent<GunController>().Shoot();
        //Aiming the gun.
        if (Input.GetButtonDown("Fire2"))
        {
            gunPosition.DOLocalMove(adsPosition,aimSpeed);
            cam.DOFieldOfView(fovAim, aimSpeed);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            gunPosition.DOLocalMove(hipPosition, aimSpeed);
            cam.DOFieldOfView(fovNormal, aimSpeed);
        }
        //Reloading
        if (Input.GetButtonDown("Reload")) currentGun.GetComponent<GunController>().Reload();
    }

    void InitializeWeapons() //Adapt after allowing multiple weapons.
    {
        currentGun.GetComponent<GunController>().magSize = guns[0].ammoCount;
        currentGun.GetComponent<GunController>().totalAmmo = guns[0].ammoCount * guns[0].totalClips;
    }

    void SwapWeapon(int w)
    {

    }

    public void UpdateUI() //Adapt after allowing multiple weapons.
    {
        //gunName.text = guns[0].gunName;
        currentAmmo.text = currentGun.GetComponent<GunController>().currentAmmo.ToString("000");
        totalAmmo.text = currentGun.GetComponent<GunController>().totalAmmo.ToString("/ 000");
    }
}
