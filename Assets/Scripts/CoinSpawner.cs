using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    private Transform[] spawnPlaces;

    private void Awake()
    {
        spawnPlaces = new Transform[transform.childCount];

        for (int i = 0; i < spawnPlaces.Length; i++)
            spawnPlaces[i] = transform.GetChild(i);

        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        var waitTime = new WaitForSeconds(0.5f);

        while (true)
        {
            int random = Random.Range(0, spawnPlaces.Length);

            if (spawnPlaces[random].gameObject.activeSelf == false)
                spawnPlaces[random].gameObject.SetActive(true);

            yield return waitTime;
        }
    }
}
