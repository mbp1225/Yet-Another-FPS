using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapons/Guns", order = 1)]
public class Gun : ScriptableObject
{
    public string gunName = "New Gun";
    public int ammoCount, totalClips;
    public int damage;

    public float aimHeight;
    public bool canToggleFiremode;

    public float recoil;
    public float recoilMultiplier;

    public bool spreadShot;
    public float spread;
    public int pellets;

    public enum FireMode {semi, auto, burst};

    public FireMode fireMode;

    public GameObject gunModel;
    public GameObject gunCasing;

}
