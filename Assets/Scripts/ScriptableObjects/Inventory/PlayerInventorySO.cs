using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New inventory", menuName = "Inventory")]
public class PlayerInventorySO : ScriptableObject
{
    public string userName;
    public int level;
    public int currentXP;
    public int xpToNextLevel;
    public int mission;
    public int money;
    public List<GameObject> boughtItems;

    public void setItemInInventory(GameObject item, Transform holder) {
        GameObject boughtItem = Instantiate(item, holder.position, Quaternion.identity, holder);
    }
    public void setItemInMenu(GameObject item, Transform holder) {
        GameObject boughtItem = Instantiate(item, holder.position, Quaternion.identity, holder);
        boughtItem.GetComponent<Button>().interactable = false;
    }

    public bool isBought(GameObject item) {
        foreach(GameObject boughtItem in boughtItems) {
            if (boughtItem == item)
                return true;
        }

        return false;
    }

    public void CreateNewUser(string userName) {
        this.userName = userName;
        level = 1;
        currentXP = 0;
        xpToNextLevel = 1000;
        mission = 1;
        money = 0;
        boughtItems.Clear();
    }
}
