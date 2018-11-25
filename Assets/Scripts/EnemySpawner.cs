using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public static readonly float TIME_BETWEEN_UPDATES = 10f;

    public int concurrentEnemiesMax = 15;

    private Transform[] spawnPoints;
    private GameObject enemy;
    private float timeRemaining;

	// Use this for initialization
	void Start () {
        spawnPoints = transform.Cast<Transform>().ToArray();
        enemy = (GameObject) Resources.Load("Enemy1");

        for (int i = 0; i < spawnPoints.Length; i++)
            InstantiateOne(i);
    }

    private void InstantiateOne(int spawnPoint)
    {
        Instantiate(enemy, spawnPoints[spawnPoint].position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update () {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0) {
            int remainingEnemies = GameObject.FindObjectsOfType<EnemyController>().Length;
            for (int i = 0; i < concurrentEnemiesMax - remainingEnemies; i++)
                InstantiateOne(Random.Range(0, spawnPoints.Length));

            timeRemaining = TIME_BETWEEN_UPDATES;
        }

	}
}
