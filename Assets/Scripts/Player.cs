using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
	public int speed;
	float countTime;
	bool onInvincibility;
	public GameObject[] bulletList = new GameObject[2];
	public GameManager gameManager;
	public FixedJoystick joystick;

	void Update()
	{
		countTime += Time.deltaTime;

		MovePlayer();
		ShootBullet();
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (!onInvincibility)
		{
			if (collision.CompareTag("Enemy"))
			{
				StartCoroutine(Invincibility());
				gameManager.DownHP();
			}
		}
	}

	void MovePlayer()
	{
		Vector3 position = new Vector3(joystick.Horizontal * speed * Time.deltaTime, joystick.Vertical * speed * Time.deltaTime);

		transform.position += position;

		Vector3 temp = Camera.main.WorldToViewportPoint(transform.position);

		if (temp.x > 1) temp.x = 1;
		if (temp.y > 1) temp.y = 1;
		if (temp.x < 0) temp.x = 0;
		if (temp.y < 0) temp.y = 0;

		transform.position = Camera.main.ViewportToWorldPoint(temp);
	}

	void ShootBullet()
	{
		if (countTime >= 0.15f)
		{
			countTime -= 0.15f;
			Instantiate(bulletList[0], new Vector3(transform.position.x, transform.position.y + 0.3f), Quaternion.identity);
			Instantiate(bulletList[1], new Vector3(transform.position.x + 0.2f, transform.position.y + 0.3f), Quaternion.identity);
			Instantiate(bulletList[1], new Vector3(transform.position.x - 0.2f, transform.position.y + 0.3f), Quaternion.identity);
		}
	}

	IEnumerator Invincibility()
	{
		onInvincibility = true;
		
		bool onOff = false;

		for (int count = 0; count < 15; count++)
		{
			GetComponent<SpriteRenderer>().enabled = onOff;
			onOff = !onOff;
			yield return new WaitForSeconds(0.1f);
		}
		GetComponent<SpriteRenderer>().enabled = true;

		onInvincibility = false;
	}
}