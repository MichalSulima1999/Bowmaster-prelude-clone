using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowUI : MonoBehaviour
{
    [SerializeField] private Arrow arrow;
    [SerializeField] private Image reloadImage;

    [SerializeField] private BowManager bowManager;

    private void Start() {
        arrow.arrowSO.reloadCounter = 0;
    }

    private void Update() {
        arrow.arrowSO.reloadCounter -= Time.deltaTime;

        reloadImage.fillAmount = Mathf.Max(arrow.arrowSO.reloadCounter / arrow.arrowSO.reloadTime, 0);
    }

    public void UseThisArrow() {
        bowManager.ChangeArrow(arrow);
    }
}
