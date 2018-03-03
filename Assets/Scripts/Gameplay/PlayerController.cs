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

    [Header("Movement Variables")]
    [SerializeField] float movementSpeed;
    [SerializeField] float lookSpeed;
    float movH, movV, mouseX, mouseY;
    [SerializeField] Transform head;
    float angleV = 0, angleH = 0;
    Rigidbody rb;

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
        Cursor.visible = false;

        cam.fieldOfView = fovNormal;

        gunPosition.localPosition = hipPosition;
        adsPosition = new Vector3(0, guns[0].aimHeight, hipPosition.z);

        currentGun.GetComponent<GunController>().casing = guns[0].gunCasing;

        InitializeWeapons();
        UpdateUI();

        rb = GetComponent<Rigidbody>();
	}

    void Update ()
    {
        //Movement related inputs.
        //Movement.
        movH = Input.GetAxis("Horizontal");
        movV = Input.GetAxis("Vertical");
        if (movH > 0.1f || -0.1f > movH || movV > 0.1f || -0.1f > movV) Move(movH, movV);
        //Look.
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        if (mouseY > 0.05f || -0.05f > mouseY || mouseX > 0.05f || -0.05f > mouseX) Look(mouseX, mouseY);
        //---//

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
        //---//

        gunPosition.LookAt(head.transform.forward * 100);
    }

    void InitializeWeapons() //Adapt after allowing multiple weapons.
    {
        currentGun.GetComponent<GunController>().magSize = guns[0].ammoCount;
        currentGun.GetComponent<GunController>().totalAmmo = guns[0].ammoCount * guns[0].totalClips;
    }

    void SwapWeapon(int w)
    {

    }

    void Move(float h, float v)
    {
        //rb.MovePosition(transform.position + (transform.right * h * movementSpeed * Time.deltaTime) + (transform.forward * v * movementSpeed * Time.deltaTime));
        transform.position += (transform.forward * v * movementSpeed * Time.deltaTime) + (transform.right * h * movementSpeed * Time.deltaTime);
    }

    void Look(float x, float y)
    {
        angleV += y * (lookSpeed) * Time.deltaTime;
        if (angleV < -60) angleV = -60;
        if (angleV > 80) angleV = 80;
        head.transform.localRotation = Quaternion.Euler(-angleV, 0, 0);

        angleH += x * (lookSpeed) * Time.deltaTime;
        if (angleH < 0) angleH %= 360;
        if (angleH > 360) angleH %= 360;
        transform.rotation = Quaternion.Euler(0, angleH, 0);
        //transform.Rotate(0, angleH/10, 0);
    }

    public void UpdateUI() //Adapt after allowing multiple weapons.
    {
        //gunName.text = guns[0].gunName;
        currentAmmo.text = currentGun.GetComponent<GunController>().currentAmmo.ToString("000");
        totalAmmo.text = currentGun.GetComponent<GunController>().totalAmmo.ToString("/ 000");
    }
}
