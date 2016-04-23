using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController: MonoBehaviour
{
    [SerializeField]
    private float _accel = 50;
    public float Accel
    {
        get { return this._accel; }
        set { this._accel = value; }
    }

    [SerializeField]
    private float _speedMax = 6;
    public float SpeedMax
    {
        get { return this._speedMax; }
        set { this._speedMax = value; }
    }

    [SerializeField]
    private float _jumpFirstSpeed = 10;
    public float JumpFirstSpeed
    {
        get { return this._jumpFirstSpeed; }
        set { this._jumpFirstSpeed = value; }
    }

    [SerializeField]
    private float _jumpHoldSpeed = 10;
    public float JumpHoldSpeed
    {
        get { return this._jumpHoldSpeed; }
        set { this._jumpHoldSpeed = value; }
    }

    [SerializeField]
    private float _friction = 5.0f;
    public float Friction
    {
        get { return this._friction; }
        set { this._friction = value; }
    }

    [SerializeField]
    private float _gravity = -9.8f;
    public float Gravity
    {
        get { return this._gravity; }
        set { this._gravity = value; }
    }

    public int SumLevel
    {
        get
        {
            int sum = 0;
            foreach( var exp in Levels )
            {
                sum += exp;
            }
            return sum;
        }
    }

    /// <summary>
    /// 各ステータス
    /// </summary>
    public List<int> Levels = new List<int>();

    private int _jumpCount;
    private int _jumpCountMax = 1;
    
    private CharacterController cc;
    private Vector3 dir;
    private float h,v ;

	private GameObject playerObj;
	private Vector3 playerPos;

    private bool _isCanAttackNear = true;
    private bool _isCanAttackFar = true;

    /// <summary>
    /// Awake
    /// </summary>
    void Awake()
    {
        // 各ステータス
        for( int i=0; i < (int)item.eExpType.Max; i++ )
        {
            Levels.Add( 0 );
        }
    }
    
    // Use this for initialization
    void Start()
    {
        h = 0;
        dir = Vector3.zero;
        cc = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        // 攻撃
        if (IsCanAttackNear())
        {
            if (Input.GetButton("Fire1"))
            {
                AttackNear();
            }
        }

        if (IsCanAttackFar())
        {
            if (Input.GetButton("Fire2"))
            {
                AttackFar();
            }
        }
    }

    /// <summary>
    /// LateUpdate
    /// </summary>
    void LateUpdate()
    {
        // 外に出ないように無理やり
        var pos = this.gameObject.transform.position;
        pos.z = 0.0f;
        this.gameObject.transform.position = pos;
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    void Move()
    {
        // 横入力
        h = Input.GetAxis("Horizontal");
        dir.x += h * _accel * Time.deltaTime;
        dir.x += -dir.x * _friction * Time.deltaTime;
        dir.x = Mathf.Clamp(dir.x, -SpeedMax, SpeedMax);

        // Gravity
        if (cc.isGrounded)
        {
            dir.y = 0.0f;
            _jumpCount = 0;
        }
        else
        {
            dir.y += _gravity * Time.deltaTime;

            if (Input.GetKey(KeyCode.Space))
            {
                dir.y += _jumpHoldSpeed * Time.deltaTime;
            }
        }

        // 縦入力
        v = Input.GetAxis("Vertical");
        if( v < 0 )
        {
            dir.y += v * 10.0f * _accel * Time.deltaTime;
        }

        // Jump
        if ( IsCanJump() )
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dir.y = _jumpFirstSpeed;
                _jumpCount++;
            }
        }
        cc.Move(dir * Time.deltaTime);

    }

    bool IsCanJump()
    {
        if( _jumpCount < Levels[(int)item.eExpType.Jump] + 1)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// レベルアップ
    /// </summary>
    /// <param name="expType"></param>
    public void LevelUpStatus(item.eExpType expType)
    {
        Levels[(int)expType]++;
        Debug.Log(this.gameObject.ToString() + " Level Up :" + expType.ToString());

        switch (expType)
        {
            default:
                break;
        }
    }

    /// <summary>
    /// 近距離攻撃を使用できるか
    /// </summary>
    /// <returns></returns>
    bool IsCanAttackNear()
    {
        return _isCanAttackNear;
    }

    /// <summary>
    /// 遠距離攻撃を使用できるか
    /// </summary>
    /// <returns></returns>
    bool IsCanAttackFar()
    {
        return _isCanAttackFar;
    }

    private IEnumerator ResumeCanAttackNear()
    {
        _isCanAttackNear = false;
        yield return new WaitForSeconds(0.1f);

        _isCanAttackNear = true;
    }

    private IEnumerator ResumeCanAttackFar()
    {
        _isCanAttackFar = false;

        yield return new WaitForSeconds(0.1f);

        _isCanAttackFar = true;
    }

    /// <summary>
    /// 近距離攻撃
    /// </summary>
    void AttackNear()
    {
        //Debug.Log(this.gameObject.ToString() + " Fire Near!");

		playerObj = GameObject.Find ("Player");
		playerPos = playerObj.transform.position;
		GameObject obj = Instantiate (Resources.Load ("Shell"), playerPos, Quaternion.identity) as GameObject;
		obj.GetComponent<shell> ().initDir = Vector3.left;

        StartCoroutine("ResumeCanAttackNear");
    }

    /// <summary>
    /// 遠距離攻撃
    /// </summary>
    void AttackFar()
    {
        //Debug.Log(this.gameObject.ToString() + " Fire Far!");

		playerObj = GameObject.Find ("Player");
		playerPos = playerObj.transform.position;
		GameObject obj = Instantiate (Resources.Load ("Shell"), playerPos, Quaternion.identity) as GameObject;
		obj.GetComponent<shell> ().initDir = Vector3.right;

        StartCoroutine("ResumeCanAttackFar");

    }
}