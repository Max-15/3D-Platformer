using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun")]
public class Gun : ScriptableObject
{
    public enum SpecialAbility { None, Boom }
    public new string name;
    public float fireRate;
    [Range(0, 100)] public int ammo;
    public float reloadTime;
    public int damage;
    public bool useGravity;
    [Range(0, 1)] public float accuracy;
    public SpecialAbility ability;
    public int bulletsPerShot = 1;
    public float bulletSpeedMultiplier = 1;

    [Header("Ability Stats")]
    public float boomRadius = 4f;
    public float boomKnockbackForce = 100f;
}
