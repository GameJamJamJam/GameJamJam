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

	private int mScore;


	const float cDelayTime = 0.4f;
	float mDelayTimer;

	// Use this for initialization
	void Start () {
		mDelayTimer = cDelayTime;
		mScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		switch (mSceneChangeType) {
		case eSceneChangeType.ToGame:
			mDelayTimer -= Time.deltaTime;
			if(  mDelayTimer < 0 && Input.anyKeyDown)
			{
				mDelayTimer = cDelayTime;
				Application.LoadLevel ("Scene/MainGame");
			}

			break;
		case eSceneChangeType.ToResult:

			GameObject player = GameObject.Find ("Player");
			if (player) {
				if (player.GetComponent<PlayerLifeManager> ().Life <= 0) {

					saveScore ();
					Application.LoadLevel ("Scene/Result");
				}
			}
			break;
		case eSceneChangeType.ToTitle:
			mDelayTimer -= Time.deltaTime;
			if( mDelayTimer < 0 && Input.anyKeyDown)
			{
				mDelayTimer = cDelayTime;
				Debug.Log ("hoge");
				Application.LoadLevel ("Scene/Title");
			}
			break;

		}

	}



	//スコアを加算
	public void addScore(float dmgVal)
	{
		mScore += (int)dmgVal;
	}

	//最後にプレイしたゲームのスコア取得
	public int getLastScore()
	{
		return PlayerPrefs.GetInt ("LastScore", 0);
	}

	//ハイスコア取得
	public int getHighScore()
	{
		return PlayerPrefs.GetInt ("HightScore", 0);
	}

	//スコアを保存する
	public void saveScore()
	{
		PlayerPrefs.SetInt ("LastScore", mScore);

		int hightscore = getHighScore ();
		if (hightscore < mScore) {
			PlayerPrefs.SetInt ("HightScore", mScore);
		}
	}
}
