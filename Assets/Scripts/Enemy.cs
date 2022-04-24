using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[Range(0, 2)] public int enemyNumber;
	public int HP;
	GameManager gameManager;
	public Sprite hitImage;

	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		HP = gameManager.enemyHP[enemyNumber];
	}

	void Update()
	{
		transform.Translate(Vector3.down * gameManager.enemySpeed[enemyNumber] * Time.deltaTime);
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
				gameManager.score += gameManager.enemyScore[enemyNumber];
				Destroy(gameObject);
			}
		}
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}

	IEnumerator OnHitImage()
	{
		Sprite spriteRenderer = GetComponent<SpriteRenderer>().sprite;

		GetComponent<SpriteRenderer>().sprite = hitImage;
		yield return new WaitForSeconds(0.1f);
		GetComponent<SpriteRenderer>().sprite = spriteRenderer;
	}
}