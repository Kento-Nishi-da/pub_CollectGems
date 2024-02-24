using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using Constants;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject cameraMain;

    PlayerMove pMove;

    SpriteRenderer sr;

    List<GemManager> getGemsList = new List<GemManager>();

    [SerializeField] GameManager gm;

    Transform touchedBox;

    [SerializeField] Text actionText;

    // �K�i��Transform
    [SerializeField] Transform startArea;
    // �K�i�̈ʒu�������q�̎O�p�`
    Transform childTriangle;



    // ���̕ϐ��ɐG�ꂽ��΂̃X�R�A���i�[
    public int tmpScore;
    public int gemsCount;


    // �ړ����x�ቺ��臒l
    int gemsLimitAmount;


    float damageSpan;
    float damageDeltaTime;
    bool damageFlg;

    // �펞�\�����b�Z�[�W
    string actionMes;

    [SerializeField] bool isSpace;

    const float gaugeDecAmount = 6f;


    bool isCanOpenBox;
    public bool isNoTime;

    // Start is called before the first frame update
    void Start()
    {
        touchedBox = null;
        childTriangle = transform.GetChild(0);

        // �R���|�[�l���g�擾
        pMove = GetComponent<PlayerMove>();
        sr = GetComponent<SpriteRenderer>();

        // �ϐ�������
        tmpScore = 0;

        damageSpan = 1.0f;
        damageDeltaTime = damageSpan;
        damageFlg = false;
        gemsLimitAmount = GameParam.GEMS_LIMIT_AMOUNT;


        isCanOpenBox = false;
        isNoTime = false;

    }

    // Update is called once per frame
    void Update()
    {
        // �L�[����
        InputKey();

        // ��΂̌�
        GemsCountCheck();

        // �_���[�W���󂯂Ă�����_��
        DamageEffect();

        // �A�N�V�������b�Z�[�W�̔��菈��
        ActionMessageCheck();

        // �K�i�̈ʒu�\��
        ShowExit();
    }


    /// <summary>
    /// �_�ŏ����̊֐�
    /// </summary>
    void DamageEffect()
    {
        damageDeltaTime += Time.deltaTime;
        if (damageDeltaTime < damageSpan)
        {
            damageFlg = !damageFlg;
            if (damageFlg)
            {

                sr.color = Color.clear;
            }
            else
            {
                sr.color = Color.white;
            }
        }
        else
        {
            sr.color = Color.white;
        }
    }

    private void LateUpdate()
    {
        // �J�����ړ�
        SetCameraPosition();
    }

    /// <summary>
    /// �J������Player�ɒǏ]������
    /// </summary>
    private void SetCameraPosition()
    {
        cameraMain.transform.position =
            new Vector3(
                transform.position.x,
                transform.position.y,
                cameraMain.transform.position.z);
    }

    /// <summary>
    /// �L�[����
    /// </summary>
    void InputKey()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isSpace = true;
        }
        
        if(Input.GetKeyUp(KeyCode.Space))
        {
            isSpace = false;
        }

        if(isSpace)
        {
            if(touchedBox != null)
            {
                Vector3 tmp = touchedBox.localScale;
                tmp.x -= gaugeDecAmount * Time.deltaTime;
                touchedBox.localScale = tmp;
            }
        }
    }

    /// <summary>
    /// ��Ίl�������̊֐�
    /// </summary>
    public void GetGem(GemManager gem)
    {
        getGemsList.Add(gem);
        ScoreCheck();
    }

    void ScoreCheck()
    {
        int tmp = 0;
        gemsCount = getGemsList.Count;

        for(int i = 0; i < getGemsList.Count; i++) 
        {
            tmp += getGemsList[i].score;
        }

        tmpScore = tmp;
    }

    /// <summary>
    /// �v���C���[�ւ̎w�����������b�Z�[�W�̊Ǘ��֐�
    /// </summary>
    void ActionMessageCheck()
    {

        if(isCanOpenBox)
        {
            actionMes = "�X�y�[�X�������Ŕ����J���Ă�����Q�b�g";
        }
        else if (isNoTime)
        {
            actionMes = "�c�莞�Ԃ��O�ɂȂ�^�C�~���O�ŊK�i�ɍs����";
        }
        else
        {
            actionMes = "WASD\n��������\n�ňړ�";
        }

        gm.ActionMessage(actionMes);
    }

    /// <summary>
    /// ��΂̌����Q�Ƃ��ăv���C���[�̈ړ����x��ω�
    /// </summary>
    void GemsCountCheck()
    {
        if(getGemsList.Count > gemsLimitAmount)
        {
            gm.warningText.gameObject.SetActive(true);
            pMove.speed = PlayerParam.SPEED_SLOWLY;
        }
        else
        {
            gm.warningText.gameObject.SetActive(false);
            pMove.speed = PlayerParam.SPEED_NORMAL;
        }
    }

    /// <summary>
    /// �v���C���[���_���[�W���󂯂����̏���
    /// </summary>
    public void PlayerDamage()
    {
        int index = getGemsList.Count - 1;
        if (getGemsList.Count > 0)
        {
            getGemsList.RemoveAt(index);
        }


        ScoreCheck();

        damageDeltaTime = 0;
    }

    /// <summary>
    /// �K�i�̈ʒu��\������֐�
    /// </summary>
    void ShowExit()
    {
        // ���������߂�
        Vector3 vec =  startArea.position - transform.position;
        float rad = Mathf.Atan2 (vec.y, vec.x) * Mathf.Rad2Deg;

        childTriangle.rotation = Quaternion.Euler (0f, 0f, rad - 90f);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.tag == "Fire")
        {
            for(int i = 0; i < 2; i++)
            {
                PlayerDamage();
            }
            gm.SEPlay(gm.seFireDamage);
            gm.MessageDisplay("�����I��΂𗎂Ƃ����I", true);
        }

        if(obj.tag == "Holl")
        {
            while(getGemsList.Count > 0)
            {
                PlayerDamage();
            }

            
            gm.SEPlay(gm.seFall);
            transform.position = Vector3.zero;
            gm.MessageDisplay("���ׂẲו����������I", true);

        }

        if (obj.tag == "Box" || obj.tag == "Mimic")
        {
            touchedBox = obj.transform.GetChild(0);
            touchedBox.gameObject.SetActive(true);

            isCanOpenBox = true;
            if(obj.tag == "Mimic")
            {
                Mimic mm = obj.GetComponent<Mimic>();
                mm.animator.SetTrigger("Nears");
            }
        }

        if( obj.tag == "Bolder")
        {
            for( int i = 0;i < 3;i++)
            {
                PlayerDamage();
            }

            gm.SEPlay(gm.seBolder);
            gm.MessageDisplay("�ォ��傫�Ȋ₪�I", true);
        }


        if(obj.tag == "Spike")
        {

            for(int i = 0; i < 2;i++)
            {
                PlayerDamage();
            }

            gm.SEPlay(gm.seBolder);
            gm.MessageDisplay("�ǂ���g�Q���I", true);

        }
        

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.tag == "Box" || obj.tag == "Mimic")
        {
            obj.transform.GetChild(0).gameObject.SetActive(false);
            touchedBox = null;

            isCanOpenBox = false;

            if (obj.tag == "Mimic")
            {
                Mimic mm = obj.GetComponent<Mimic>();
                mm.animator.SetTrigger("Leave");
            }
        }
    }

}