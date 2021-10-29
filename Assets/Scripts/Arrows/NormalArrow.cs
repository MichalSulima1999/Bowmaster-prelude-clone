using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalArrow : Arrow
{
    [SerializeField] private GameObject criticalEffect;
    [SerializeField] private PlayerInventorySO playerInventory;

    [SerializeField] private int moneyForHit;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 6)
            HitGround();

        if (collision.gameObject.CompareTag("EnemyUnit") || collision.gameObject.CompareTag("FriendlyUnit")) {
            collision.gameObject.GetComponent<Unit>().TakeDamage(arrowSO.damageToUnits);

            arrowSO.PlaySFX(transform);

            playerInventory.money += moneyForHit;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("EnemyBase")) {
            collision.gameObject.GetComponent<Castle>().takeDamage(arrowSO.damageToBuildings);

            arrowSO.PlaySFX(transform);

            playerInventory.money += moneyForHit;
            Destroy(gameObject);
        } else if (collision.CompareTag("Head")) {
            collision.gameObject.GetComponent<Head>().TakeDamage(arrowSO.damageToUnits * arrowSO.criticalMultiplier);
            playerInventory.money += moneyForHit * 2;
            GameObject effect = Instantiate(criticalEffect, transform.position, transform.rotation);

            arrowSO.PlaySFX(transform);

            Destroy(effect, 1f);
            Destroy(gameObject);
        }
    }
}
