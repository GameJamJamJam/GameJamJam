using UnityEngine;
using System.Collections;

public class shellPlNear : MonoBehaviour {

	public Vector3 dir;
	public Vector3 initDir;
	public float speed = 0.5f;
	public float ttl = 0.5f;
	public int power = 5;

	// Use this for initialization
	void Start () {
		dir = initDir;
	}

	// Update is called once per frame
	void Update () {
		ttl -= Time.deltaTime;
		Debug.Log (ttl.ToString ());
		if (ttl <= 0.0f) {
			Destroy (this.gameObject);
		}

		transform.position += dir * speed * Time.deltaTime;
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
