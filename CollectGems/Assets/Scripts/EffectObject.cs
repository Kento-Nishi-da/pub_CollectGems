using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ��̉�ʊO�ɏo����j��
        if(transform.position.y <  -7)
        {
            Destroy(gameObject);
        }

        // �����Ȃ���񂵑�����Ƃ�������
        transform.position += new Vector3(0, -3, 0) * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
    }
}
