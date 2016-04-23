using UnityEngine;
using System.Collections;

public class status : MonoBehaviour {

	// 
	public float vital
	{
		get;
		private set;
	}


	// Use this for initialization
	void Start () {
		vital = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnDamage(){
		vital -= 1.0f;
		if( vital < 0.0f ){
			vital = 0.0f;
		}
	}
}
