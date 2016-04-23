using UnityEngine;
using System.Collections;

public class shell : MonoBehaviour {

	public Vector3 dir;
	public Vector3 initDir;
	public float speed = 0.3f;
	public int ttl = 60;

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
}
