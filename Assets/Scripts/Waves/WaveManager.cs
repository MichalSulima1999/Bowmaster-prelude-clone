using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Wave[] wave;
    [SerializeField] private Transform enemySpawnPoint;

    [SerializeField] private float timeBetweenSpawn = 1f;
    [SerializeField] private float timeBetweenWaves = 15f;

    [SerializeField] private Image waveTimerImage;

    private int waveIndex = -1;
    private float countdown;

    // Update is called once per frame
    void Update()
    {
        waveTimerImage.fillAmount = countdown / timeBetweenWaves;

        if (waveIndex >= wave.Length - 1) {
            countdown = 0;
            GameManager.WavesEnded = true;
            return;
        }


        if (countdown <= 0f || Input.GetKeyDown(KeyCode.N)) {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
    }

    IEnumerator SpawnWave() {
        waveIndex++;

        for (int i = 0; i < wave[waveIndex].enemies.Length; i++) {
            SpawnEnemy(i);
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    void SpawnEnemy(int i) {
        Instantiate(wave[waveIndex].enemies[i], enemySpawnPoint.position, Quaternion.identity);
    }
}
