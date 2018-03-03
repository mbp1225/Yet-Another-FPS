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

    public bool isAuto;
    public bool canToggleFiremode;
    public bool spreadShot;

    public GameObject gunModel;
    public GameObject gunCasing;

}
