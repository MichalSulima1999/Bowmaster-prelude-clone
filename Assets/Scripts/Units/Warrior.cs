using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Unit {
    public override void AttackUnit(GameObject unit) {
            unit.GetComponent<Unit>().TakeDamage(damage);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (enemy) {
            if (collision.CompareTag("FriendlyFlag")) {
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180, transform.rotation.eulerAngles.z));
            }
        }
    }
}
