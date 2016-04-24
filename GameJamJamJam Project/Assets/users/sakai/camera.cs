using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

	public float[] CamDist = new float[10];

	PlayerController mPlCtrl;

	// Use this for initialization
	void Start () {
		mPlCtrl = GameObject.Find ("Player").GetComponent<PlayerController> ();

	}
	
	// Update is called once per frame
	void Update () {
		updateCamLevel ();
	}

	void updateCamLevel()
	{
		int camLevel = mPlCtrl.Levels[(int)item.eExpType.Jump];
		Vector3 camPos = mPlCtrl.gameObject.transform.position;
		camPos.z = CamDist [camLevel/5];

		Camera.main.transform.position = camPos;
	}

}
