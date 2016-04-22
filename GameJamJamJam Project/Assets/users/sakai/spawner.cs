using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {
	public float SpawnTime = 3.0f;


	private float mSpawnTimer;


	// Use this for initialization
	void Start () {
		mSpawnTimer = SpawnTime;
	}
	
	// Update is called once per frame
	void Update () {
		mSpawnTimer -= Time.deltaTime;
		if (mSpawnTimer < 0) {
			mSpawnTimer += SpawnTime;

			GameObject obj = Instantiate (Resources.Load ("Enemy"), transform.position, Quaternion.identity) as GameObject;
		}
	}
}
