using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun")]
public class Gun : ScriptableObject
{
    public enum SpecialAbility { None, MultiFire, Boom }
    public new string name;
    public float fireRate;
    [Range(0, 30)] public int ammo;
    public float reloadTime;
    public int damage;
    public float bulletWeight;
    public float accuracy;
    public SpecialAbility ability;

    [Header("Ability Stats")]
    public float boomRadius = 4f;
    public float boomKnockbackForce = 100f;
    public int bulletsPerShot = 1;
}
