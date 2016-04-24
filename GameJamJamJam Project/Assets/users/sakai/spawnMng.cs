using UnityEngine;
using System.Collections;

public class spawnMng : MonoBehaviour {

	int mEnemyNum;
	int mEnemyTotalNum;
	//int[] mSpawnNum ={3,6,12,20,20,20,22,24,26,28,30,32,34,36,38,};

	// Use this for initialization
	void Start () {
		mEnemyNum = 0;
		mEnemyTotalNum = 0;
	}

	// Update is called once per frame
	void Update () {

	}

	public bool isBossOrder()
	{
		int bossNum = 5;
		return (mEnemyTotalNum % bossNum == bossNum - 1);
	}

	public bool isFastOrder()
	{
		int fastNum = 10;
		return (mEnemyTotalNum % fastNum == fastNum - 1);
	}

	public bool isCanSpawn()
	{
		int currentLv = GameObject.Find ("Player").GetComponent<PlayerController> ().SumLevel;
		//const int maxLv = 10;
		//currentLv = Mathf.Clamp (currentLv, 0, mSpawnNum.Length-1);

		if (currentLv + 5 > 200) {
			return (200 > mEnemyNum);
		} else {
			return (currentLv + 5 > mEnemyNum);
		}
		//return (currentLv+3 > mEnemyNum);
	}

	public void addEnemyNm()
	{
		mEnemyNum++;
		mEnemyTotalNum++;
	}
	public void subEnemyNm(){
		mEnemyNum--;
	}

}