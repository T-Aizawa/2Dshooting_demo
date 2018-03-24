using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//移動スピード
	public float speed = 5;

	// Update is called once per frame
	void Update ()
	{
		//水平方向
		float x = Input.GetAxisRaw ("Horizontal");

		//垂直方向
		float y = Input.GetAxisRaw ("Vertical");

		//移動する向きを変える
		Vector2 direction = new Vector2 (x, y).normalized;

		//移動する向きとスピードを代入する
		GetComponent<Rigidbody2D>().velocity = direction * speed;

	}
}
