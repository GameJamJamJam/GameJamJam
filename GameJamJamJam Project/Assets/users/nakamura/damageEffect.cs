using UnityEngine;
using System.Collections;

public class damageEffect : MonoBehaviour {

	public int damage;
	public int ttl = 20;
	public Vector3 dir;
	public float speed = 0.3f;
	public float acc = 0.025f;

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<TextMesh> ().text = damage.ToString();

		float dirX = Random.value * 0.4f - 0.2f;
		dir = new Vector3 (dirX, 0.5f, 0.0f);	
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position += dir * speed;
		dir.y -= acc;

		ttl -= 1;
		if (ttl <= 0) {
			Destroy (this.gameObject);
		}
	}
}
