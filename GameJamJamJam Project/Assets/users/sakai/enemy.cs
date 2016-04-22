﻿using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {

	public int Life = 1;
	public float Spd = 1.0f;
	public int ItemNum = 10;


	public enum eEnemyType
	{
		Normal,
		Fly,
		NormalRange,
		FlyRange,
	};

	public eEnemyType EnemyType = eEnemyType.Normal;


	private int mLife;
	private const float cMoveTime = 0.5f;
	private float mMoveTimer;

	// Use this for initialization
	void Start () {
		mLife = Life;
	}
	
	// Update is called once per frame
	void Update () {
		mMoveTimer -= Time.deltaTime;
		if (mMoveTimer < 0.0f) {
			mMoveTimer += cMoveTime;

			Vector3 dir = Vector3.right;
			int rand = Random.Range (0, 2);
			if (rand == 0) {
				dir = Vector3.left;
			}

			GetComponent<Rigidbody> ().AddForce (dir * Spd, ForceMode.Impulse);
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Bullet") {
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Hit to Pl");

			//todo add damage

			//todo test death
			Destroy(this.gameObject);
		}
	}


	void OnDestroy()
	{
		for (int i = 0; i < ItemNum; i++) {
			GameObject obj = Instantiate (Resources.Load ("ItemExp"), transform.position +new Vector3(0.01f * Random.Range(-1.0f, 1.0f) ,0.01f * i,0.0f), Quaternion.identity) as GameObject;
		}
	}

}