using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bulletNum : MonoBehaviour {

	public GameObject refImage;
	public Text refText;
	public status refPlayerStatus;
	public GameObject refPlayer;

	// Use this for initialization
	void Start () {
		refImage = GameObject.Find ("BullteImage");
		refText = GetComponent ("Text") as Text;
		GameObject tmp = GameObject.Find ("PlayerStatus");
		if (tmp) {
			refPlayerStatus = tmp.GetComponent<status>();
		}
		refPlayer = GameObject.Find ("Player");
	}

	// Update is called once per frame
	void Update () {
		if (refPlayer != null) {
			refText.text = "x";
			refText.text += refPlayerStatus.bulletNum.ToString();
		}
	}
}
