﻿using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {
	public float SpawnTime = 3.0f;
	public enemy.eEnemyType SpawnEnemyType;
	public int numCreated = 0;


	private float mSpawnTimer;
	private const int SpawnChangeNum = 5;
	private int mSpawnChangeCount;

	private item.eExpType mExpType;

	private spawnMng mSpawnMng;
	// Use this for initialization
	void Start () {
		mSpawnTimer = SpawnTime;
		mSpawnChangeCount = SpawnChangeNum;
		randExpType ();
		mSpawnMng = GameObject.Find ("Spawners").GetComponent<spawnMng> ();
	}
	
	// Update is called once per frame
	void Update () {
		mSpawnTimer -= Time.deltaTime;



		if (mSpawnTimer < 0) {
			mSpawnTimer += SpawnTime;

			if (mSpawnMng.isCanSpawn ()) {
				mSpawnMng.addEnemyNm ();
				GameObject obj = Instantiate (Resources.Load ("Enemy"), transform.position, Quaternion.identity) as GameObject;
				obj.transform.parent = GameObject.Find ("Enemies").gameObject.transform;
				obj.GetComponent<enemy> ().ExpType = mExpType;
				obj.GetComponent<enemy> ().EnemyType = SpawnEnemyType;
				obj.GetComponent<enemy> ().Life = numCreated * 3;
				obj.GetComponent<enemy> ().Spd += numCreated * 0.2f; 
				numCreated++;

				mSpawnChangeCount--;
				if (mSpawnChangeCount <= 0) {
					mSpawnChangeCount = SpawnChangeNum;
					randExpType ();
				}
			}
		}
	}

	void randExpType()
	{
		mExpType = (item.eExpType)Random.Range ((int)item.eExpType.Jump,(int)item.eExpType.Cam);
	}
}
