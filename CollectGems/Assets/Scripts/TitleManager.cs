using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Constants;
using System;
// �������� using [�B�ł������O] �Ƃ���
// ���̃X�N���v�g�Œ萔���g����悤�ɂȂ�

public class TitleManager : MonoBehaviour
{


    // �񋓑�
    /// <summary>
    /// �J�[�\���p
    /// �����������J�[�\���̏ꏊ�̕������񋓑̂̐��𑝂₵�Ă���
    /// ����̓X�^�[�g�ƏI���̂Q��
    /// </summary>
    enum TitleCursor
    {
        START,
        END,

        COUNT   // ���ɗ񋓑̂̐��l��������Ȃ��ꍇ�͍Ō�� COUNT �������Ă�����
                // �ȒP�ɗv�f�����擾�ł���
    }

    // �R���|�[�l���g��
    AudioSource audioSource;

    [SerializeField] AudioClip bgm;
    [SerializeField] AudioClip cursorSE;
    [SerializeField] AudioClip selectSE;

    [SerializeField] Text highScoreText;

    // �J�[�\���ϐ��錾
    TitleCursor cursor;

    // �J�[�\���̈ړ���ƂȂ�{�^���̐F��ς������̂Ŕz��Ɋi�[
    [SerializeField] Image[] buttonArray;

    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g�擾
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        // �ϐ�������
        cursor = TitleCursor.START;


        // �n�C�X�R�A�n�̏���
        string playerName = PlayerPrefs.GetString(PrefsParam.KEY_PLAYER_NAME, "���o�^");
        int highScore = PlayerPrefs.GetInt(PrefsParam.KEY_HIGH_SCORE, -1);
        highScoreText.text = "HIGH SCORE :  " + playerName + "." + highScore;
    }


    // Update�ɂ͒����������Ɋ֐�������������
    // �u���Ă����̂����₷���Ă�������

    // Update is called once per frame
    void Update()
    {
        // �L�[����
        InputKey();

        // �J�[�\���`��
        DrawCursor();
    }

    // ����summary�� / ���R����
    /// <summary>
    /// �L�[���͎�t�֐�
    /// �J�[�\����񋓑̂ŊǗ�
    /// </summary>
    void InputKey()
    {
        // 2�p�^�[���̑���ɑΉ�
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (cursor == 0)
            {
                cursor = TitleCursor.COUNT - 1;
                //var tmp =  Enum.GetValues(typeof(TitleCursor)).Length;
                // �ق�Ƃ͂�������
                
            }
            else
            {
                cursor--;
            }
            audioSource.PlayOneShot(cursorSE);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (cursor == TitleCursor.COUNT - 1)
            {
                cursor = 0;
            }
            else
            {
                cursor++;
            }
            audioSource.PlayOneShot(cursorSE);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(selectSE);
            switch(cursor) 
            {
                case TitleCursor.START:
                    StartButton();
                    break;
                case TitleCursor.END:
                    EndButton();
                    break;
            }

        }

        // �J���җp�R�}���h
        if(Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.Backspace))
        {
            // �n�C�X�R�A�f�[�^�̏���
            PlayerPrefs.DeleteKey(PrefsParam.KEY_PLAYER_NAME);
            PlayerPrefs.DeleteKey(PrefsParam.KEY_HIGH_SCORE);

            EndButton();
        }
    }

    /// <summary>
    /// �J�[�\���`��֐�
    /// </summary>
    void DrawCursor()
    {
        // �z��Ɋi�[���Ă���{�^���̏��ԂƐ錾�����񋓑̂̏��Ԃ������Ă����
        // ���� for �Ŕėp�Ŏg����
        for(int i = 0; i < buttonArray.Length; i++) 
        {
            if((int)cursor == i)
            {
                buttonArray[i].color = Color.red;
            }
            else
            {
                buttonArray[i].color = Color.white;
            }
        }

    }


    /// <summary>
    /// �X�^�[�g�{�^������
    /// �J�[�\���ɑΉ�
    /// </summary>
    public void StartButton()
    {
        print("�X�^�[�g");
        //SceneManager.LoadScene("GameScene");

        // �x�����������Ă����֐��A�Ăт����֐��̖��O�ƕb�����w��
        Invoke("SceneChange", 1.0f);
    }

    void SceneChange()
    {
        //SceneManager.LoadScene("Map");
        SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// �I���{�^��
    /// �J�[�\���ɑΉ�
    /// </summary>
    public void EndButton()
    {
        print("�G���h");

        // �G�f�B�^�[�̏�ԂȂ�Đ����I�����āAexe�Ȃ�exe���I��
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif

    }

}
