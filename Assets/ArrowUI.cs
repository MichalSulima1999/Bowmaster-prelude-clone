using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowUI : MonoBehaviour
{
    [SerializeField] private Arrow arrow;
    [SerializeField] private Image reloadImage;

    [SerializeField] private bool defaultItem;

    private Image image;

    private BowManager bowManager;
    private static ArrowUI activeArrowUI;


    private void Start() {
        arrow.arrowSO.reloadCounter = 0;

        image = GetComponent<Image>();

        
        bowManager = GameObject.FindGameObjectWithTag("Bow").GetComponent<BowManager>();

        if (defaultItem)
            activeArrowUI = this;
    }

    private void Update() {
        arrow.arrowSO.reloadCounter -= Time.deltaTime;

        reloadImage.fillAmount = Mathf.Max(arrow.arrowSO.reloadCounter / arrow.arrowSO.reloadTime, 0);

        if (activeArrowUI != this)
            image.color = Color.black;
    }

    public void UseThisArrow() {
        image.color = Color.white;

        activeArrowUI = this;
        bowManager.ChangeArrow(arrow);
    }
}
