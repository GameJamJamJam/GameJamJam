using UnityEngine;
using System.Collections;

public class item : MonoBehaviour {

	public enum eExpType
	{
		Jump = 0,
		MeleePow,
		ShotPow,

		Cam,
		Spd,
		MeleeHit,
		ShotBullet,
		Max,
	}

	public eExpType ExpType = eExpType.Cam;
	public GameObject[] DrawObjes = new GameObject[(int)item.eExpType.Max];

	public bool IsHeal = false;
	private GameObject mPlayerObj;

	// Use this for initialization
	void Start () {
		foreach (GameObject obj in DrawObjes) {
			obj.SetActive (false);
		}

		DrawObjes [(int)ExpType].SetActive (true);

		mPlayerObj = GameObject.Find ("Player");
	}



	// Update is called once per frame
	void Update () {
		float spd = 10.0f * Time.deltaTime;

		Vector3 vel = mPlayerObj.transform.position - transform.position;
		vel *= spd;

		transform.position += vel;


	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {

			GameObject.Find ("PlayerStatus").GetComponent<status> ().AddExp (ExpType);
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player") {

			GameObject.Find ("PlayerStatus").GetComponent<status> ().AddExp (ExpType);
			Destroy (this.gameObject);
		}
	}


	void OnDestroy()
	{
		//TODO effect
		//	GameObject obj = Instantiate (Resources.Load ("ItemExp"), transform.position, Quaternion.identity) as GameObject;
	}

}
