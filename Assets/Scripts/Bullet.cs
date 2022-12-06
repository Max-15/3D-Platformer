using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [System.NonSerialized] public Gun stats;
    public GameObject bulletCollideParticles;
    public LayerMask whatKillsBullet;

    [Header("Special")]
    public GameObject explosionGameObject;

    private void OnCollisionEnter(Collision other)
    {
        if ((whatKillsBullet & (1 << other.gameObject.layer)) != 0)
        {
            GameObject bulletParticles = Instantiate(bulletCollideParticles);
            bulletParticles.transform.position = transform.position;
            bulletParticles.GetComponent<ParticleSystem>().Play();

            if(stats.ability == Gun.SpecialAbility.Boom){
                explosionGameObject.GetComponent<Explosion>().stats = stats;
                explosionGameObject.transform.SetParent(null);
                explosionGameObject.SetActive(true);
            }

            Destroy(gameObject);
        }
    }
}
