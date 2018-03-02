using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject fpsCam, casing;
    [SerializeField] Animation shootAnimation;
    public GameObject gun;

    bool isShooting = false;
    [SerializeField] float shotDuration;

	void Start ()
    {
        Cursor.visible = false;
    }
	
	void Update ()
    {
		if (Input.GetButtonDown("Fire1") && !isShooting)
        {
            Shoot();
        }
	}

    void Shoot()
    {
        isShooting = true;
        gun.GetComponent<GunController>().Shoot();
        fpsCam.transform.DOPunchPosition(Vector3.forward * -0.05f, .15f, 7, 1, false);
        fpsCam.transform.DOPunchRotation(Vector3.right * -1, .15f, 7, 1);

        //Invoke("SetShooting", shotDuration);
    }

    public void SetShooting()
    {
        isShooting = false;
    }
}
