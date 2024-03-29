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
        // 大体画面外に出たら破壊
        if(transform.position.y <  -7)
        {
            Destroy(gameObject);
        }

        // 落ちながら回し続けるといい感じ
        transform.position += new Vector3(0, -3, 0) * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
    }
}
