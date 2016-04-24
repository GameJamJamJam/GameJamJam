using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using UnityEngine.UI;

public class levelGauge : MonoBehaviour {

	public string baseText;
	public string caption;
	public exp refExp;
	public GameObject refPlayerStatus;
	public item.eExpType expType;
	public float displayTime;

	// Use this for initialization
	void Start () {
		refPlayerStatus = GameObject.Find ("PlayerStatus");
		displayTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (refPlayerStatus) {
			exp tmpExp = refPlayerStatus.GetComponent<status> ().expArray [(int)expType];
			Text tmpText = GetComponent<Text> ();
			if (tmpText) {
				tmpText.text = caption;
				tmpText.text += ":";
				tmpText.text += tmpExp.level.ToString ();
				tmpText.text += "  [";
				tmpText.text += tmpExp.sub.ToString ();
				tmpText.text += " / ";
				tmpText.text += tmpExp.next.ToString ();
				tmpText.text += "]";
			}
		}
		if (0.0f < displayTime) {
			displayTime -= Time.deltaTime;
			this.GetComponent<CanvasRenderer> ().SetAlpha(1.0f);
		}
		if (displayTime <= 0.0f) {
			this.GetComponent<CanvasRenderer> ().SetAlpha(0.0f);
		}

	}
	public void setDisplayTime(float sec){
		displayTime = sec;
	}

}
