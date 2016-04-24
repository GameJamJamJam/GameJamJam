using UnityEngine;
using System.Collections;

public class spawnMng : MonoBehaviour {

	int mEnemyNum;
	int[] mSpawnNum ={3,6,12,20,35,50,80,100};

	// Use this for initialization
	void Start () {
		mEnemyNum = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public bool isCanSpawn()
	{
		int currentLv = GameObject.Find ("Player").GetComponent<PlayerController> ().SumLevel;
		const int maxLv = 10;
		currentLv = Mathf.Clamp (currentLv, 0, mSpawnNum.Length-1);

		return (mSpawnNum[currentLv] > mEnemyNum);
	}

	public void addEnemyNm()
	{
		mEnemyNum++;
	}
	public void subEnemyNm(){
		mEnemyNum--;
	}

}
