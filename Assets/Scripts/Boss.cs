using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	public int HP;
	float countTime, patternTime;

	public GameObject bullet;
	GameManager gameManager;
	public Sprite hitImage;

	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		HP = gameManager.enemyHP[3];
		patternTime = Random.Range(2f, 3f);
	}

	void Update()
	{
		countTime += Time.deltaTime;

		MoveBoss();
		ShootPatterns();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerBullet"))
		{
			Destroy(collision.gameObject);

			HP -= gameManager.playerDamage;
			if (HP <= 0)
			{
				StartCoroutine(OnHitImage());
				gameManager.score += gameManager.enemyScore[3];
				Destroy(gameObject);
			}
		}
	}

	private void OnDestroy()
	{
		gameManager.OnRetryScreen();
	}

	void MoveBoss()
	{
		if (transform.position.y > 3)
		{
			transform.Translate(Vector3.down * gameManager.enemySpeed[3] * Time.deltaTime);
		}
	}

	void ShootPatterns()
	{
		if (countTime >= patternTime)
		{
			countTime -= patternTime;
			patternTime = Random.Range(3f, 5f);

			switch (Random.Range(1, 2))
			{
				case 1:
					for (int count = 0; count < 360; count += 10)
					{
						Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, count));
					}
					break;
				case 2:
					int emptySpace = Random.Range(-25, 20 + 1);

					for (int count = -30; count <= 30; count += 10)
					{
						if (count >= emptySpace && count <= emptySpace + 5)
						{
							continue;
						}
						Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, count));
					}
					break;
			}
		}
	}

	IEnumerator OnHitImage()
	{
		Sprite spriteRenderer = GetComponent<SpriteRenderer>().sprite;

		GetComponent<SpriteRenderer>().sprite = hitImage;
		yield return new WaitForSeconds(0.1f);
		GetComponent<SpriteRenderer>().sprite = spriteRenderer;
	}
}
