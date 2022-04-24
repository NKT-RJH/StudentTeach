using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
	public GameObject[] enemyLIst = new GameObject[4];
	float countTime, countBossTime, spawnTime;
	public int bossSpawnTime;
    
    void Start()
    {
		spawnTime = Random.Range(3f, 5f);
    }
	
    void Update()
    {
		countTime += Time.deltaTime;
		countBossTime += Time.deltaTime;

		NormalSpawn();
		BossSpawn();
    }

	void NormalSpawn()
	{
		if (countTime >= spawnTime)
		{
			countTime -= spawnTime;
			spawnTime = Random.Range(3f, 5f);
			Instantiate(enemyLIst[Random.Range(0, 3)], new Vector3(Random.Range(-2f, 2f), 5), Quaternion.identity);
		}
	}

	void BossSpawn()
	{
		if (countBossTime >= bossSpawnTime)
		{
			Instantiate(enemyLIst[3], new Vector3(0, 5), Quaternion.identity);
			enabled = false;
		}
	}
}