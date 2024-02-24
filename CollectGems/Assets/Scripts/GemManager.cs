using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    // ��{�I��Unity���ŃX�R�A�Ɖ摜�̍����ւ����s��


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
            // �v���C���[���̏����Ăяo��
            PlayerManager pm = obj.GetComponent<PlayerManager>();
            pm.GetGem(gem);

            // ���b�Z�[�W����
            string ms = objName + "����肵���B";

            // ���b�Z�[�W�\��
            gm.MessageDisplay(ms, false);
            // ���ʉ��Đ�
            gm.SEPlay(gm.seGetGem);

            // ������j��
            Destroy(gameObject);
        }
    }
}
