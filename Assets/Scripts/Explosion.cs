using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [System.NonSerialized] public Gun stats;
    public LayerMask boomable;
    public float lifetime = 0.3f;
    float deathTimer;
    private void OnEnable()
    {
        GetComponent<SphereCollider>().radius = stats.boomRadius;
        deathTimer = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        print("explosion found " + other.gameObject.name);
        if ((boomable & (1 << other.gameObject.layer)) != 0)
        {
            if (other.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                // rb.AddExplosionForce(stats.boomKnockbackForce, transform.position, stats.boomRadius, 0, ForceMode.Impulse);
                if (other.gameObject.tag == "Player")
                {
                    rb.velocity = (rb.position - transform.position).normalized * (stats.boomKnockbackForce / 4);
                }
                else
                {
                    rb.velocity = (rb.position - transform.position).normalized * (stats.boomKnockbackForce / 6);
                }
                print("explosion knocked " + other.gameObject.name + " back");
            }
            // if(other.TryGetComponent<Enemy>(out Enemy enemy)){
            //     enemy.TakeDamage(stats.damage)
            // }
        }
    }
    private void Update()
    {
        deathTimer += Time.deltaTime;
        if (deathTimer > lifetime)
        {
            Destroy(gameObject);
        }
    }
}
