using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [SerializeField] private int maxHP = 10000;
    [SerializeField] private Image hpImage;

    private int currentHP;

    private void Start() {
        currentHP = maxHP;
    }

    private void Update() {
        hpImage.fillAmount = (float)currentHP / (float)maxHP;
    }

    public void takeDamage(int damage) {
        currentHP -= damage;

        if(currentHP <= 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
    }
}
