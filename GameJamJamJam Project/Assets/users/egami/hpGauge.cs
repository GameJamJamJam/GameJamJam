using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class hpGauge : MonoBehaviour {

	public GameObject refHpString;
	public Text refText;
	public Vector3 offset;
	public status refPlayerStatus;

	// Use this for initialization
	void Start () {
		refHpString = GameObject.Find ("HpString");
		refText = GetComponent ("Text") as Text;
		offset.x = 28;
		offset.y = 1;
		GameObject tmp = GameObject.Find ("PlayerStatus");
		if (tmp) {
			refPlayerStatus = tmp.GetComponent<status>();
		}
	}

	// Update is called once per frame
	void Update () {
		if (refHpString != null) {
			Vector3 pos = refHpString.transform.position;
			pos += offset;
			this.transform.position = pos;

			refText.text = "";
			int lineNum = (int)refPlayerStatus.vital;
			if (0.0f < refPlayerStatus.vital && refPlayerStatus.vital < 1.0f) {
				lineNum = 1;
			}
			for (int i = 0; i < lineNum; i++) {
				refText.text += "☆";
			}
		}
	}
}
