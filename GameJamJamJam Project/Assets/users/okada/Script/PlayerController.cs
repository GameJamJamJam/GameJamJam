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

    public bool IsRight
    {
        get
        {
            return dir.x > 0;
        }
    }

    public delegate void EventAttack();
    public EventAttack EventAttackNear;
    public EventAttack EventAttackFar;

    public List<GameObject> LevelUpEffects = new List<GameObject>();

    public List<AudioClip> VoiceList = new List<AudioClip>();
    public AudioSource voiceAudio;
    public AudioSource levelUpAudio;
    public AudioSource jumpAudio;
    public AudioSource attackSE;

    public GameObject DamageEffect;

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

        EventAttackNear += PlayAttackVoice;
        EventAttackFar += PlayAttackVoice;
    }
    
    // Use this for initialization
    void Start()
    {
        h = 0;
        dir = Vector3.zero;
        cc = gameObject.GetComponent<CharacterController>();
        //audioSource = gameObject.GetComponent<AudioSource>();
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

			if ( Input.GetButton("Jump"))
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
			if(Input.GetButtonDown("Jump"))
			//if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                dir.y = _jumpFirstSpeed;
                jumpAudio.Play();
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

        var effect = Instantiate(LevelUpEffects[(int)expType], transform.position + new Vector3(0, 0, -3.0f), Quaternion.identity) as GameObject;
        effect.transform.parent = this.gameObject.transform;

        levelUpAudio.Play();
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

		yield return new WaitForSeconds(0.6f);

        _isCanAttackNear = true;
    }

    private IEnumerator ResumeCanAttackFar()
    {
        _isCanAttackFar = false;

		int shotBullet = Levels [(int)item.eExpType.ShotPow];
		float wait = 0.2f - shotBullet * 0.002f;
		if (wait < 0.01f) {
			wait = 0.01f;
		}

		yield return new WaitForSeconds(wait);

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

		int meleeHit = Levels [(int)item.eExpType.MeleePow];
		int meleePow = Levels [(int)item.eExpType.MeleePow]+1;

		int numShell = 2 + meleeHit / 2;
		float maxDeg = 30.0f + meleeHit * 5;
		float deltaDeg = maxDeg / (numShell-1);

		for (int i = 0; i < numShell; i++) {
			GameObject obj = Instantiate (Resources.Load ("ShellPlNear"), playerPos, Quaternion.identity) as GameObject;


			int idx = i - (numShell / 2);
			float offsetDeg = 0.0f;

			// 偶数ならオフセット
			if (numShell % 2 == 0) {
				offsetDeg = deltaDeg / 2.0f;
			}

			float vecX = Mathf.Cos ((idx * deltaDeg + offsetDeg) / 360.0f * 2.0f * 3.14159265f);
			float vecY = Mathf.Sin ((idx * deltaDeg + offsetDeg) / 360.0f * 2.0f * 3.14159265f);
			Vector3 vec;

			if (IsRight) {
				vec = new Vector3 (vecX, vecY, 0.0f);
			} else {
				vec = new Vector3 (-vecX, vecY, 0.0f);
			}
			obj.GetComponent<shellPlNear> ().initDir = vec;
			obj.GetComponent<shellPlNear> ().power = meleePow * meleePow * 5;

			obj.GetComponent<shellPlNear> ().ttl = 0.4f + meleeHit * 0.01f;
		}

		EventAttackNear();
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

		int shotBullet = Levels [(int)item.eExpType.ShotPow];
		int shotPow = Levels [(int)item.eExpType.ShotPow]+1;


		GameObject obj = Instantiate (Resources.Load ("ShellPlFar"), playerPos, Quaternion.identity) as GameObject;
		if (IsRight) {
			obj.GetComponent<shellPlFar> ().initDir = Vector3.right;
		} else {
			obj.GetComponent<shellPlFar> ().initDir = Vector3.left;
		}
		obj.GetComponent<shellPlFar> ().power = shotPow * shotPow * shotPow;


        EventAttackFar();
        StartCoroutine("ResumeCanAttackFar");

    }

    void PlayAttackVoice()
    {
        //VoiceList[0].Play();

        if (!voiceAudio.isPlaying)
        {
            int id = Random.Range(0, 3);
            voiceAudio.clip = VoiceList[id];
            voiceAudio.Play();
        }
        attackSE.Play();
    }

    public void PlayDamageVoice()
    {
        int id = Random.Range(3, 7);

        voiceAudio.clip = VoiceList[id];
        voiceAudio.Play();

        var effect = Instantiate(DamageEffect, transform.position, Quaternion.identity) as GameObject;
        effect.transform.parent = this.gameObject.transform;
    }
}