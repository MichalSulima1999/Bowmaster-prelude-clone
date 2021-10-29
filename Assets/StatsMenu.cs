using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsMenu : MonoBehaviour
{
    [SerializeField] private Text playerName;
    [SerializeField] private Text level;
    [SerializeField] private Text xp;
    [SerializeField] private Text mission;
    [SerializeField] private Text gold;

    [SerializeField] private GameObject itemTemplate;
    [SerializeField] private Transform items;

    [SerializeField] private PlayerInventorySO inventory;

    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    public void UpdateText()
    {
        playerName.text = inventory.userName;
        level.text = inventory.level + "";
        xp.text = inventory.currentXP + " / " + inventory.xpToNextLevel;
        mission.text = inventory.mission + "";
        gold.text = inventory.money + "";

        foreach (Transform child in items.transform) {
            GameObject.Destroy(child.gameObject);
        }

        foreach (GameObject item in inventory.boughtItems) {
            GameObject setItem = Instantiate(itemTemplate, items.position, Quaternion.identity, items);
            setItem.GetComponent<Image>().sprite = item.GetComponent<Image>().sprite;
        }
    }
}
