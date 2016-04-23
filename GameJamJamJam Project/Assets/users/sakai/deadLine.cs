using UnityEngine;
using System.Collections;

public class deadLine : MonoBehaviour {

	public bool isPlayer = false;
	private float mDedLineY = -4.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < mDedLineY) {
			if (isPlayer) {
				GetComponent<PlayerLifeManager> ().Life = -1;
			} else {
				Destroy (this.gameObject);
			}
		}
	}
}
