using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Constants;

public class GameManager : MonoBehaviour
{

    [SerializeField] Text timerText;
    [SerializeField] Text messageDisplay;   // アイテム取得時に表示用
    [SerializeField] Text actionDisplay;    // 常時表示用
    public Text warningText;                // 一定条件達成で表示用

    [SerializeField] AudioSource audioSource1;  // ノーマルBGM用
    [SerializeField] AudioSource audioSource2;  // 残り時間減少BGM用

    [SerializeField] AudioClip bgm1;    // ノーマルBGM
    [SerializeField] AudioClip bgm2;    // 残り時間減少BGM

    public AudioClip seGetGem;    // 宝石取得時のSE
    public AudioClip seFireDamage;    // 焼かれたSE
    public AudioClip seMimic;    // ミミックに騙されたSE
    public AudioClip seFall;    // 全ロス落とし穴SE
    public AudioClip seBolder;    // 落下岩


    // プレイヤーのスクリプトを持っておく用の変数
    [SerializeField] PlayerManager playerManager;
    
    // 最終的にシーン外に渡すスコア
    // staticにしておくと別シーンから触れる
    public static int scoreMaster;

    //残り時間.
    [SerializeField] float countTime; 
    // BGMが切り替わるタイミング
    float bgmFlipTime;

    // アイテム取得時のメッセージのタイマー管理用
    float deltaTimeDisplay;
    float displayEraseTime;

    // BGMがすでに切り替わっているかどうか
    bool isBGMFlip;
    // ゲームが終了したかどうか
    bool isGameEnd;
    // ゲーム終了時にプレイヤーがスタート地点にいるかどうか
    public bool isPlayerReturn;


    // Start is called before the first frame update
    void Start()
    {
        // BGMごとにソースを設定
        audioSource1.clip = bgm1;
        audioSource2.clip = bgm2;

        // 通常BGMを再生開始
        audioSource1.Play();


        // 変数初期化


        messageDisplay.gameObject.SetActive(false);

        // タイマー初期化
        countTime = GameParam.TIME_LIMIT;
        // BGMの切り替わり時間　要調整
        bgmFlipTime = GameParam.BGM_FLIP_TIME;



        // EraseTime秒間メッセージ表示
        deltaTimeDisplay = 0;
        displayEraseTime = 5.0f;


        isBGMFlip = false;
        isGameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        //カウントダウン
        Timer();

        // テスト用
        TestFunc();

        // ゲーム終了チェック
        GameEndCheck();
    }



    /// <summary>
    /// タイマー処理関数
    /// 時間経過で行う処理
    /// </summary>
    void Timer()
    {
        if(!isGameEnd)
        {
            //カウントダウン
            countTime -= Time.deltaTime;
            //print(countTime);

            // テキストに表示
            timerText.text = "残り時間：" + countTime.ToString("f2");

            // 残り時間が既定のラインを超えるとBGM切り替え
            if (countTime < bgmFlipTime)
            {
                playerManager.isNoTime = true;
                BGMFlip();
            }

            // 残りの時間が0になるとゲーム終了
            if (countTime < 0)
            {
                isGameEnd = true;
            }
        }
        // メッセージ画面消去
        DisplayControll();
        
    }

    void TestFunc()
    {
        
    }

    /// <summary>
    /// BGM切り替え関数
    /// 残り時間が少なくなったら呼ばれる
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
    /// ゲーム終了時の処理関数
    /// </summary>
    void GameEndCheck()
    {
        if(isGameEnd)
        {
            scoreMaster = playerManager.tmpScore;
            //ここでプレイヤーが初期位置にいるかどうかを判定
            if(isPlayerReturn)
            {
                // 落とし穴で成功感出したくないので0点でもGameOverに
                if(scoreMaster <= 0)
                {
                    GameOver();
                }
                else
                {
                    // プレイヤーがスタート地点にいるかつ成果があるとき
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
    /// ゲームクリア処理関数
    /// </summary>
    void GameClear()
    {
        SceneManager.LoadScene("GameClear");
    }

    /// <summary>
    /// ゲーム失敗処理関数
    /// </summary>
    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    /// <summary>
    /// メッセージ表示
    /// ダメージメッセージは赤く表示
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

        // 指定時間後に消去
        deltaTimeDisplay = displayEraseTime;
        messageDisplay.text = ms;
    }

    /// <summary>
    /// 画面右側のディスプレイ
    /// 常時表示予定
    /// </summary>
    public void ActionMessage(string msg)
    {
        actionDisplay.text = msg;
    }

    /// <summary>
    /// メッセージを制御する
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
    /// 宝石取得時の効果音再生関数
    /// </summary>
    public void SEPlay(AudioClip se)
    {
        audioSource1.PlayOneShot(se);
    }

}
