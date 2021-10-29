using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New shop item", menuName = "Shop item")]
public class ShopItemSO : ScriptableObject
{
    public GameObject itemToBuy;
    public int price = 1000;
    [TextArea] public string description;
}
