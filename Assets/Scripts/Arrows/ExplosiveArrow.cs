using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveArrow : Arrow
{
    [SerializeField] private GameObject explosion;

    private void InstantiateExplosion() {
        GameObject explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);
        Explosion explosionScript = explosionInstance.GetComponent<Explosion>();
        explosionScript.damageToBuildings = arrowSO.damageToBuildings;
        explosionScript.damageToUnits = arrowSO.damageToUnits;
        Destroy(explosionInstance, 0.1f);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        InstantiateExplosion();
        arrowSO.PlaySFX(transform);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("EnemyBase")) {
            InstantiateExplosion();
            arrowSO.PlaySFX(transform);

            Destroy(gameObject);
        }
    }
}
