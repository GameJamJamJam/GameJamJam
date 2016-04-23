using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class status : MonoBehaviour {

	// 
	public float vital
	{
		get;
		private set;
	}

	public exp[] expArray;



	// Use this for initialization
	void Start () {
		vital = 3.0f;

		this.expArray = new exp[(int)item.eExpType.Max];
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

	public void AddExp(item.eExpType expType)
	{
		expArray[(int)expType].addExp( 1 );
	}
}
