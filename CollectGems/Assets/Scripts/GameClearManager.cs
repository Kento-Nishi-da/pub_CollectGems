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

    // クリア時の画面の演出に使う画像の配列
    [SerializeField] Sprite[] sprites;

    // 演出に使うオブジェクト
    [SerializeField] GameObject effectObject;

    // ハイスコア時のパネル
    [SerializeField] GameObject panel;
    // ハイスコア時の名前入力領域
    [SerializeField] InputField nameField;

    AudioSource audioSource;

    // 一応誹謗中傷を避けるために文字列のブラックリスト
    [SerializeField]
    string[] badNames;

    // 演出のため、スコア表示を加算していくようにするため空のものと２つ用意
    int tmpScore;
    int masterScore;

    // 演出の制御、一定時間ごとに呼ぶ処理に使う
    float span;
    float deltaTime;

    // パネル表示時に画面がうるさかったため演出制御用
    bool effectFlg;

    // ハイスコア更新か判定
    bool isNewRec;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        panel.SetActive(false);
        isNewRec = false;
        effectFlg = true;
        span = 0.2f;
        deltaTime = 0;
        tmpScore = 0;
        // GameManagerのほうがstaticなので直アクセス
        masterScore = GameManager.scoreMaster;
        //masterScore = 5050;     // Debug

        // サウンド処理
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.Play();

        // 新記録達成の判定
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

    // キー入力
    void InputKey()
    {
        // スペースキーを押したときに新記録かどうかで処理を分岐
        // ハイスコアの名前登録をさせる
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // 新記録なら演出を止め、パネルを出す
            if (isNewRec)
            {
                effectFlg = false;
                panel.SetActive(true);
            }
            else
            {
                // 新記録じゃないならシーンチェンジ
                SceneManager.LoadScene("Title");
            }
        }
    }

    /// <summary>
    /// 演出用の関数
    /// </summary>
    void EffectControll()
    {
        // string.Formatはいろいろ種類あるからググってみて
        scoreText.text = "￥" + string.Format("{0:#,0}", tmpScore);


        // おおもとのスコアになるまでマイフレーム足し続けるといい感じの演出
        if (tmpScore < masterScore)
        {
            tmpScore += 1000;
        }
        else
        {
            // 超えたら直接代入
            tmpScore = masterScore;
        }

        // 演出フラグがオンの間
        if (effectFlg)
        {
            deltaTime += Time.deltaTime;

            // span秒ごとに処理を行う
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

    // 新記録が出たとき、PlayerPrefsで保存
    public void NewRecord()
    {
        string pName = nameField.text;

        for(int i = 0; i < badNames.Length; i++)
        {
            pName = pName.Replace(badNames[i], "*");
        }

        print(pName);

        // 定数でキーを作ったのでキー名に変更があっても大丈夫
        PlayerPrefs.SetString(PrefsParam.KEY_PLAYER_NAME, pName);
        PlayerPrefs.SetInt(PrefsParam.KEY_HIGH_SCORE, masterScore);

        SceneManager.LoadScene("Title");
    }
}
