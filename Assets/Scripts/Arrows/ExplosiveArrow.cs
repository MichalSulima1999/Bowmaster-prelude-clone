using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveArrow : Arrow
{
    [SerializeField] private GameObject explosion;

    private void InstantiateExplosion() {
        GameObject explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);
        Explosion explosionScript = explosionInstance.GetComponent<Explosion>();
        explosionScript.damageToBuildings = damageToBuildings;
        explosionScript.damageToUnits = damageToUnits;
        Destroy(explosionInstance, 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        InstantiateExplosion();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("EnemyBase")) {
            InstantiateExplosion();
            Destroy(gameObject);
        }
    }
}
