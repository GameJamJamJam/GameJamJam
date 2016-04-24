using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mpGauge : MonoBehaviour {

	public GameObject refString;
	public Text refText;
	public Vector3 offset;
	public status refPlayerStatus;
	public GameObject refPlayer;

	// Use this for initialization
	void Start () {
		refString = GameObject.Find ("MpString");
		refText = GetComponent ("Text") as Text;
		offset.x = 36;
		offset.y = 1;
		GameObject tmp = GameObject.Find ("PlayerStatus");
		if (tmp) {
			refPlayerStatus = tmp.GetComponent<status>();
		}
		refPlayer = GameObject.Find ("Player");
	}

	// Update is called once per frame
	void Update () {
		if (refString != null) {
			Vector3 pos = refString.transform.position;
			pos += offset;
			this.transform.position = pos;

			refText.text = "";
			if (refPlayer != null) {
				float value = refPlayerStatus.magicPower;
				///float lifeVal = refPlayer.GetComponent<PlayerLifeManager> ().Life;
				int lineNum = (int)value;
			
				if (0.0f < value && value < 1.0f) {
					lineNum = 1;
				}
				for (int i = 0; i < lineNum; i++) {
					refText.text += "|";
				}
			}
		}
	}
}
