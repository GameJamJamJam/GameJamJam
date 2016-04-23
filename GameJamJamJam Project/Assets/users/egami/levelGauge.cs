using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using UnityEngine.UI;

public class levelGauge : MonoBehaviour {

	public Color color;
	public string baseText;
	public exp refExp;
	public GameObject refPlayerStatus;
	public item.eExpType expType;

	// Use this for initialization
	void Start () {
		refPlayerStatus = GameObject.Find ("PlayerStatus");
		GetComponent<Text> ().color = color;
	}
	
	// Update is called once per frame
	void Update () {
		if (refPlayerStatus) {
			Debug.Log ("TEST");
			Debug.Log (refPlayerStatus.GetComponent<status> ());
			exp tmpExp = refPlayerStatus.GetComponent<status> ().expArray [(int)expType];
			Debug.Log (tmpExp);
			GetComponent<Text> ().text = "Lv:";
			GetComponent<Text> ().text += tmpExp.level.ToString();
		}
		
	}

}
