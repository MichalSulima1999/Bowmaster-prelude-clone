using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Wave[] wave;
    [SerializeField] private Transform enemySpawnPoint;

    [SerializeField] private float timeBetweenSpawn = 1f;
    [SerializeField] private float timeBetweenWaves = 15f;

    private int waveIndex = -1;
    private float countdown;
    private bool wavesEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waveIndex >= wave.Length - 1) {
            countdown = 0;
            wavesEnded = true;
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
