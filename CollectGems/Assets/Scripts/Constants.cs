using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*  Unity�ł̒萔
 *  
 *  �����b�g
 *  �E�C���X�^���X���i�I�u�W�F�N�g�ɂ��铙�j���s�v�Ȃ̂ŊȒP
 *  �E����Ă���΂ǂ̃X�N���v�g������ǂݎ���̂ŕ֗�
 *  
 *  ����������
    
    �@�V�����X�N���v�g���쐬�i�X�N���v�g���͂킩��₷�����̂Ȃ�Ȃ�ł������j

    �A���̃R�����g������c���A���ׂď���

    �B���L�̂悤�� namespace [���O] �Ƃ����i�����ł̖��O�͌�q��
                                            �o�e�X�N���v�g�̓��ɏ������O�p
                                            �ɂȂ�j

    �C���L���Q�l��public class [���O] ���쐬�i�����ł̖��O�͌�q��
                                            �o�萔���g�����тɏ����N���X���p
                                            �ɂȂ�j

    �D�錾�����N���X����public const [�萔�̌^] [�萔��]

    �E����OK�A���̃X�N���v�g��GO
    �܂��́@TitleManager.cs�@�� 6�s��

    PlayerMove.cs�@�� Start�֐��̓��̂ق�
 */

namespace Constants
{
    // �N���X�𕡐�����Č��₷���ł���
    public class PlayerParam
    {
        public const float SPEED_NORMAL = 6.0f;
        public const float SPEED_SLOWLY = 4.0f;



        //void TestFunc()
        //{
        //    // �֐�������
        //    // �ėp�̊֐�������Ă����ƕ֗�
        //}

    }

    public class GameParam
    {
        public const float TIME_LIMIT = 60f;
        public const float BGM_FLIP_TIME = 30f;

        public const int GEMS_LIMIT_AMOUNT = 15;
    }

    public class PrefsParam
    {
        public const string KEY_PLAYER_NAME = "KeyPlayerName";
        public const string KEY_HIGH_SCORE = "KeyHighScore";
    }
}