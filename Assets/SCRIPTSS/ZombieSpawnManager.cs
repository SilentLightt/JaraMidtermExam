using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    public GameObject[] zombieVariants;
    public GameObject[] spawnPoints;
    public float spawnInterval;
    public float minSpawnInterval = 0.5f;
    public int maxSpawnedZombiesPerWave;
    public int zombiesPerWaveIncrease = 5;

    public int currentWave = 1;
    public int zombiesSpawnedThisWave = 0;
    public int currentSpawnedZombies = 0;

    private bool waveInProgress = false;

    public GameObject player; // Reference to the player
    public float minSpawnDistance = 10.0f; // Minimum distance from the player
    public float maxSpawnDistance = 50.0f; // Maximum distance from the player


    // UI Elements
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI zombiesDestroyedText;

    // Counter for destroyed zombies
    private int zombiesDestroyedCount = 0;

    private void Start()
    {
        // Initialize the wave and destroyed zombie text on start
        waveText.text = "Wave: " + currentWave;
        zombiesDestroyedText.text = "Points: " + zombiesDestroyedCount;

        StartCoroutine(SpawnZombieRoutine());
    }

    IEnumerator SpawnZombieRoutine()
    {
        while (true)
        {
            if (!waveInProgress && currentSpawnedZombies == 0)
            {
                StartNewWave();
            }

            if (waveInProgress && zombiesSpawnedThisWave < maxSpawnedZombiesPerWave)
            {
                yield return new WaitForSeconds(spawnInterval);
                SpawnZombies();
            }

            yield return null;
        }
    }

    void StartNewWave()
    {
        Debug.Log("Starting Wave: " + currentWave);
        waveInProgress = true;
        zombiesSpawnedThisWave = 0;

        // Update the TextMeshPro UI with the current wave number
        waveText.text = "Wave: " + currentWave;

        maxSpawnedZombiesPerWave += zombiesPerWaveIncrease;
        spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - 0.2f);
    }

    void SpawnZombies()
    {
        if (currentSpawnedZombies < maxSpawnedZombiesPerWave)
        {
            // Get valid spawn points based on the distance to the player
            List<GameObject> validSpawnPoints = new List<GameObject>();

            foreach (GameObject spawnPoint in spawnPoints)
            {
                float distanceToPlayer = Vector3.Distance(spawnPoint.transform.position, player.transform.position);
                if (distanceToPlayer >= minSpawnDistance && distanceToPlayer <= maxSpawnDistance)
                {
                    validSpawnPoints.Add(spawnPoint);
                }
            }

            // Only proceed if there are valid spawn points
            if (validSpawnPoints.Count > 0)
            {
                // Pick a random spawn point from the valid ones
                int randomSpawnPoint = Random.Range(0, validSpawnPoints.Count);
                Vector3 spawnPointPosition = validSpawnPoints[randomSpawnPoint].transform.position;

                // Pick a random zombie variant to spawn
                int randomZombieIndex = Random.Range(0, zombieVariants.Length);

                // Instantiate the zombie at the chosen spawn point
                GameObject newObject = Instantiate(zombieVariants[randomZombieIndex], spawnPointPosition, Quaternion.identity);
                currentSpawnedZombies++;
                zombiesSpawnedThisWave++;
            }
            else
            {
                Debug.Log("No Spawn");
            }
        }

        if (zombiesSpawnedThisWave >= maxSpawnedZombiesPerWave)
        {
            waveInProgress = false;
        }
    }

    public void ZombieDestroyed()
    {
        currentSpawnedZombies--;

        // Increase destroyed zombie count and update UI
        zombiesDestroyedCount+=5;
        zombiesDestroyedText.text = "Zombo Points: " + zombiesDestroyedCount;

        if (currentSpawnedZombies <= 0 && !waveInProgress && zombiesSpawnedThisWave >= maxSpawnedZombiesPerWave)
        {
            currentWave++;
            StartNewWave();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minSpawnDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxSpawnDistance);
    }
}
//public class ZombieSpawnManager : MonoBehaviour
//{
//    public GameObject zombieToSpawn;
//    public GameObject[] spawnPoint;
//    public float spawnTime;
//    public int spawnCount;

//    public int spawnCountMax;

//    void Start()
//    {
//        StartCoroutine(SpawnZombieRoutine());
//    }

//    IEnumerator SpawnZombieRoutine()
//    {
//        while (true)
//        {
//            yield return new WaitForSeconds(spawnTime);
//            GameObject[] spawnZombies = GameObject.FindGameObjectsWithTag("Enemy");
//            if (spawnZombies.Length < spawnCountMax)
//            {
//                //spawn more Zombies
//                SpawnZombies();
//            }
//        }
//    }
//    void SpawnZombies()
//    {
//        int randomInt = Random.Range(0, spawnPoint.Length);
//        Vector3 spawnPointPosition = spawnPoint[randomInt].transform.position;

//        GameObject newObject = Instantiate(zombieToSpawn, spawnPointPosition, Quaternion.identity);

//    }
//}
