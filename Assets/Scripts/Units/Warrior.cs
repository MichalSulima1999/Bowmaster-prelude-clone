using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Unit {
    [SerializeField] private GameObject attackSFX;

    private RaycastHit2D hitEnemy;

    public override void AttackMoment() {
        AttackUnit(hitEnemy.collider.gameObject);
        GameObject sfx = Instantiate(attackSFX, transform.position, Quaternion.identity);
        Destroy(sfx, 2f);
    }

    public override void AttackUnit(GameObject unit) {
        unit.GetComponent<Unit>().TakeDamage(damage);
    }

    public override void CheckForEnemy() {
        hitEnemy = Physics2D.Raycast(transform.position + new Vector3(0, 0.2f, 0), transform.right, attackRange, enemyLayer);

        if (hitEnemy.collider == null)
            hitEnemy = Physics2D.Raycast(transform.position + new Vector3(0, 0.2f, 0), -transform.right, attackRange, enemyLayer);

        if (hitEnemy.collider != null) {
            if (attackTimeCounter <= 0) {
                Attack();
                LookAtTarget(hitEnemy.transform);
                attackTimeCounter = timeBetweenAttacks;
            } else {
                Stay();
            }
        } else {
            if (Vector3.Distance(transform.position, followSpot.position) > maxDistanceFromFlag)
                Move();
            else
                rbody.velocity = Vector2.zero;
        }
    }
}
