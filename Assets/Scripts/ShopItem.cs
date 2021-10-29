using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] ShopItemSO shopItem;
    [SerializeField] PlayerInventorySO playerInventory;
    [SerializeField] PlayerItems playerItems;

    [SerializeField] Text descriptionText;

    private Button button;

    private void Start() {
        button = GetComponent<Button>();

        if (playerInventory.isBought(shopItem.itemToBuy)) {
            button.interactable = false;
        } else {
            button.interactable = true;
        }
     }

    public void BuyItem() {
        if (!playerInventory.isBought(shopItem.itemToBuy) && playerInventory.money >= shopItem.price) {
            button.interactable = false;
            Transform parent = playerItems.GetFreeItemHolder();
            playerInventory.setItemInInventory(shopItem.itemToBuy, parent);
            playerInventory.boughtItems.Add(shopItem.itemToBuy);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        descriptionText.text = "$" + shopItem.price;
        descriptionText.text += "\n" + shopItem.description;
    }
}
