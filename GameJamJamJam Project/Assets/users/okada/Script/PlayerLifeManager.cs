﻿using UnityEngine;
using System.Collections;

public class PlayerLifeManager : MonoBehaviour {

    /// <summary>
    /// プレイヤーの体力
    /// </summary>
    [SerializeField]
    private float _life = 3;
    public float Life
    {
        get { return this._life; }
        set { this._life = value; }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// プレイヤーにダメージを与える
    /// </summary>
    /// <param name="value"></param>
    public void ApplayDamage( float value )
    {


    }
}