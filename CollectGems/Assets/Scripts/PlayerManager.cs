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

    // 階段のTransform
    [SerializeField] Transform startArea;
    // 階段の位置を示す子の三角形
    Transform childTriangle;



    // この変数に触れた宝石のスコアを格納
    public int tmpScore;
    public int gemsCount;


    // 移動速度低下の閾値
    int gemsLimitAmount;


    float damageSpan;
    float damageDeltaTime;
    bool damageFlg;

    // 常時表示メッセージ
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

        // コンポーネント取得
        pMove = GetComponent<PlayerMove>();
        sr = GetComponent<SpriteRenderer>();

        // 変数初期化
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
        // キー入力
        InputKey();

        // 宝石の個数
        GemsCountCheck();

        // ダメージを受けていたら点滅
        DamageEffect();

        // アクションメッセージの判定処理
        ActionMessageCheck();

        // 階段の位置表示
        ShowExit();
    }


    /// <summary>
    /// 点滅処理の関数
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
        // カメラ移動
        SetCameraPosition();
    }

    /// <summary>
    /// カメラをPlayerに追従させる
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
    /// キー入力
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
    /// 宝石獲得処理の関数
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
    /// プレイヤーへの指示を示すメッセージの管理関数
    /// </summary>
    void ActionMessageCheck()
    {

        if(isCanOpenBox)
        {
            actionMes = "スペース長押しで箱を開けてお宝をゲット";
        }
        else if (isNoTime)
        {
            actionMes = "残り時間が０になるタイミングで階段に行こう";
        }
        else
        {
            actionMes = "WASD\n↑↓→←\nで移動";
        }

        gm.ActionMessage(actionMes);
    }

    /// <summary>
    /// 宝石の個数を参照してプレイヤーの移動速度を変化
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
    /// プレイヤーがダメージを受けた時の処理
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
    /// 階段の位置を表示する関数
    /// </summary>
    void ShowExit()
    {
        // 向きを求める
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
            gm.MessageDisplay("あっつ！宝石を落とした！", true);
        }

        if(obj.tag == "Holl")
        {
            while(getGemsList.Count > 0)
            {
                PlayerDamage();
            }

            
            gm.SEPlay(gm.seFall);
            transform.position = Vector3.zero;
            gm.MessageDisplay("すべての荷物を失った！", true);

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
            gm.MessageDisplay("上から大きな岩が！", true);
        }


        if(obj.tag == "Spike")
        {

            for(int i = 0; i < 2;i++)
            {
                PlayerDamage();
            }

            gm.SEPlay(gm.seBolder);
            gm.MessageDisplay("壁からトゲが！", true);

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