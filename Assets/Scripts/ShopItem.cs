using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] ShopItemSO shopItem;
    [SerializeField] PlayerItems playerItems;

    [SerializeField] Text descriptionText;

    private void Start() {
        shopItem.bought = false; // temp
    }

    public void BuyItem() {
        if (!shopItem.bought) {
            shopItem.bought = true;
            Transform parent = playerItems.GetFreeItemHolder();
            GameObject boughtItem = Instantiate(shopItem.itemToBuy, parent.position, Quaternion.identity, parent);
            //boughtItem.transform.position = Vector3.zero;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        descriptionText.text = "$" + shopItem.price;
        descriptionText.text += "\n" + shopItem.description;
    }
}
