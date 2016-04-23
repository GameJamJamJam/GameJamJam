using UnityEngine;
using System.Collections;

public class sceneMng : MonoBehaviour {

	public enum eSceneChangeType
	{
		ToGame,
		ToResult,
		ToTitle,
	}

	public eSceneChangeType mSceneChangeType;

	const float cDelayTime = 0.4f;
	float mDelayTimer;

	// Use this for initialization
	void Start () {
		mDelayTimer = cDelayTime;
	}
	
	// Update is called once per frame
	void Update () {
		switch (mSceneChangeType) {
		case eSceneChangeType.ToGame:
			mDelayTimer -= Time.deltaTime;
			if(  mDelayTimer < 0 && Input.anyKeyDown)
			{
				mDelayTimer = cDelayTime;
				Application.LoadLevel ("MainGame");
			}

			break;
		case eSceneChangeType.ToResult:
			break;
		case eSceneChangeType.ToTitle:
			mDelayTimer -= Time.deltaTime;
			if( mDelayTimer < 0 && Input.anyKeyDown)
			{
				mDelayTimer = cDelayTime;
				Application.LoadLevel ("Title");
			}
			break;

		}

	}
}
