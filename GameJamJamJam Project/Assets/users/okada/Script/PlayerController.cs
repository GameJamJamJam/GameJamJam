using UnityEngine;
using System.Collections;

public class PlayerController: MonoBehaviour
{

    

    [SerializeField]
    private float _speed = 6;
    public float Speed
    {
        get { return this._speed; }
        set { this._speed = value; }
    }


    [SerializeField]
    private float _jumpSpeed = 10;
    public float JumpSpeed
    {
        get { return this._jumpSpeed; }
        set { this._jumpSpeed = value; }
    }

    [SerializeField]
    private float _gravity = -9.8f;
    public float Gravity
    {
        get { return this._gravity; }
        set { this._gravity = value; }
    }

    private CharacterController cc;
    private Vector3 dir;
    private float h;

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

        h = Input.GetAxis("Horizontal");
        if (cc.isGrounded)
        {
            dir = new Vector3(h, 0, 0);
            dir = transform.TransformDirection(dir);
            dir *= _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                dir.y = _jumpSpeed;
            }
        }
        dir.y += _gravity * Time.deltaTime;
        cc.Move(dir * Time.deltaTime);

        if (Input.GetButton("Fire1"))
        {
            AttackNear();
        }
        else if( Input.GetButton("Fire2") )
        {
            AttackFar();
        }
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