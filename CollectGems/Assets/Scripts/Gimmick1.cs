using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick1 : MonoBehaviour
{
    float span;
    float deltaTime;

    [SerializeField] bool isAvtive;

    // Start is called before the first frame update
    void Start()
    {
        isAvtive = false;
        deltaTime = 0;
        span = 3;
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += Time.deltaTime;
        // ƒtƒ‰ƒO”½“]
        if(deltaTime > span)
        {
            deltaTime = 0;
            isAvtive = !isAvtive;
        }


        if(isAvtive)
        { 
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
