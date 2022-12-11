using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    GameObject player;
    Health health;
    Rigidbody rb;
    public float collisionDamageMultiplier = 0.6f;
    private void Start() {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        health = GetComponent<Health>();
    }
    private void Update() {
        transform.rotation = Quaternion.LookRotation(-(player.transform.position - transform.position));
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody orb)){
            int damage = Mathf.CeilToInt(collisionDamageMultiplier * (orb.mass * Vector2.Distance(orb.velocity, rb.velocity)));
            if(damage >= 3 && other.gameObject.tag != "Bullet"){
                health.TakeDamage(damage);
            }
        }
    }
}
