using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {

	public int Life = 3;
	public float Spd = 1.0f;
	public int ItemNum = 10;
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

	//PLの攻撃によるダメージ
	public void addDamage(int dmgVal){
		mLife -= dmgVal;
		if (mLife <= 0) {
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


		foreach (GameObject obj in DrawObjes) {
			obj.SetActive (false);
		}

		DrawObjes [(int)ExpType].SetActive (true);

	}
	
	// Update is called once per frame
	void Update () {
		mMoveTimer -= Time.deltaTime;
		if (mMoveTimer < 0.0f) {
			mMoveTimer += cMoveTime;
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
			//todo test death
			Destroy(this.gameObject);
		}
	}


	void OnDestroy()
	{
		for (int i = 0; i < ItemNum; i++) {
			GameObject obj = Instantiate (Resources.Load ("ItemExp"), transform.position +new Vector3(0.01f * Random.Range(-1.0f, 1.0f) ,0.01f * i,0.0f), Quaternion.identity) as GameObject;
			obj.transform.parent = GameObject.Find ("Items").gameObject.transform;
			obj.GetComponent<item> ().ExpType = ExpType;
		}
	}


}
