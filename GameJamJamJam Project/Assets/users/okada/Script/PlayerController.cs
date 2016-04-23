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
    private float _jumpSpeed = 10;
    public float JumpSpeed
    {
        get { return this._jumpSpeed; }
        set { this._jumpSpeed = value; }
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

    private CharacterController cc;
    private Vector3 dir;
    private float h;

    
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
        if (Input.GetButton("Fire1"))
        {
            AttackNear();
        }
        else if( Input.GetButton("Fire2") )
        {
            AttackFar();
        }
    }

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
        }
        else
        {
            dir.y += _gravity * Time.deltaTime;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dir.y = _jumpSpeed;
        }

        cc.Move(dir * Time.deltaTime);

    }

    /// <summary>
    /// レベルアップ
    /// </summary>
    /// <param name="expType"></param>
    public void LevelUpStatus( item.eExpType expType )
    {
        Levels[(int)expType]++;
        Debug.Log(this.gameObject.ToString() + " Level Up :" + expType.ToString());
    }

    /// <summary>
    /// 近距離攻撃
    /// </summary>
    void AttackNear()
    {
        Debug.Log(this.gameObject.ToString() + " Fire Near!");
    }

    /// <summary>
    /// 遠距離攻撃
    /// </summary>
    void AttackFar()
    {
        Debug.Log(this.gameObject.ToString() + " Fire Far!");
    }
}