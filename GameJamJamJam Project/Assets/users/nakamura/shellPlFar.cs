using UnityEngine;
using System.Collections;

public class shellPlFar : MonoBehaviour {

	public Vector3 dir;
	public Vector3 initDir;
	public float speed = 0.3f;
	public int ttl = 60;
	public int power = 1;

	// Use this for initialization
	void Start () {
		dir = initDir;
	}

	// Update is called once per frame
	void Update () {
		ttl -= 1;
		if (ttl < 0) {
			Destroy (this.gameObject);
		}

		transform.position += dir * speed;
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Enemy") {
			//Debug.Log ("Hit to Pl");

			//other.gameObject.GetComponent<
			//todo test death

			other.gameObject.GetComponent<enemy> ().addDamage (power);

			Destroy(this.gameObject);
		}
	}
}
