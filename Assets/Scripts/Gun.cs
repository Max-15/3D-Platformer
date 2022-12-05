using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun")]
public class Gun : ScriptableObject
{
    public enum SpecialAbility { None, Shotgun, RPG, RocketLauncher }
    public new string name;
    public float fireRate;
    public int ammo;
    public float reloadTime;
    public int damage;
    public float bulletWeight;
    public float accuracy;
    public SpecialAbility ability;
}
