using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartArea : MonoBehaviour
{
    // 

    [SerializeField] GameObject gm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
         
        if(obj.tag == "Player")
        {
            print("プレイヤーが初期位置");
            gm.GetComponent<GameManager>().isPlayerReturn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player")
        {
            print("プレイヤーが初期位置から出た");
            gm.GetComponent<GameManager>().isPlayerReturn = false;
        }
    }
}
