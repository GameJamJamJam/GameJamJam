using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scoreDraw : MonoBehaviour {

	sceneMng mSceneMng;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		mSceneMng =	GameObject.Find ("SceneMng").GetComponent<sceneMng>();

		GameObject.Find ("ScoreInGame").GetComponent<Text> ().text = mSceneMng.getNowScore ().ToString() + "点";	
	}
}
