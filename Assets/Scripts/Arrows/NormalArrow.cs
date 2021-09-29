using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalArrow : Arrow
{
    private void Update() {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 6)
            HitGround();

        if (collision.gameObject.CompareTag("EnemyBase")) {
            collision.gameObject.GetComponent<EnemyBase>().takeDamage(damageToBuildings);
        } else if (collision.gameObject.CompareTag("EnemyUnit") || collision.gameObject.CompareTag("FriendlyUnit")) {
            collision.gameObject.GetComponent<Unit>().TakeDamage(damageToUnits);
            Destroy(gameObject);
        }
    }
}
