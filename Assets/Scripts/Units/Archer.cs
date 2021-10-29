using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Unit {
    private Collider2D hitEnemy;
    private bool attackEnded = true;
    private bool stopMoving = false;
    private Transform attackTarget;

    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform fireSpot;
    [SerializeField] private GameObject attackSFX;

    public override void AttackMoment() {
        AttackUnit(attackTarget.gameObject);

        GameObject sfx = Instantiate(attackSFX, transform.position, Quaternion.identity);
        Destroy(sfx, 2f);
    }

    public override void AttackUnit(GameObject unit) {
        GameObject arrowInstance = Instantiate(arrow, fireSpot.position, transform.rotation);
        ArcherArrow archerArrow = arrowInstance.GetComponent<ArcherArrow>();
        archerArrow.target = unit.transform;
        archerArrow.enemyArrow = enemy;
        archerArrow.damage = damage;
        attackEnded = true;
    }

    public override void CheckForEnemy() {
        hitEnemy = Physics2D.OverlapCircle(transform.position, attackRange, enemyLayer);

        if (hitEnemy != null) {
            if (attackTimeCounter <= 0) {
                attackTarget = hitEnemy.transform;
                attackEnded = false;
                Attack();
                LookAtTarget(hitEnemy.transform);
                attackTimeCounter = timeBetweenAttacks;
            } else {
                if (Vector3.Distance(transform.position, hitEnemy.transform.position) > attackRange * 3 / 4 && !stopMoving) {
                    Move();
                } else {
                    stopMoving = true;
                    Stay();
                }
                    
            }
        } else {
            if (Vector3.Distance(transform.position, followSpot.position) > maxDistanceFromFlag && attackEnded) {
                Move();
                stopMoving = false;
            }    
            else
                rbody.velocity = Vector2.zero;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
