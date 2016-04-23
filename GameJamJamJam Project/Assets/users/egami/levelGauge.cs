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
			Text tmpText = GetComponent<Text> ();
			if (tmpText) {
				tmpText.text = "Lv:";
				tmpText.text += tmpExp.level.ToString ();
				tmpText.text += " [";
				tmpText.text += tmpExp.sub.ToString ();
				tmpText.text += ":";
				tmpText.text += tmpExp.total.ToString ();
				tmpText.text += "]";
			}
		}
		
	}

}
