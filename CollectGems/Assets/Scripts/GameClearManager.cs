using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.ComponentModel;
using Constants;

public class GameClearManager : MonoBehaviour
{
    [SerializeField] Text scoreText;

    // �N���A���̉�ʂ̉��o�Ɏg���摜�̔z��
    [SerializeField] Sprite[] sprites;

    // ���o�Ɏg���I�u�W�F�N�g
    [SerializeField] GameObject effectObject;

    // �n�C�X�R�A���̃p�l��
    [SerializeField] GameObject panel;
    // �n�C�X�R�A���̖��O���͗̈�
    [SerializeField] InputField nameField;

    AudioSource audioSource;

    // �ꉞ��排���������邽�߂ɕ�����̃u���b�N���X�g
    [SerializeField]
    string[] badNames;

    // ���o�̂��߁A�X�R�A�\�������Z���Ă����悤�ɂ��邽�ߋ�̂��̂ƂQ�p��
    int tmpScore;
    int masterScore;

    // ���o�̐���A��莞�Ԃ��ƂɌĂԏ����Ɏg��
    float span;
    float deltaTime;

    // �p�l���\�����ɉ�ʂ����邳���������߉��o����p
    bool effectFlg;

    // �n�C�X�R�A�X�V������
    bool isNewRec;

    // Start is called before the first frame update
    void Start()
    {
        // ������
        panel.SetActive(false);
        isNewRec = false;
        effectFlg = true;
        span = 0.2f;
        deltaTime = 0;
        tmpScore = 0;
        // GameManager�̂ق���static�Ȃ̂Œ��A�N�Z�X
        masterScore = GameManager.scoreMaster;
        //masterScore = 5050;     // Debug

        // �T�E���h����
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.Play();

        // �V�L�^�B���̔���
        int oldHighScore = PlayerPrefs.GetInt(PrefsParam.KEY_HIGH_SCORE, -1);
        if(masterScore > oldHighScore)
        {
            isNewRec = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        InputKey();
        

        EffectControll();
    }

    // �L�[����
    void InputKey()
    {
        // �X�y�[�X�L�[���������Ƃ��ɐV�L�^���ǂ����ŏ����𕪊�
        // �n�C�X�R�A�̖��O�o�^��������
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // �V�L�^�Ȃ牉�o���~�߁A�p�l�����o��
            if (isNewRec)
            {
                effectFlg = false;
                panel.SetActive(true);
            }
            else
            {
                // �V�L�^����Ȃ��Ȃ�V�[���`�F���W
                SceneManager.LoadScene("Title");
            }
        }
    }

    /// <summary>
    /// ���o�p�̊֐�
    /// </summary>
    void EffectControll()
    {
        // string.Format�͂��낢���ނ��邩��O�O���Ă݂�
        scoreText.text = "��" + string.Format("{0:#,0}", tmpScore);


        // �������Ƃ̃X�R�A�ɂȂ�܂Ń}�C�t���[������������Ƃ��������̉��o
        if (tmpScore < masterScore)
        {
            tmpScore += 1000;
        }
        else
        {
            // �������璼�ڑ��
            tmpScore = masterScore;
        }

        // ���o�t���O���I���̊�
        if (effectFlg)
        {
            deltaTime += Time.deltaTime;

            // span�b���Ƃɏ������s��
            if (deltaTime > span)
            {
                deltaTime = 0;

                int rand = Random.Range(-8, 8);
                int randIndex = Random.Range(0, sprites.Length);
                Vector3 pos = new Vector3(rand, 7, 100);

                GameObject clone = effectObject;
                clone.GetComponent<SpriteRenderer>().sprite = sprites[randIndex];
                Instantiate(clone, pos, Quaternion.identity);

            }
        }
        
    }

    // �V�L�^���o���Ƃ��APlayerPrefs�ŕۑ�
    public void NewRecord()
    {
        string pName = nameField.text;

        for(int i = 0; i < badNames.Length; i++)
        {
            pName = pName.Replace(badNames[i], "*");
        }

        print(pName);

        // �萔�ŃL�[��������̂ŃL�[���ɕύX�������Ă����v
        PlayerPrefs.SetString(PrefsParam.KEY_PLAYER_NAME, pName);
        PlayerPrefs.SetInt(PrefsParam.KEY_HIGH_SCORE, masterScore);

        SceneManager.LoadScene("Title");
    }
}
