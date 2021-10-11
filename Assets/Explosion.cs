using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damageToUnits { get; set; }
    public int damageToBuildings { get; set; }

    [SerializeField] GameObject explosionEffect;

    private void Start() {
        GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("EnemyUnit") || collision.CompareTag("FriendlyUnit")) {
            collision.GetComponent<Unit>().TakeDamage(damageToUnits);
            Destroy(gameObject);
        }

        if (collision.CompareTag("EnemyBase")) {
            collision.gameObject.GetComponent<Castle>().takeDamage(damageToBuildings);
            Destroy(gameObject);
        }
    }
}
