using UnityEngine;
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
				PlayerController mPlCtrl = GameObject.Find ("Player").GetComponent<PlayerController> ();
				int sumLv = mPlCtrl.SumLevel;
				GameObject obj = Instantiate (Resources.Load ("Enemy"), transform.position, Quaternion.identity) as GameObject;
				obj.transform.parent = GameObject.Find ("Enemies").gameObject.transform;
				obj.GetComponent<enemy> ().ExpType = mExpType;
				obj.GetComponent<enemy> ().EnemyType = SpawnEnemyType;
				obj.GetComponent<enemy> ().Life = sumLv * sumLv;
				obj.GetComponent<enemy> ().Spd += numCreated * 0.05f;

				if (mSpawnMng.isFastOrder ()) {
					changeScale (obj,0.3f);
					obj.GetComponent<enemy> ().Life /= 2;
					obj.GetComponent<enemy> ().Spd += 5.0f;
					Debug.Log ("fast pop");

				}

				if (mSpawnMng.isBossOrder ()) {
					changeScale (obj,2.0f);
					obj.GetComponent<enemy> ().Life *= 20;
					if (!mSpawnMng.isFastOrder ()) {
						obj.GetComponent<enemy> ().Spd = 1.0f;
					}
					obj.GetComponent<enemy> ().IsBoss = true;
					Debug.Log ("boss pop");

				}


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
		int rand = Random.Range (0,100);

		if (rand < 40) {
			mExpType = item.eExpType.MeleePow;
		} else if (rand < 80) {
			mExpType = item.eExpType.ShotPow;

		} else {
			mExpType = item.eExpType.Jump;

		}

		//mExpType = (item.eExpType)Random.Range ((int)item.eExpType.Jump,(int)item.eExpType.Cam);

	//	if(
		//mExpType = (item.eExpType)Random.Range ((int)item.eExpType.Jump,(int)item.eExpType.Cam0);
	}

	void changeScale(GameObject obj, float sizeRate)
	{
		Vector3 scale =		obj.transform.localScale;
		scale *= sizeRate;
		obj.transform.localScale = scale;
	}
}