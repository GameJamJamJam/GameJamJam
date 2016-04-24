using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {

	public int Life = 3;
	public float Spd = 1.0f;
	private int mItemNum = 1;
	public item.eExpType ExpType = item.eExpType.Cam;
	public GameObject[] DrawObjes = new GameObject[(int)item.eExpType.Max];

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
	private spawnMng mSpawnMng;

	//PLの攻撃によるダメージ
	public void addDamage(int dmgVal){
		mLife -= dmgVal;

		GameObject obj = Instantiate (Resources.Load ("damageEffect"), transform.position +new Vector3(0.0f,1.0f,0.0f), Quaternion.identity) as GameObject;
		obj.GetComponent<damageEffect> ().damage = dmgVal;

		GameObject sceneMng = GameObject.Find ("SceneMng");
		sceneMng.GetComponent<sceneMng> ().addScore (dmgVal);

		if (mLife <= 0) {
			mItemNum = 1;
			status plStatus = GameObject.Find ("PlayerStatus").GetComponent<status> ();
			plStatus.setLastKill (ExpType);
			if (plStatus.getLastKillExp () == ExpType) {
				mItemNum += plStatus.getKillStreak ();
			}

			mItemNum = Mathf.Clamp (mItemNum,1, 10);

			Debug.Log ("ItemNum="+mItemNum);
			for (int i = 0; i < mItemNum; i++) {
				GameObject item = Instantiate (Resources.Load ("ItemExp"), transform.position +new Vector3(0.1f * Random.Range(-1.0f, 1.0f) ,0.01f * i,0.0f), Quaternion.identity) as GameObject;
				item.transform.parent = GameObject.Find ("Items").gameObject.transform;
				item.GetComponent<item> ().ExpType = ExpType;
			}

			Destroy (this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		mLife = Life;
		switch (EnemyType) {
		case eEnemyType.Normal:
			break;
		case eEnemyType.NormalRange:
			break;
		case eEnemyType.Fly:
			GetComponent<Rigidbody> ().useGravity = false;
			break;
		case eEnemyType.FlyRange:
			GetComponent<Rigidbody> ().useGravity = false;
			break;
		}

		updateColor ();

		mSpawnMng = GameObject.Find ("Spawners").GetComponent<spawnMng> ();
	}

	void updateColor()
	{
		foreach (GameObject obj in DrawObjes) {
			obj.SetActive (false);
		}

		DrawObjes [(int)ExpType].SetActive (true);

	}

	void updateSwitchType()
	{
		switch (EnemyType) {
		case eEnemyType.Normal:
			updateMoveNormal ();
			break;
		case eEnemyType.NormalRange:
			break;
		case eEnemyType.Fly:
			updateMoveFly ();
			break;
		case eEnemyType.FlyRange:
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		mMoveTimer -= Time.deltaTime;
		if (mMoveTimer < 0.0f) {
			mMoveTimer += cMoveTime;
			updateSwitchType ();
		}

#if UNITY_EDITOR
		for( int i = (int)item.eExpType.Cam; i < (int)item.eExpType.Max; i++)
		{
			if( Input.GetKeyDown(i.ToString()) )
			{
				ExpType = (item.eExpType)i;
				updateColor();
			}
		}
#endif

	}

	void updateMoveNormal()
	{
		Vector3 dir = Vector3.right;
		int rand = Random.Range (0, 2);
		if (rand == 0) {
			dir = Vector3.left;
		}

		GetComponent<Rigidbody> ().AddForce (dir * Spd, ForceMode.Impulse);

	}

	void updateMoveFly()
	{
		updateMoveNormal ();
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

//			other.gameObject.GetComponent<
			//todo test death

			other.gameObject.GetComponent<PlayerLifeManager> ().ApplayDamage (1.0f);

			Destroy(this.gameObject);
		}

		if (other.gameObject.tag == "Untagged") {
			updateSwitchType ();
		}
	}


	void OnDestroy()
	{
		mSpawnMng.subEnemyNm ();

	
	}


}
