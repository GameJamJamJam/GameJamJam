using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class status : MonoBehaviour {

	// 
	public float vital;
	public float magicPower;
	public int bulletNum;


	public exp[] expArray;

    public AudioSource GetSEAudio;

	private item.eExpType mLastKillExp;
	private int mKillStreak;

	public item.eExpType getLastKillExp()
	{
		return mLastKillExp;
	}
	public int getKillStreak()
	{
		return mKillStreak;
	}

	public void setLastKill(item.eExpType expType)
	{
		if (mLastKillExp == expType) {
			mKillStreak++;
		} else {
			mKillStreak = 0;
		}
		mLastKillExp = expType;
	}

	// Use this for initialization
	void Start () {
		vital = 3.0f;
		magicPower = 100.0f;
		bulletNum = 100;

		this.expArray = new exp[(int)item.eExpType.Max];
		for (int i = 0; i < expArray.Length; i++) {
			expArray [i] = new exp ();
		}

		mLastKillExp = item.eExpType.None;

        GetSEAudio = gameObject.GetComponent<AudioSource>();

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
		// level up?
		if (expArray [(int)expType].addExp (1)) {
			GameObject tmpPlayer = GameObject.Find ("Player");
			if (tmpPlayer) {
				tmpPlayer.GetComponent<PlayerController>().LevelUpStatus( expType );
			}
		}

        GetSEAudio.Play();

    }
}
