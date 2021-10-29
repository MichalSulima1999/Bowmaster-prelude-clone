using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerInventorySO playerInventory;
    [SerializeField] PlayerItems playerItems;

    [SerializeField] private Text moneyText;

    private void Start() {
        SetItemsInInventory();
    }

    private void Update() {
        moneyText.text = "$" + playerInventory.money;
    }

    private void SetItemsInInventory() {
        foreach (GameObject item in playerInventory.boughtItems) {
            playerInventory.setItemInInventory(item, playerItems.GetFreeItemHolder());
        }
    }
}
