using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class PlayerMove : MonoBehaviour
{
    // 変数を宣言
    public float speed;

    Animator animator;

    bool isRight;
    bool isLeft;
    bool isUp;
    bool isDown;

    // Start is called before the first frame update
    void Start()
    {
        // 変数と変えたい軸を書く
        speed = PlayerParam.SPEED_NORMAL;   // 定数は使うたびに[クラス名].[定数名]とかく

        animator = GetComponent<Animator>();

        isRight = false;
        isLeft = false;
        isUp = false;
        isDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        // キー入力
        InputKey();

        // アニメーション
        SetAnimation();
    }


    /// <summary>
    /// キー入力関数
    /// </summary>
    void InputKey()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            //移動方向のspeedを現在座標に足す
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //-を変数に付けるだけ
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
        }


        // アニメーションの制御
        // 制御するために各アニメーションに対応したフラグを管理
        {
            // アニメーションをオンにする
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                isRight = true;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                isLeft = true;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                isUp = true;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                isDown = true;
            }


            // アニメーションをオフにする
            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            {
                isRight = false;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
            {
                isLeft = false;
            }
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
            {
                isUp = false;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
            {
                isDown = false;
            }
        }
        
    }

    /// <summary>
    /// キー入力から制御しているフラグをみてアニメーションの再生を行う
    /// SetTriggerは if else で書かないと上手くいかないことが多い
    /// </summary>
    void SetAnimation()
    {
        if(isRight)
        {
            animator.SetTrigger("Right");
        }
        else if(isLeft)
        {
            animator.SetTrigger("Left");
        }
        else if(isUp)
        {
            animator.SetTrigger("Up");
        }
        else if(isDown)
        {
            animator.SetTrigger("Down");
        }
        else
        {
            animator.SetTrigger("Idle");
        }

    }
}
