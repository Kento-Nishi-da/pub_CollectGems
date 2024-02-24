using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Constants;
using System;
// ↑ここに using [③でつけた名前] とする
// このスクリプトで定数が使えるようになる

public class TitleManager : MonoBehaviour
{


    // 列挙体
    /// <summary>
    /// カーソル用
    /// 動かしたいカーソルの場所の分だけ列挙体の数を増やしていく
    /// 今回はスタートと終了の２こ
    /// </summary>
    enum TitleCursor
    {
        START,
        END,

        COUNT   // 特に列挙体の数値をいじらない場合は最後に COUNT をおいておくと
                // 簡単に要素数が取得できる
    }

    // コンポーネント取
    AudioSource audioSource;

    [SerializeField] AudioClip bgm;
    [SerializeField] AudioClip cursorSE;
    [SerializeField] AudioClip selectSE;

    [SerializeField] Text highScoreText;

    // カーソル変数宣言
    TitleCursor cursor;

    // カーソルの移動先となるボタンの色を変えたいので配列に格納
    [SerializeField] Image[] buttonArray;

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネント取得
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        // 変数初期化
        cursor = TitleCursor.START;


        // ハイスコア系の処理
        string playerName = PlayerPrefs.GetString(PrefsParam.KEY_PLAYER_NAME, "未登録");
        int highScore = PlayerPrefs.GetInt(PrefsParam.KEY_HIGH_SCORE, -1);
        highScoreText.text = "HIGH SCORE :  " + playerName + "." + highScore;
    }


    // Updateには長く書かずに関数化した処理を
    // 置いておくのが見やすくておすすめ

    // Update is called once per frame
    void Update()
    {
        // キー入力
        InputKey();

        // カーソル描画
        DrawCursor();
    }

    // ↓のsummaryは / を３かい
    /// <summary>
    /// キー入力受付関数
    /// カーソルを列挙体で管理
    /// </summary>
    void InputKey()
    {
        // 2パターンの操作に対応
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (cursor == 0)
            {
                cursor = TitleCursor.COUNT - 1;
                //var tmp =  Enum.GetValues(typeof(TitleCursor)).Length;
                // ほんとはこうする
                
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

        // 開発者用コマンド
        if(Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.Backspace))
        {
            // ハイスコアデータの消去
            PlayerPrefs.DeleteKey(PrefsParam.KEY_PLAYER_NAME);
            PlayerPrefs.DeleteKey(PrefsParam.KEY_HIGH_SCORE);

            EndButton();
        }
    }

    /// <summary>
    /// カーソル描画関数
    /// </summary>
    void DrawCursor()
    {
        // 配列に格納しているボタンの順番と宣言した列挙体の順番があっていれば
        // この for で汎用で使える
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
    /// スタートボタン押下
    /// カーソルに対応
    /// </summary>
    public void StartButton()
    {
        print("スタート");
        //SceneManager.LoadScene("GameScene");

        // 遅延処理をしてくれる関数、呼びたい関数の名前と秒数を指定
        Invoke("SceneChange", 1.0f);
    }

    void SceneChange()
    {
        //SceneManager.LoadScene("Map");
        SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// 終了ボタン
    /// カーソルに対応
    /// </summary>
    public void EndButton()
    {
        print("エンド");

        // エディターの状態なら再生を終了して、exeならexeを終了
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif

    }

}
