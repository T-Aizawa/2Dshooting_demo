using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//移動スピード
	// public float speed = 5;

	//PlayerBulletプレハブ
	// public GameObject bullet;

	//Spaceshipコンポーネント
	Spaceship spaceship;


	// Startメソッドをコルーチンとして呼び出す
	IEnumerator Start ()
	{
		//Spaceshipコンポーネントを取得
		spaceship = GetComponent<Spaceship> ();

		while (true) {
			//弾をプレイヤーと同じ位置/角度で作成
			// Instantiate (bullet, transform.position, transform.rotation);
			spaceship.Shot (transform);

			//ショット音を鳴らす
      GetComponent<AudioSource>().Play();

			//0.05秒待つ
			// yield return new WaitForSeconds (0.05f);
			//shotDelay秒待つ
			yield return new WaitForSeconds (spaceship.shotDelay);
		}
	}

	void Update ()
	{
		//水平方向
		float x = Input.GetAxisRaw ("Horizontal");

		//垂直方向
		float y = Input.GetAxisRaw ("Vertical");

		//移動する向きを変える
		Vector2 direction = new Vector2 (x, y).normalized;

		//移動する向きとスピードを代入する
		// GetComponent<Rigidbody2D>().velocity = direction * speed;

		//移動
		// spaceship.Move (direction);

		//移動の制御
		// Clamp ();
		Move (direction);
	}

	void Move (Vector2 direction)
	{
		//画面左下のワールド座標をビューポートから取得
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		//画面右上のワールド座標をビューポートから取得
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

		//Playerの座標を取得
		Vector2 pos = transform.position;

		//移動量を加える
		pos += direction * spaceship.speed * Time.deltaTime;

		//Playerの位置が画面内に収まるように制限をかける
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);

		//制限をかけた値をPlayerの位置とする
		transform.position = pos;
	}

	//ぶつかった瞬間に動く処理
	void OnTriggerEnter2D (Collider2D c)
	{
		//レイヤー名を取得
		string LayerName = LayerMask.LayerToName(c.gameObject.layer);

		//レイヤー名がBullet (Enemy)の場合
		if (LayerName == "Bullet (Enemy)")
		{
			//弾を削除
			Destroy(c.gameObject);
		}

		//レイヤー名がBullet (Enemy)またはEnemyの場合
		if (LayerName == "Bullet (Enemy)" || LayerName == "Enemy")
		{
			//爆発する
			spaceship.Explosion();

			//プレイヤーを削除
			Destroy(gameObject);
		}
	}
}
