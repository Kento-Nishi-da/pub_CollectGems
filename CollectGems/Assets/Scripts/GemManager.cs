using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    // 基本的にUnity側でスコアと画像の差し替えを行う


    public int score;

    public string objName;


    GameManager gm;
    GemManager gem;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gem = GetComponent<GemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if(obj.tag == "Player")
        {
            // プレイヤー側の処理呼び出し
            PlayerManager pm = obj.GetComponent<PlayerManager>();
            pm.GetGem(gem);

            // メッセージ生成
            string ms = objName + "を入手した。";

            // メッセージ表示
            gm.MessageDisplay(ms, false);
            // 効果音再生
            gm.SEPlay(gm.seGetGem);

            // 自分を破壊
            Destroy(gameObject);
        }
    }
}
