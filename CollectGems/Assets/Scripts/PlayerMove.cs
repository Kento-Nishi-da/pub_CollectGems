using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class PlayerMove : MonoBehaviour
{
    // �ϐ���錾
    public float speed;

    Animator animator;

    bool isRight;
    bool isLeft;
    bool isUp;
    bool isDown;

    // Start is called before the first frame update
    void Start()
    {
        // �ϐ��ƕς�������������
        speed = PlayerParam.SPEED_NORMAL;   // �萔�͎g�����т�[�N���X��].[�萔��]�Ƃ���

        animator = GetComponent<Animator>();

        isRight = false;
        isLeft = false;
        isUp = false;
        isDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        // �L�[����
        InputKey();

        // �A�j���[�V����
        SetAnimation();
    }


    /// <summary>
    /// �L�[���͊֐�
    /// </summary>
    void InputKey()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            //�ړ�������speed�����ݍ��W�ɑ���
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //-��ϐ��ɕt���邾��
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


        // �A�j���[�V�����̐���
        // ���䂷�邽�߂Ɋe�A�j���[�V�����ɑΉ������t���O���Ǘ�
        {
            // �A�j���[�V�������I���ɂ���
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


            // �A�j���[�V�������I�t�ɂ���
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
    /// �L�[���͂��琧�䂵�Ă���t���O���݂ăA�j���[�V�����̍Đ����s��
    /// SetTrigger�� if else �ŏ����Ȃ��Ə�肭�����Ȃ����Ƃ�����
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
