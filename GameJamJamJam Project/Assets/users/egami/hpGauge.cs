using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class hpGauge : MonoBehaviour {

	public GameObject refHpString;
	public Text refText;
	public Vector3 offset;
	public status refPlayerStatus;
	public GameObject refPlayer;
	public PlayerLifeManager refLifeManager;

	// Use this for initialization
	void Start () {
		refHpString = GameObject.Find ("HpString");
		refText = GetComponent ("Text") as Text;
		offset.x = 35;
		offset.y = 1;
		GameObject tmp = GameObject.Find ("PlayerStatus");
		if (tmp) {
			refPlayerStatus = tmp.GetComponent<status>();
		}
		refPlayer = GameObject.Find ("Player");
		refLifeManager = GameObject.Find ("Player").GetComponent<PlayerLifeManager> ();
	}

	// Update is called once per frame
	void Update () {
		if (refHpString != null) {
			Vector3 pos = refHpString.transform.position;
			pos += offset;
			this.transform.position = pos;

			refText.text = "";
			if (refPlayer != null) {
				float lifeVal = refPlayer.GetComponent<PlayerLifeManager> ().Life;
				int lineNum = (int)lifeVal;
			
				if (0.0f < lifeVal && lifeVal < 1.0f) {
					lineNum = 1;
				}
				for (int i = 0; i < lineNum; i++) {
					refText.text += "★";
				}
			}

			float displayTime = refHpString.GetComponent<hpString> ().displayTime;
			if (0.0f < displayTime) {
				this.GetComponent<CanvasRenderer> ().SetAlpha (1.0f);
			}
			if (displayTime <= 0.0f && refHpString.GetComponent<hpString> ().alwaysDisplayBeginLife < refLifeManager.Life ) {
				this.GetComponent<CanvasRenderer> ().SetAlpha (0.0f);
			}
		}
	}

}


