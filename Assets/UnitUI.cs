using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUI : MonoBehaviour
{
    [SerializeField] GameObject unit;
    [SerializeField] int price = 50;
    [SerializeField] PlayerInventorySO inventory;

    private Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").transform;
    }

    public void BuyUnit() {
        if(inventory.money >= price) {
            Instantiate(unit, spawnPoint.position, Quaternion.identity);
            inventory.money -= price;
        }
    }
}
