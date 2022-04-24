using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public int HP;
	public int score;
	public int playerDamage;

	public int[] enemyHP = new int[4];
	public int[] enemyScore = new int[4];
	public float[] enemySpeed = new float[4];

	public GameObject[] HPObject = new GameObject[3];
	public Text scoreText;
	public GameObject retryScreen;

	void Start()
	{
		Time.timeScale = 1;
		HP = 3;
		score = 0;
	}

	void Update()
	{
		scoreText.text = score.ToString();

		if (HP <= 0)
		{
			OnRetryScreen();
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void DownHP()
	{
		HP--;
		HPObject[HP].SetActive(false);
	}

	public void OnRetryScreen()
	{
		Time.timeScale = 0;
		retryScreen.transform.GetChild(1).GetComponent<Text>().text = "Score : " + score;
		retryScreen.SetActive(true);
	}

	public void RetryGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}