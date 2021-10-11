using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool WavesEnded = false;
    public static bool EnemyFlagDelivered = false;
    public static bool FriendlyFlagDelivered = false;

    public static bool Won = false;
    public static bool Lost = false;

    private GameObject[] enemies;

    [SerializeField] private GameObject lostCanvas;
    [SerializeField] private GameObject wonCanvas;

    // Start is called before the first frame update
    void Start()
    {
        WavesEnded = false;

        InvokeRepeating("GetEnemies", 0f, 0.5f);

        lostCanvas.SetActive(false);
        wonCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Won || Lost)
            return;

        if (WavesEnded || EnemyFlagDelivered) {
            Win();
        }

        if (FriendlyFlagDelivered) {
            Lose();
        }
    }

    void GetEnemies() {
        enemies = GameObject.FindGameObjectsWithTag("EnemyUnit");

        if (enemies == null && WavesEnded)
            Win();
    }

    void Win() {
        Won = true;
        lostCanvas.SetActive(true);
        Debug.Log("You won!");
        Time.timeScale = 0;
    } 

    void Lose() {
        Lost = true;
        wonCanvas.SetActive(true);
        Debug.Log("You lost!");
        Time.timeScale = 0;
    }
}
