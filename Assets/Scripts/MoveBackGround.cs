using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround : MonoBehaviour
{
	public int speed;
	public float yPos;
	Vector2 startPos;
	float newPos;

	void Start()
	{
		startPos = transform.position;	
	}
	void Update()
    {
		newPos = Mathf.Repeat(Time.time * speed, yPos);
		transform.position = startPos + newPos * Vector2.down;
    }
}
