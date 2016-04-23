﻿using UnityEngine;
using System.Collections;

public class PlayerController: MonoBehaviour
{

    private float h;
    private float v;

    private float speed;
    private float jumpspeed = 12f;

    private CharacterController cc;
    private Vector3 dir;

    // Use this for initialization
    void Start()
    {
        h = 0;
        v = 0;
        speed = 6;
        dir = Vector3.zero;
        cc = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        if (cc.isGrounded)
        {

            dir = new Vector3(h, 0, v);
            dir = transform.TransformDirection(dir);
            dir *= speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                dir.y = jumpspeed;
            }
        }

        dir.y -= 20f * Time.deltaTime;

        cc.Move(dir * Time.deltaTime);



        Debug.Log("" + h + "," + v + "," + jumpspeed);
    }

}