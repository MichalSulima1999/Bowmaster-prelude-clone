using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    private Unit unit;

    private void Start() {
        unit = GetComponentInParent<Unit>();
    }

    public void TakeDamage(int amount) {
        unit.TakeDamage(amount);
    }
}
