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
		None,
	}

	public eExpType ExpType = eExpType.Cam;
	public GameObject[] DrawObjes = new GameObject[(int)item.eExpType.Max];

	public bool IsHeal = false;
	private GameObject mPlayerObj;

	float mSpeed = 1.0f;
	// Use this for initialization
	void Start () {
		foreach (GameObject obj in DrawObjes) {
			obj.SetActive (false);
		}

		DrawObjes [(int)ExpType].SetActive (true);

		mPlayerObj = GameObject.Find ("Player");

		mSpeed = 6.0f * Random.Range (0.5f, 1.0f);
	}



	// Update is called once per frame
	void Update () {
		float spd =mSpeed  * Time.deltaTime;

		Vector3 vel = mPlayerObj.transform.position - transform.position;
		if (vel.magnitude > 0) {
			vel = vel.normalized;
		}
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
	/*	if (other.gameObject.tag == "Player") {

			GameObject.Find ("PlayerStatus").GetComponent<status> ().AddExp (ExpType);
			Destroy (this.gameObject);
		}
		*/
	}


	void OnDestroy()
	{
		//TODO effect
		//	GameObject obj = Instantiate (Resources.Load ("ItemExp"), transform.position, Quaternion.identity) as GameObject;
	}

}
