using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*  Unityでの定数
 *  
 *  メリット
 *  ・インスタンス化（オブジェクトにつける等）が不要なので簡単
 *  ・作ってあればどのスクリプトからも読み取れるので便利
 *  
 *  ↓書き方↓
    
    �@新しいスクリプトを作成（スクリプト名はわかりやすいものならなんでもいい）

    �Aこのコメントより上を残し、すべて消す

    �B下記のように namespace [名前] とかく（ここでの名前は後述の
                                            ｛各スクリプトの頭に書く名前｝
                                            になる）

    �C下記を参考にpublic class [名前] を作成（ここでの名前は後述の
                                            ｛定数を使うたびに書くクラス名｝
                                            になる）

    �D宣言したクラス内にpublic const [定数の型] [定数名]

    �E準備OK、他のスクリプトにGO
    まずは　TitleManager.cs　の 6行目

    PlayerMove.cs　の Start関数の頭のほう
 */

namespace Constants
{
    // クラスを複数作って見やすくできる
    public class PlayerParam
    {
        public const float SPEED_NORMAL = 6.0f;
        public const float SPEED_SLOWLY = 4.0f;



        //void TestFunc()
        //{
        //    // 関数も作れる
        //    // 汎用の関数を作っておくと便利
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