using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Constants;

public class GameManager : MonoBehaviour
{

    [SerializeField] Text timerText;
    [SerializeField] Text messageDisplay;   // �A�C�e���擾���ɕ\���p
    [SerializeField] Text actionDisplay;    // �펞�\���p
    public Text warningText;                // �������B���ŕ\���p

    [SerializeField] AudioSource audioSource1;  // �m�[�}��BGM�p
    [SerializeField] AudioSource audioSource2;  // �c�莞�Ԍ���BGM�p

    [SerializeField] AudioClip bgm1;    // �m�[�}��BGM
    [SerializeField] AudioClip bgm2;    // �c�莞�Ԍ���BGM

    public AudioClip seGetGem;    // ��Ύ擾����SE
    public AudioClip seFireDamage;    // �Ă��ꂽSE
    public AudioClip seMimic;    // �~�~�b�N���x���ꂽSE
    public AudioClip seFall;    // �S���X���Ƃ���SE
    public AudioClip seBolder;    // ������


    // �v���C���[�̃X�N���v�g�������Ă����p�̕ϐ�
    [SerializeField] PlayerManager playerManager;
    
    // �ŏI�I�ɃV�[���O�ɓn���X�R�A
    // static�ɂ��Ă����ƕʃV�[������G���
    public static int scoreMaster;

    //�c�莞��.
    [SerializeField] float countTime; 
    // BGM���؂�ւ��^�C�~���O
    float bgmFlipTime;

    // �A�C�e���擾���̃��b�Z�[�W�̃^�C�}�[�Ǘ��p
    float deltaTimeDisplay;
    float displayEraseTime;

    // BGM�����łɐ؂�ւ���Ă��邩�ǂ���
    bool isBGMFlip;
    // �Q�[�����I���������ǂ���
    bool isGameEnd;
    // �Q�[���I�����Ƀv���C���[���X�^�[�g�n�_�ɂ��邩�ǂ���
    public bool isPlayerReturn;


    // Start is called before the first frame update
    void Start()
    {
        // BGM���ƂɃ\�[�X��ݒ�
        audioSource1.clip = bgm1;
        audioSource2.clip = bgm2;

        // �ʏ�BGM���Đ��J�n
        audioSource1.Play();


        // �ϐ�������


        messageDisplay.gameObject.SetActive(false);

        // �^�C�}�[������
        countTime = GameParam.TIME_LIMIT;
        // BGM�̐؂�ւ�莞�ԁ@�v����
        bgmFlipTime = GameParam.BGM_FLIP_TIME;



        // EraseTime�b�ԃ��b�Z�[�W�\��
        deltaTimeDisplay = 0;
        displayEraseTime = 5.0f;


        isBGMFlip = false;
        isGameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�J�E���g�_�E��
        Timer();

        // �e�X�g�p
        TestFunc();

        // �Q�[���I���`�F�b�N
        GameEndCheck();
    }



    /// <summary>
    /// �^�C�}�[�����֐�
    /// ���Ԍo�߂ōs������
    /// </summary>
    void Timer()
    {
        if(!isGameEnd)
        {
            //�J�E���g�_�E��
            countTime -= Time.deltaTime;
            //print(countTime);

            // �e�L�X�g�ɕ\��
            timerText.text = "�c�莞�ԁF" + countTime.ToString("f2");

            // �c�莞�Ԃ�����̃��C���𒴂����BGM�؂�ւ�
            if (countTime < bgmFlipTime)
            {
                playerManager.isNoTime = true;
                BGMFlip();
            }

            // �c��̎��Ԃ�0�ɂȂ�ƃQ�[���I��
            if (countTime < 0)
            {
                isGameEnd = true;
            }
        }
        // ���b�Z�[�W��ʏ���
        DisplayControll();
        
    }

    void TestFunc()
    {
        
    }

    /// <summary>
    /// BGM�؂�ւ��֐�
    /// �c�莞�Ԃ����Ȃ��Ȃ�����Ă΂��
    /// </summary>
    void BGMFlip()
    {
        if(!isBGMFlip)
        {
            audioSource1.Stop();
            audioSource2.Play();
            isBGMFlip = true;
        }
    }

    /// <summary>
    /// �Q�[���I�����̏����֐�
    /// </summary>
    void GameEndCheck()
    {
        if(isGameEnd)
        {
            scoreMaster = playerManager.tmpScore;
            //�����Ńv���C���[�������ʒu�ɂ��邩�ǂ����𔻒�
            if(isPlayerReturn)
            {
                // ���Ƃ����Ő������o�������Ȃ��̂�0�_�ł�GameOver��
                if(scoreMaster <= 0)
                {
                    GameOver();
                }
                else
                {
                    // �v���C���[���X�^�[�g�n�_�ɂ��邩���ʂ�����Ƃ�
                    GameClear();
                }
            }
            else
            {
                GameOver();
            }
        }
    }

    /// <summary>
    /// �Q�[���N���A�����֐�
    /// </summary>
    void GameClear()
    {
        SceneManager.LoadScene("GameClear");
    }

    /// <summary>
    /// �Q�[�����s�����֐�
    /// </summary>
    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    /// <summary>
    /// ���b�Z�[�W�\��
    /// �_���[�W���b�Z�[�W�͐Ԃ��\��
    /// </summary>
    public void MessageDisplay(string ms, bool isDamage)
    {
        if(isDamage)
        {
            messageDisplay.color = Color.red;
        }
        else
        {
            messageDisplay.color = Color.white;
        }

        // �w�莞�Ԍ�ɏ���
        deltaTimeDisplay = displayEraseTime;
        messageDisplay.text = ms;
    }

    /// <summary>
    /// ��ʉE���̃f�B�X�v���C
    /// �펞�\���\��
    /// </summary>
    public void ActionMessage(string msg)
    {
        actionDisplay.text = msg;
    }

    /// <summary>
    /// ���b�Z�[�W�𐧌䂷��
    /// </summary>
    void DisplayControll()
    {
        deltaTimeDisplay -= Time.deltaTime;
        if (deltaTimeDisplay < 0)
        {
            messageDisplay.text = null;
            messageDisplay.gameObject.SetActive(false);
        }
        else
        {
            messageDisplay.gameObject.SetActive(true);
        }
    }


    /// <summary>
    /// ��Ύ擾���̌��ʉ��Đ��֐�
    /// </summary>
    public void SEPlay(AudioClip se)
    {
        audioSource1.PlayOneShot(se);
    }

}
