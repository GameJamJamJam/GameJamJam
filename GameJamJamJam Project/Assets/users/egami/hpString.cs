using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class hpString : MonoBehaviour {

	public float displayTime;
	//public Camera refMainCamera;
	//public GameObject refPlayer;
	//public Canvas refCanvas;

	// Use this for initialization
	void Start () {
		displayTime = 0.0f;
		//refMainCamera = (Camera)GameObject.Find ("Main Camera").GetComponent<Camera>();
		//refPlayer = GameObject.Find ("Player");
		//refCanvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (0.0f < displayTime) {
			displayTime -= Time.deltaTime;
			this.GetComponent<CanvasRenderer> ().SetAlpha(1.0f);
		} else {
			this.GetComponent<CanvasRenderer> ().SetAlpha(0.0f);
		}
		#if FALSE
		if (refMainCamera && refPlayer) {
			//RectTransform UI_Element;
			RectTransform CanvasRect = refCanvas.GetComponent<RectTransform> ();
			Vector2 ViewportPosition = refMainCamera.WorldToViewportPoint (refPlayer.transform.position);
			this.transform.position = ViewportPosition;
			/*
			Vector2 WorldObject_ScreenPosition = new Vector2 (
				                                     ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
				                                     ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

			this.transform.position = WorldObject_ScreenPosition;
			Debug.Log (refPlayer.transform.position);
			Debug.Log (WorldObject_ScreenPosition);
			*/
		}
		#endif
	}
	public void setDisplayTime(float sec){
		displayTime = sec;
	}
}
