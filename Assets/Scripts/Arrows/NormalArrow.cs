using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalArrow : Arrow
{

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 6)
            HitGround();

        if (collision.gameObject.CompareTag("EnemyUnit") || collision.gameObject.CompareTag("FriendlyUnit")) {
            collision.gameObject.GetComponent<Unit>().TakeDamage(damageToUnits);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("EnemyBase")) {
            collision.gameObject.GetComponent<Castle>().takeDamage(damageToBuildings);
            Destroy(gameObject);
        } else if (collision.CompareTag("Head")) {
            collision.gameObject.GetComponent<Head>().TakeDamage(damageToUnits * criticalMultiplier);
            GameObject effect = Instantiate(criticalEffect, transform.position, transform.rotation);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
    }
}
