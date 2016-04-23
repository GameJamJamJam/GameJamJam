using UnityEngine;
using System.Collections;

public class item : MonoBehaviour {

	public enum eExpType
	{
		Cam = 0,
		Spd,
		Jump,
		MeleePow,
		MeleeHit,
		ShotPow,
		ShotBullet,
		Max,
	}

	public eExpType ExpType = eExpType.Cam;

	public bool IsHeal = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Bullet") {
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Item Hit to Pl");

			//TODO add exp

			Destroy (this.gameObject);
		}
	}


	void OnDestroy()
	{
		//TODO effect
		//	GameObject obj = Instantiate (Resources.Load ("ItemExp"), transform.position, Quaternion.identity) as GameObject;
	}

}
