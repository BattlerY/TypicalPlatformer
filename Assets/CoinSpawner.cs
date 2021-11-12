using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    private List<Transform> spawnPlaces;

    private void Awake()
    {
        spawnPlaces = new List<Transform>();
        foreach (Transform item in transform)
            spawnPlaces.Add(item);

        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        var waitTime = new WaitForSeconds(0.5f);

        while (true)
        {
            int random = Random.Range(0, spawnPlaces.Count);

            if (spawnPlaces[random].gameObject.activeSelf == false)
                spawnPlaces[random].gameObject.SetActive(true);

            yield return waitTime;
        }
    }
}
