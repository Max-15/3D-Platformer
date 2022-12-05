using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : ScriptableObject
{
    public enum SpecialAbility { None, Shotgun, RPG, RocketLauncher }
    public Mesh model;
    public new string name;
    public float fireRate;
    public int ammo;
    public int damage;
    public float bulletWeight;
    public float accuracy;
    public SpecialAbility ability;
}
