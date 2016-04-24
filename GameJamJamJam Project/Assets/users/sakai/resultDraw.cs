using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class resultDraw : MonoBehaviour {

	sceneMng mSceneMng;

	// Use this for initialization
	void Start () {
	
		mSceneMng =	GameObject.Find ("SceneMng").GetComponent<sceneMng>();

		GameObject textObj = GameObject.Find ("score");
		if (textObj) {
			textObj.GetComponent<Text> ().text = mSceneMng.getLastScore () + "点";
		}
		GameObject.Find ("highScore").GetComponent<Text> ().text = mSceneMng.getHighScore () + "点";


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
