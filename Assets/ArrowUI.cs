using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowUI : MonoBehaviour
{
    [SerializeField] private Arrow arrow;
    [SerializeField] private Image reloadImage;

    [SerializeField] private Sprite normalBackground;
    [SerializeField] private Sprite activeBackground;

    private BowManager bowManager;
    private static ArrowUI activeArrowUI;


    private void Start() {
        arrow.arrowSO.reloadCounter = 0;

        bowManager = GameObject.FindGameObjectWithTag("Bow").GetComponent<BowManager>();
    }

    private void Update() {
        arrow.arrowSO.reloadCounter -= Time.deltaTime;

        reloadImage.fillAmount = Mathf.Max(arrow.arrowSO.reloadCounter / arrow.arrowSO.reloadTime, 0);

        if(activeArrowUI != this)
            transform.parent.GetComponent<Image>().sprite = normalBackground;
    }

    public void UseThisArrow() {
        transform.parent.GetComponent<Image>().sprite = activeBackground;

        activeArrowUI = this;
        bowManager.ChangeArrow(arrow);
    }
}
