using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using UnityEngine.UI;

public class levelGauge : MonoBehaviour {

	public string baseText;
	public exp refExp;
	public GameObject refPlayerStatus;
	public item.eExpType expType;

	// Use this for initialization
	void Start () {
		refPlayerStatus = GameObject.Find ("PlayerStatus");
	}
	
	// Update is called once per frame
	void Update () {
		if (refPlayerStatus) {
			exp tmpExp = refPlayerStatus.GetComponent<status> ().expArray [(int)expType];
			GetComponent<Text> ().text = "Lv:";
			GetComponent<Text> ().text += tmpExp.level.ToString();
			GetComponent<Text> ().text += " [";
			GetComponent<Text> ().text += tmpExp.sub.ToString();
			GetComponent<Text> ().text += ":";
			GetComponent<Text> ().text += tmpExp.total.ToString();
			GetComponent<Text> ().text += "]";
		}
		
	}

}
