using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [System.NonSerialized] public Gun stats;
    public GameObject bulletCollideParticles;
    public GameObject bulletHitEnemyParticles;
    public LayerMask whatKillsBullet;
    public LayerMask whatIsEnemy;

    [Header("Special")]
    public GameObject explosionGameObject;

    private void OnCollisionEnter(Collision other)
    {
        if ((whatKillsBullet & (1 << other.gameObject.layer)) != 0)
        {
            if (stats.ability == Gun.SpecialAbility.Boom)
            {
                explosionGameObject.GetComponent<Explosion>().stats = stats;
                explosionGameObject.transform.SetParent(null);
                explosionGameObject.SetActive(true);
            }
            if ((whatIsEnemy & (1 << other.gameObject.layer)) != 0)
            {
                GameObject bulletParticles = Instantiate(bulletHitEnemyParticles);
                bulletParticles.transform.position = transform.position;
                bulletParticles.GetComponent<ParticleSystem>().Play();
                if(stats.ability != Gun.SpecialAbility.Boom){
                    Health enemyHealth = other.gameObject.GetComponent<Health>();
                    enemyHealth.TakeDamage(stats.damage);
                }
            }
            else
            {
                GameObject bulletParticles = Instantiate(bulletCollideParticles);
                bulletParticles.transform.position = transform.position;
                bulletParticles.GetComponent<ParticleSystem>().Play();
            }
            Destroy(gameObject);
        }
    }
}
